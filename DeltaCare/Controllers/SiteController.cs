using DeltaCare.BAL;
using DeltaCare.BAL.Site;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : DeltaBaseController
    {
        private readonly ISiteRepository _siteRepository;
        public SiteController(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }         

        [HttpPut("Register-UserSites")]
        public async Task<ActionResult<int>> RegisterUserSites(List<UserSitesAccessModel> userSitesAccessModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming InsertOrUpdateEVReferenceRanges handles both insert and update operations
            var result = await _siteRepository.RegisterUserSites(userSitesAccessModels);
            return Ok(result);
        }

        [HttpGet("GetSitesByUserId/{userId}")]
        public async Task<IActionResult> GetSitesByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId (userId) cannot be null or empty.");
            }
            var evProfile = await _siteRepository.GetSitesByUserId(userId);

            if (evProfile == null)
            {
                return NotFound($"No UserId found for the provided Userid: {userId}");
            }

            return Ok(evProfile);
        }


        [HttpGet("GetAllUserSites")]
        public async Task<ActionResult> GetAllUserSites()
        {
            return Ok(await _siteRepository.GetAllUserSites());
        }

        [HttpGet("GetSiteDetailBySiteNo/{siteNo}")]
        public async Task<ActionResult<UserSitesAccessModel>> GetSiteDetailBySiteNo(string siteNo)
        {
            var result = await _siteRepository.GetSiteDetailBySiteNo(siteNo);
            if (result == null)
                return NotFound($"Site No = {siteNo} not found");
            return result;
        }

        [HttpDelete("Delete-UserSite/{Id}")]
        public async Task<ActionResult<int>> DeleteUserSite(int Id)
        {
            var result = await _siteRepository.DeleteUserSite(Id);
            if (result == 0)
            {
                return NotFound($"Site with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("FindAllSites/{search}")]
        public async Task<ActionResult> FindAllSites(string search)
        {
            return Ok(await _siteRepository.FindAllSites(search));
        }
    }
}
