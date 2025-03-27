using DeltaCare.Entity.Model;
using System.Security.Claims;

namespace DeltaCare.BAL.Common
{
    public interface ITokenRepository
    {
        public string GenerateToken(UserFLModel userFLModel);
        public ClaimsPrincipal ValidateToken(string token);
    }
}
