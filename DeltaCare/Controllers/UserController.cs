using DeltaCare.BAL;
using DeltaCare.Entity.Model;
using DeltaCare.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : DeltaBaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
        private readonly string _validChars;
        private readonly int _passwordLength;

        public UserController(IUserRepository UserRepository, IConfiguration configuration)
        {
            _userRepository = UserRepository;
            _configuration = configuration;

            _validChars = configuration["PasswordSettings:ValidChars"];
            if (!int.TryParse(configuration["PasswordSettings:PasswordLength"], out _passwordLength))
            {
                _passwordLength = 8; // Default length if configuration value is invalid
            }
        }
        bool isDataInserted = false;
        [HttpGet("GetAllUser")]
        public async Task<ActionResult> GetAllUser()
        {
            return Ok(await _userRepository.GetAllUser());
        }

        [HttpGet("GetUserById/{Id}")]
        public async Task<ActionResult<UserFLModel>> GetUserById(int Id)
        {
            var result = await _userRepository.GetUserById(Id);
            if (result == null)
                return NotFound("UserFL_Id not found");
            return result;
        }

        [HttpGet("GetUserByUserId/{Id}")]
        public async Task<ActionResult<UserFLModel>> GetUserById(string userId)
        {
            var result = await _userRepository.GetUserId(userId);
            if (result == null)
                return NotFound("UserFL_Id not found");
            return result;
        }

        [HttpGet("GetUserById_pass/{Id}")]
        public async Task<ActionResult<UserFLModel>> GetUserById_pass(int Id)
        {
            var result = await _userRepository.GetUserById(Id);
            result.PASS_WORD = AesEncryptionHelper.Decrypt(result.PASS_WORD);
            if (result == null)
                return NotFound("UserFL_Id not found");
            return result;
        }


        [HttpPost("InsertUserFl")]
        public async Task<ActionResult<int>> InsertUser(UserFLModel userFLModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newPassword = GenerateRandomPassword(_passwordLength);
            userFLModel.PASS_WORD = newPassword;
            var isMailSent = await EmailSender.SendEmailAsync(userFLModel);
            if (isMailSent)
            {
                userFLModel.PASS_WORD = AesEncryptionHelper.Encrypt(newPassword);
                var insertResult = await _userRepository.InsertUser(userFLModel);
                if (insertResult > 0)
                {
                    isDataInserted = true;
                }
            }
            return Ok();
        }

        [HttpPut("UpdateUser/{Id}")]
        public async Task<IActionResult> UpdateUser(int Id, UserFLModel userFLModel)
        {
            if (Id != userFLModel.USER_FL_ID)
            {
                return BadRequest(new { success = false, message = "User ID mismatch" });
            }

            var existingUser = await _userRepository.GetUserById(Id);
            if (existingUser == null)
            {
                return NotFound(new { success = false, message = $"User with Id = {userFLModel.USER_FL_ID} not found" });
            }

            var result = await _userRepository.UpdateUser(Id, userFLModel);

            if (result == 0)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while updating the user. Please try again later." });
            }

            return Ok(new { success = true, message = "User updated successfully." });
        }


        [HttpDelete("DeleteUser/{Id}")]
        public async Task<ActionResult<int>> DeleteSite(int Id)
        {
            var result = (await _userRepository.DeleteUser(Id));
            if (result == 0)
            {
                return NotFound($"User with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestModel request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Request cannot be null." });
            }

            if (string.IsNullOrWhiteSpace(request.CurrentPassword) || string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return BadRequest(new { Message = "Both current and new passwords are required." });
            }

            var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id == 0)
            {
                return Unauthorized(new { Message = "User not authorized." });
            }
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            var isPasswordValid = await _userRepository.VerifyPasswordAsync(id, request.CurrentPassword);
            if (!isPasswordValid)
            {
                return BadRequest(new { Message = "Current password is incorrect." });
            }

            request.CurrentPassword = AesEncryptionHelper.Decrypt(request.CurrentPassword);
            request.NewPassword = AesEncryptionHelper.Decrypt(request.NewPassword);
            if (request.CurrentPassword == request.NewPassword)
            {
                return Conflict(new { Message = "The new password cannot be the same as the current password." });
            }

            request.CurrentPassword = AesEncryptionHelper.Encrypt(request.CurrentPassword);
            request.NewPassword = AesEncryptionHelper.Encrypt(request.NewPassword);
            var isPasswordChanged = await _userRepository.ChangePasswordAsync(id, request.CurrentPassword, request.NewPassword);
            if (!isPasswordChanged)
            {
                return StatusCode(500, new { Message = "Failed to change the password. Please try again later." });
            }

            return Ok(new { Message = "Password changed successfully." });
        }

        [HttpGet("GetAllUserLoginHistory")]
        public async Task<ActionResult> GetAllUserLoginHistory()
        {
            return Ok(await _userRepository.GetAllUserLoginHistory());
        }

        [HttpGet("GetUserLoginHistoryById/{Id}")]
        public async Task<ActionResult<IEnumerable<LoginFLModel>>> GetUserLoginHistoryById(string Id)
        {
            var result = await _userRepository.GetUserLoginHistoryById(Id);

            if (result == null || !result.Any())
                return NotFound($"User with Id = {Id} not found");

            return Ok(result);
        }

        [HttpPost("InsertUserLogInTime")]
        public async Task<ActionResult<int>> InsertUserLogInTime(LoginFLModel loginFLModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _userRepository.InsertUserLogInTime(loginFLModel));
        }

        [HttpPost("LogOut/{Id}")]
        public async Task<ActionResult<int>> LogOut(int Id, LoginFLModel loginFLModel)
        {
            if (Id != loginFLModel.LOGIN_FL_ID)
                return BadRequest("User ID mismatch");

            var result = (await _userRepository.UpdateUserLogoutTime(Id, loginFLModel));

            if (result == 0)
            {
                return NotFound($"User with Id = {loginFLModel.LOGIN_FL_ID} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {
            return Ok(await _userRepository.GetAllRoles());
        }

        [HttpGet("GetAllJobType")]
        public async Task<ActionResult> GetAllJobType()
        {
            return Ok(await _userRepository.GetAllJobType());
        }

        [HttpGet("GetAllUserJobType")]
        public async Task<ActionResult> GetAllUserJobType()
        {
            return Ok(await _userRepository.GetAllUserJobType());
        }


        [HttpGet("GetUserJobTypeById/{JobType}")]
        public async Task<ActionResult<UserJobTypeModel>> GetUserJobTypeById(string JobType)
        {
            var result = await _userRepository.GetUserJobTypeById(JobType);
            if (result == null)
                return NotFound($"JobType = {JobType} not found");
            return result;

        }

        [HttpPost("generate-password")]
        public async Task<IActionResult> GeneratePassword([FromBody] UserFLModel user)
        {
            var result = await InsertUser(user);
            if (result != null)
            {
                return Ok();
            }
            return BadRequest("Failed to generate password and insert user");
        }
        /// <summary>
        /// Insert users into  the table while accessing the module
        /// </summary>
        /// <param name="pageTrackRecordModel"></param>
        /// <returns></returns>

        [HttpPost("insert-pageRecord")]
        public async Task<ActionResult<int>> InsertPageRecord([FromBody] PageTrackRecordModel pageTrackRecordModel)
        {
            var result = await _userRepository.InsertPageRecord(pageTrackRecordModel);
            if (result != 0)
            {
                return Ok(result);
            }
            return NoContent();
        }
        /// <summary>
        /// Get Page Record based on UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("GetPageRecordByUserId/{UserId}")]
        public async Task<IActionResult> GetPageRecordbyUserId(string UserId)
        {
            var result = await _userRepository.GetPageRecord(UserId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to generate password and insert user");
        }

        [HttpGet("GetAllPageRecord")]
        public async Task<IActionResult> GetAllPageRecord()
        {
            var result = await _userRepository.GetAllPageRecord();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to generate password and insert user");
        }
        private string GenerateRandomPassword(int length)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(_validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}

