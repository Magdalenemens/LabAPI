using DeltaCare.BAL;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : DeltaBaseController
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ILogger<ConfigurationController> _logger;
        public ConfigurationController(
            IDirectoryRepository directoryRepository,
            IConfigurationRepository configurationRepository,
            ILogger<ConfigurationController> logger)
        {
            _directoryRepository = directoryRepository;
            _configurationRepository = configurationRepository;
            _logger = logger;
        }

        #region System Configuration
        [HttpPost("Insert-systemconfig")]
        public async Task<ActionResult<int>> InsertSystemConfig([FromBody] SysConfigModel sysConfigModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _configurationRepository.InsertSystemConfig(sysConfigModel));
        }

        [HttpPut("Update-systemconfig/{Id}")]
        public async Task<ActionResult<int>> UpdateSystemConfig(int Id, [FromBody] SysConfigModel sysConfigModel)
        {
            if (Id != sysConfigModel.SYSCNFG_ID)
                return BadRequest("SYSCNFG_ID mismatch");

            var result = await _configurationRepository.UpdateSystemConfig(Id, sysConfigModel);
            if (result == 0)
            {
                return NotFound($"System Configuration with Id = {sysConfigModel.SYSCNFG_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-systemconfig/{Id}")]
        public async Task<ActionResult<int>> DeleteSystemConfig(int Id)
        {
            var result = await _configurationRepository.DeleteSystemConfig(Id);
            if (result == 0)
            {
                return NotFound($"System Configuration with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllSystemConfig")]
        public async Task<ActionResult> GetAllSystemConfig()
        {
            return Ok(await _configurationRepository.GetAllSystemConfig());
        }

        [HttpGet("GetSystemConfigById/{Id}")]
        public async Task<ActionResult<SysConfigModel>> GetSystemConfigById(int Id)
        {
            var result = await _configurationRepository.GetSystemConfigById(Id);
            if (result == null)
                return NotFound($"System Configuration with Id = {Id} not found");
            return result;
        }

        #endregion

        [HttpPost("Insert-sitetests-assignment")]
        public async Task<ActionResult<int>> InsertSiteTestsAssignment([FromBody] List<SiteTestsAssignmentModel> siteTestsAssignments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming the repository method can handle a list of assignments
            return Ok(await _configurationRepository.InsertSiteTestsAssignment(siteTestsAssignments));
        }

        [HttpDelete("Delete-siteTests/{Id}")]
        public async Task<ActionResult<int>> DeleteSiteTestsAssignment(int Id)
        {
            var result = (await _configurationRepository.DeleteSiteTestsAssignment(Id));
            if (result == 0)
            {
                return NotFound($"Site with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllSiteTestsAssignment")]
        public async Task<ActionResult> GetAllSiteTestsAssignment()
        {
            return this.Ok(await _configurationRepository.GetAllSiteTestsAssignment());
        }

        [HttpGet("GetSiteTestsAssignmentById/{Id}")]
        public async Task<ActionResult<SiteTestsAssignmentModel>> GetSiteTestsAssignmentById(int Id)
        {
            var result = await _configurationRepository.GetSiteTestsAssignmentById(Id);
            if (result == null)
                return NotFound($"Site with Id = {Id} not found");
            return result;
        }
    }
}
