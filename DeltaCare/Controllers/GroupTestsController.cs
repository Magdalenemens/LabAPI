using DeltaCare.BAL;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupTestsController : DeltaBaseController
    {
        private readonly IGTRepository _GTRepository;
        public GroupTestsController(IGTRepository gTRepository)
        {
            _GTRepository = gTRepository;
        }

        [HttpGet("GetGroupTestsByReqCode/{REQ_CODE}")]
        public async Task<IActionResult> GetGroupTestsByReqCode(string REQ_CODE)//GET_GT
        {

            if (REQ_CODE == null)
                return BadRequest();
            var GET_GT =  _GTRepository.GetGroupTestsByReqCode(REQ_CODE).Result.FirstOrDefault();
                //.Where(x => x.REQ_CODE == REQ_CODE).ToList();
            //if (GET_GT == null)
             //   return NotFound(new { Message = "Group Tests informaton not found!" });
            return Ok(new
            {
                 GET_GT
            });
        }
        [HttpGet("GetGroupTestsDetailedByParams/{GTNO}/{REQ_CODE}")]
        public async Task<IActionResult> GetGroupTestsDetailedByParams(string GTNO, string REQ_CODE)//GET_GTD
        {
            if (REQ_CODE == null)
                return BadRequest();
            var GET_GTD = await _GTRepository.GetGroupTestsDetailedByParams(GTNO, REQ_CODE);
                //.Where(x => x.REQ_CODE == REQ_CODE && x.GTNO == GTNO)
                //.OrderBy(x => x.GTD_ID).ToList();
            if (GET_GTD == null)
                return NotFound(new { Message = "Group Tests Detailed informaton not found!" });
            return Ok(new
            {
                GET_GTD
            });
        }
        [HttpGet("GetGroupTestsDetailedandGroupTests")]
        public async Task<IActionResult> GetGroupTestsDetailedandGroupTests()//V_GT_GTD
        {           
            var GET_GTD = await _GTRepository.GetGroupTestsDetailedandGroupTests();
            if (GET_GTD == null)
                return NotFound(new { Message = "GTD informaton not found!" });
            return Ok(new
            {
                GET_GTD
            });
        }
    }
}
