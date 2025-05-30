﻿using DeltaCare.BAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class PreAnalyticalReceivingController : DeltaBaseController
    {
        private readonly IPreAnalyticalReceivingRepository _preAnalyticalReceivingRepository;
        public PreAnalyticalReceivingController(IPreAnalyticalReceivingRepository preAnalyticalReceivingRepository)
        {
            _preAnalyticalReceivingRepository = preAnalyticalReceivingRepository;
        }
        [HttpGet("GetOrdersDetailsByAccn/{ACCN}/{STS}")]
        public async Task<IActionResult> GetOrdersDetailsByAccn(string ACCN, string STS)
        {
            var Ord_Dtl = await _preAnalyticalReceivingRepository.GetOrdersDetailsByAccn(ACCN, STS);
            //if (Ord_Dtl == null)
            //    return NotFound(new { Message = "Order not found!" });
            return Ok(new
            {
                Ord_Dtl
            });
        }

        [HttpPut("UpdatePreAnalyticalReceiving/{ACCN}/{REQ_CODE}/{SECT}/{ATRID}/{ORD_NO}/{SITE_NO}/{U_ID}")]//Collected_ATR
        public async Task<ActionResult<int>> UpdatePreAnalyticalReceiving([FromBody] Object[] ORDs, string ACCN, string REQ_CODE, string SECT, int ATRID, string ORD_NO, string SITE_NO, string U_ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _preAnalyticalReceivingRepository.UpdatePreAnalyticalReceiving(ORDs, ACCN, REQ_CODE, SECT, ATRID, ORD_NO, SITE_NO, U_ID));
        }
    }
}
