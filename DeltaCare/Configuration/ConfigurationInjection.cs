using DeltaCare.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using System.Text;

namespace DeltaCare.Configuration
{
    public static class ConfigurationInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers(options =>
            {
                // Add global authorization filter
                options.Filters.Add(new AuthorizeFilter());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(swagger =>
            {
                // This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 8.0 Web API"
                });
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }},new string[] {}}});
            });
            return services;
        }
        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services,
            AppSettings configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.Jwt.Issuer,
                    ValidAudience = configuration.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Jwt.Key))
                };
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = new PathString("/auth/login");
                options.AccessDeniedPath = new PathString("/auth/denied");
            });
            return services;
        }
        public static IServiceCollection AddDbConnectionString(this IServiceCollection services,
           IConfiguration configuration)
        {

            DbConnectionString.ConnectionString = configuration.GetConnectionString("DeltaCaredb");

            return services;
        }
        public static IServiceCollection AddCors(this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            return services;
        }
    }
    // Initialize method to set up EmailConfig from configuration
    public static class ConfigHelper
    {
        public static EmailConfiguration EmailConfig { get; private set; }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            EmailConfig = serviceProvider.GetRequiredService<IConfiguration>().GetSection("EmailConfiguration").Get<EmailConfiguration>();
        }
    }
    public static class ThirdPartyConfigExtensions
    {
        public static void ConfigureThirdPartyLicenses(this WebApplication app)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            // Add more library configurations here if needed.
        }
    }
}
