using DeltaCare.BAL.Clinical.AP_Reports;
using DeltaCare.BAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeltaCare.Entity.Model;

namespace DeltaCare.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class MBReportController : DeltaBaseController
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IReportRepository _reportRepository;
        private readonly ILogger<APReportController> _logger;

        public MBReportController(
         IDirectoryRepository directoryRepository,
         IReportRepository reportRepository,
         ILogger<APReportController> logger)
        {
            _directoryRepository = directoryRepository;
            _reportRepository = reportRepository;
            _logger = logger;
        }

        [HttpPatch("Update-mbReport/{Id}")]
        public async Task<ActionResult<int>> UpdateMBReport(int Id, [FromBody] MBReportModel mBReportModel)
        {
            // Validate the Id and partialModel
            if (Id != mBReportModel.ARF_ID)
            {
                return BadRequest("ARF_ID mismatch");
            }

            // Perform the partial update using the clinical repository
            var result = await _reportRepository.UpdateMBReport(Id, mBReportModel);

            // Check if the update was successful
            if (result == 0)
            {
                return NotFound($"MicroBiologyModel with Id = {Id} not found");
            }

            // Return a success response
            return NoContent();
        }


        [HttpPatch("Update-cgReport/{Id}")]
        public async Task<ActionResult<int>> UpdateCGReport(int Id, [FromBody] CGReportModel cgReportModel)
        {
            // Validate the Id and partialModel
            if (Id != cgReportModel.ARF_ID)
            {
                return BadRequest("ARF_ID mismatch");
            }

            // Perform the partial update using the clinical repository
            var result = await _reportRepository.UpdateCGReport(Id, cgReportModel);

            // Check if the update was successful
            if (result == 0)
            {
                return NotFound($"Cytogenetic Model with Id = {Id} not found");
            }

            // Return a success response
            return NoContent();
        }
    }
}
