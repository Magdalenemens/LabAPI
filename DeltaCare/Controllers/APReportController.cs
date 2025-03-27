using DeltaCare.BAL;
using DeltaCare.BAL.Clinical.AP_Reports;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APReportController : DeltaBaseController
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IReportRepository _reportRepository;
        private readonly ILogger<APReportController> _logger;

        public APReportController(
           IDirectoryRepository directoryRepository,
           IReportRepository reportRepository,
           ILogger<APReportController> logger)
        {
            _directoryRepository = directoryRepository;
            _reportRepository = reportRepository;
            _logger = logger;
        }

        [HttpPatch("Update-apReport/{Id}")]
        public async Task<ActionResult<int>> UpdateAPReport(int Id, [FromBody] APReportModel aPReportModel)
        {
            // Validate the Id and partialModel
            if (Id != aPReportModel.ARF_ID)
            {
                return BadRequest("ARF_ID mismatch");
            }

            // Perform the partial update using the clinical repository
            var result = await _reportRepository.UpdateAPReport(Id, aPReportModel);

            // Check if the update was successful
            if (result == 0)
            {
                return NotFound($"AnatomicModel with Id = {Id} not found");
            }

            // Return a success response
            return NoContent();
        }
    }
}
