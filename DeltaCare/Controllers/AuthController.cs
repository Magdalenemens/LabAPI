using DeltaCare.BAL;
using DeltaCare.BAL.Common;
using DeltaCare.Entity.Model;
using DeltaCare.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace JWTAuth_Validation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DeltaCare.Helper.Authorize]

    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(IUserRepository userRepository,
            ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        [AllowAnonymous]
        [HttpPost("validateUser")]
        public async Task<IActionResult> Auth([FromBody] LoginModel data)
        {
            //data.Password = AesEncryptionHelper.Decrypt(data.Password);
            UserFLModel userData = await IsValidUserInformation(data);
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            // Handle IPv6 loopback address
            if (remoteIpAddress == "::1")
            {
                remoteIpAddress = "127.0.0.1";
            }
            if (userData != null && userData.USER_NAME != null)
            {
                var tokenString = AesEncryptionHelper.Encrypt(_tokenRepository.GenerateToken(userData));
                LoginFLModel model = new LoginFLModel();
                model.STATION_ID = remoteIpAddress;
                model.U_ID = userData.USER_ID;
                model.USER_CODE = userData.USER_CODE;
                model.FULL_NAME = userData.FULL_NAME;
                TimeZoneInfo saudiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Riyadh");
                model.IN_DTTM = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, saudiTimeZone);
                await _userRepository.InsertUserLogInTime(model);

                return Ok(new { Token = tokenString, Message = "Success", FullName = userData.FULL_NAME, UserId = userData.USER_ID });
            }
            return BadRequest("Please pass the valid Username and Password");
        }

        [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            return Ok("API Validated");
        }

        private async Task<UserFLModel> IsValidUserInformation(LoginModel model)
        {
            var getUserData = await _userRepository.GetUserByUserIdAndPass(model);
            if (getUserData != null && getUserData.USER_NAME != null)
            {
                return getUserData;
            }
            else return null;
        }
    }
}