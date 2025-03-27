using DeltaCare.Common;
using DeltaCare.Helper;
using DeltaCare.Middleware;

namespace DeltaCare.Extension
{
    public static class HostingExtension
    {
        public static async Task<WebApplication> ConfigurePipeline(this WebApplication app, AppSettings configuration)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Deltacare API v1");
                });
            }
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<JWTMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.MapControllers();
            await app.RunAsync();
            return app;
        }
    }
}

