using DeltaCare.BAL;
using DeltaCare.BAL.UserAccess;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccessController : DeltaBaseController
    {
        private readonly IUserAccessRepository _userAccessRepository;       
        public UserAccessController(IUserAccessRepository userAccessRepository)
        {
            _userAccessRepository = userAccessRepository;
        }

        [HttpGet("GetModuleAccessDetailsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<UserAccesseModel>>> GetModuleAccessDetailsByUserId(string userId)
        {
            var result = await _userAccessRepository.GetModuleAccessDetailsByUserId(userId);

            if (result == null || !result.Any())
                return NotFound($"User with Id = {userId} not found");

            return Ok(result);
        }
    }
}
