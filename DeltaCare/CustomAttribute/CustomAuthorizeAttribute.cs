using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DeltaCare.CustomAttribute
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(params string[] roles)
            : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = new object[] { roles };
        }
    } 
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly string[] _roles;
        public CustomAuthorizeFilter(string[] roles)
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var userRoles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
            if (!_roles.Any(role => userRoles.Contains(role)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
