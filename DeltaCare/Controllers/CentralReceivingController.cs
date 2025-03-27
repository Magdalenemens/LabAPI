using DeltaCare.BAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentralReceivingController : DeltaBaseController
    {
        private readonly ICentralReceivingRepository _centralReceivingRepository;
        public CentralReceivingController(ICentralReceivingRepository centralReceivingRepository)
        {
            _centralReceivingRepository = centralReceivingRepository;
        }
        [HttpGet("GetOrdersDetailsByAccn/{ACCN}/{STS}")]
        public async Task<IActionResult> GetOrdersDetailsByAccn(string ACCN, string STS)
        {
            var Ord_Dtl = await _centralReceivingRepository.GetOrdersDetailsByAccn(ACCN,STS);
            //if (Ord_Dtl == null)
            //    return NotFound(new { Message = "Order not found!" });
            return Ok(new
            {
                Ord_Dtl
            });
        }

        [HttpPut("CentralReceivingOrders/{ACCN}")]//Collected_ATR
        public async Task<ActionResult<int>> CentralReceivingOrders([FromBody] Object[] ORDs,  string ACCN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _centralReceivingRepository.CentralReceivingOrders(ORDs, ACCN));
        }

        [HttpPut("UpdateCentralReceiving/{ACCN}/{REQ_CODE}/{SECT}/{ATRID}/{ORD_NO}/{SITE_NO}/{U_ID}")]//Collected_ATR
        public async Task<ActionResult<int>> UpdateCentralReceiving([FromBody] Object[] ORDs, string ACCN, string REQ_CODE, string SECT, int ATRID, string ORD_NO, string SITE_NO, string U_ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _centralReceivingRepository.UpdateCentralReceiving(ORDs, ACCN, REQ_CODE, SECT, ATRID, ORD_NO, SITE_NO, U_ID));
        }
    }
}
