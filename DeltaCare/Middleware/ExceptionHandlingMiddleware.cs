
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace DeltaCare.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {

        public readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "SQL Exception",
                    $"Procedure: {ex.Procedure}, Line: {ex.Errors[0].LineNumber},Message:{ex.Message}");

                if (ex.Message.ToLower().Contains("already exists"))
                {
                    await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict, "Conflict", ex.Message);
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Argument Exception: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Invalid Argument", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized Access: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "Unauthorized Access", ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Not Found: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, "Resource Not Found", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal Server Error: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "Internal Server Error", "An unexpected error occurred.");
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode, string type, string detail)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Type = type,
                Title = $"Error occurred in: {type}",
                Detail = detail,
                Instance = context.Request.Path
            };

            // Log detailed information and convert to JSON response
            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        // Custom NotFoundException 
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }
    }

}
