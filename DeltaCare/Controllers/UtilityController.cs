using DeltaCare.BAL.Account;
using DeltaCare.BAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeltaCare.BAL.Common;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : DeltaBaseController
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IUtilityRepository _utilityRepository;
        private readonly ILogger<UtilityController> _logger;
        public UtilityController(
            IDirectoryRepository directoryRepository,
            IUtilityRepository utilityRepository,
            ILogger<UtilityController> logger)
        {
            _directoryRepository = directoryRepository;
            _utilityRepository = utilityRepository;
            _logger = logger;
        }

        [HttpGet("GetMaxValue/{tableName}/{columnName}")]
        public async Task<IActionResult> GetMaxValue(string tableName, string columnName)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName))
            {
                return BadRequest("Table name and column name are required.");
            }
            int maxValue = await _utilityRepository.GetMaxValueAsync(tableName, columnName);
            return Ok(new { MaxValue = maxValue });
        }
    }
}

