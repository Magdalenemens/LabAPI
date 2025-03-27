using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics.Contracts;

namespace DeltaCare.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter, IFilterMetadata
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var endpoint = context.HttpContext.Features.Get<IEndpointFeature>()?.Endpoint;
            if (endpoint != null && endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                return;
            }
            var account = context.HttpContext.Items["User"];
            if (account == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}