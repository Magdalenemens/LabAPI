using DeltaCare.Helper;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    public AuthenticationMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            ClaimsPrincipal tokenData = ValidateToken(token);
            if (tokenData != null && tokenData.Claims != null)
            {
                var identity = new ClaimsIdentity(tokenData.Claims, "Bearer");
                context.User = new ClaimsPrincipal(identity);
                context.Items["User"] = tokenData.Claims;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid Token");
                return;
            }
        }
        else
        {
            //return;
        }
        await _next(context);
    }
    public ClaimsPrincipal ValidateToken(string token)
    {
        token = AesEncryptionHelper.Decrypt(token);
        IdentityModelEventSource.ShowPII = true;
        TokenValidationParameters validationParameters = new()
        {
            ValidIssuer = _config["Jwt:Issuer"],
            ValidAudience = _config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
        var principal = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);
        return principal;
    }
}
