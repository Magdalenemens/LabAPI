using DeltaCare.BAL;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DeltaCare.Entity.Model.EVSetUpModel;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDirectoryController : DeltaBaseController
    {
        private readonly ITDRepository _tDRepository;
        public TestDirectoryController(ITDRepository tDRepository)
        {
            _tDRepository = tDRepository;
        }

        [HttpGet("GetAllTestDirectory")]
        public async Task<ActionResult> GetAllTestDirectory()
        {
            try
            {
                return Ok(await _tDRepository.GetAllTestDirectory());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("InsertTestDirectory")]
        public async Task<ActionResult<int>> InsertTestDirectory([FromBody] TestDirectoryModel testDirectoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _tDRepository.InsertTestDirectory(testDirectoryModel));
        }

        [HttpPut("UpdateTestDirectory/{Id}")]
        public async Task<ActionResult<int>> UpdateTestDirectory(int Id, [FromBody] TestDirectoryModel testDirectoryModel)
        {
            if (Id != testDirectoryModel.TD_ID)
                return BadRequest("Test Directory ID mismatch");

            var result = (await _tDRepository.UpdateTestDirectory(Id, testDirectoryModel));

            if (result == 0)
            {
                return NotFound($"Test Directory  with Id = {testDirectoryModel.TD_ID} not found");
            }
            return NoContent();
        }

        [HttpPut("UpdateTestDirectoryList")]
        public async Task<IActionResult> UpdateTestDirectoryList(IEnumerable<PriceMasterListModel> testDirectoryModel)
        {
            if (testDirectoryModel == null || !testDirectoryModel.Any())
            {
                return BadRequest("No test directory data provided.");
            }
            var result = await _tDRepository.UpdateTestDirectoryList(testDirectoryModel);

            if (result == 0)
            {
                return NotFound("No matching test directories found to update.");
            }
            return NoContent();
        }

        [HttpDelete("DeleteTestDirectory/{Id}")]
        public async Task<ActionResult<int>> DeleteTestDirectory(int Id)
        {
            var result = (await _tDRepository.DeleteTestDirectory(Id));
            if (result == 0)
            {
                return NotFound($"Test Directory  with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetTestDirectoryCombosByTable/{TableName}/{ID}")]
        public async Task<IActionResult> GetTestDirectoryCombosByTable(string TableName, string ID)//GET_TD
        {
            return Ok(await _tDRepository.GetTestDirectoryCombosByTable(TableName, ID));
        }

        [HttpGet("GetTestDirectoryByTCode/{TCODE}")]
        //public async Task<IActionResult> GET_TD([FromBody] TDModel tDModel)
        public async Task<IActionResult> GetTestDirectoryByTCode(string TCODE)//GET_TD
        {

            var GET_TD = _tDRepository.GetTestDirectoryByTCode(TCODE).Result.FirstOrDefault();
            //if (GET_TD == null)
            //    return NotFound(new { Message = "TD informaton not found!" });

            return Ok(new
            {
                GET_TD
            });
        }


        [HttpGet("GetGroupTestsDetailedandTestDirectory/{REQ_CODE}")]
        public async Task<IActionResult> GetGroupTestsDetailedandTestDirectory(string REQ_CODE)
        {

            if (REQ_CODE == null)
                return BadRequest();

            var GET_V_TD_GTD = await _tDRepository.GetGroupTestsDetailedandTestDirectory(REQ_CODE);


            if (GET_V_TD_GTD == null)
                return NotFound(new { Message = "GTD informaton not found!" });

            return Ok(new { GET_V_TD_GTD });
        }

        [HttpGet("GetGroupTestsDetailedandTestDirectoryParams/{REQ_CODE}/{TCODE}")]//GET_V_TD_GTD_TCODE
        public async Task<IActionResult> GetGroupTestsDetailedandTestDirectoryParams(string REQ_CODE, string TCODE)
        {
            if (REQ_CODE == null)
                return BadRequest();
            var GET_V_TD_GTD_TCODE = await _tDRepository.GetGroupTestsDetailedandTestDirectoryParams(REQ_CODE, TCODE);



            if (GET_V_TD_GTD_TCODE == null)
                return NotFound(new { Message = "View GTD informaton not found!" });

            return Ok(new { GET_V_TD_GTD_TCODE });
        }

        [HttpGet("GetTestDirectoryandGroupTestsByTCODE/{TCODE}")]//GET_p_TD_GT
        public async Task<IActionResult> GetTestDirectoryandGroupTestsByTCODE(string TCODE)
        {

            if (string.IsNullOrEmpty(TCODE))
                return BadRequest();

            var TD_GT = await _tDRepository.GetTestDirectoryandGroupTestsByTCODE(TCODE);


            if (TD_GT == null)
                return NotFound(new { Message = "GT not found!" });

            return Ok(new { TD_GT });
        }

        #region AP Test Definition

        [HttpPost("InsertAPTestDefinition")]
        public async Task<ActionResult<int>> InsertAPTestDefinition([FromBody] APTestDefinitionModel apTestDefinitionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _tDRepository.InsertAPTestDefinition(apTestDefinitionModel));
        }

        [HttpPut("UpdateAPTestDefinition/{Id}")]
        public async Task<ActionResult<int>> UpdateAPTestDefinition(int Id, [FromBody] APTestDefinitionModel apTestDefinitionModel)
        {
            if (Id != apTestDefinitionModel.TD_ID)
                return BadRequest("Test Directory ID mismatch");

            var result = (await _tDRepository.UpdateAPTestDefinition(Id, apTestDefinitionModel));

            if (result == 0)
            {
                return NotFound($"Test Directory  with Id = {apTestDefinitionModel.TD_ID} not found");
            }
            return NoContent();
        }


        [HttpDelete("DeleteAPTestDefinition/{Id}")]
        public async Task<ActionResult<int>> DeleteAPTestDefinition(int Id)
        {
            var result = (await _tDRepository.DeleteAPTestDefinition(Id));
            if (result == 0)
            {
                return NotFound($"Test Directory  with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllAPTestDefinition")]
        public async Task<ActionResult> GetAllAPTestDefinition()
        {
            return Ok(await _tDRepository.GetAllAPTestDefinition());

        }
        #endregion

        #region EV Test Definition and Profile

        [HttpPost("InsertEVTestDefinition")]
        public async Task<ActionResult<int>> InsertEVTestDefinition([FromBody] EVTestDefinitionModel evTestDefinitionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _tDRepository.InsertEVTestDefinition(evTestDefinitionModel));
        }

        [HttpPut("UpdateEVTestDefinition/{Id}")]
        public async Task<ActionResult<int>> UpdateEVTestDefinition(int Id, [FromBody] EVTestDefinitionModel evTestDefinitionModel)
        {
            if (Id != evTestDefinitionModel.TD_ID)
                return BadRequest("Test Directory ID mismatch");

            var result = (await _tDRepository.UpdateEVTestDefinition(Id, evTestDefinitionModel));

            if (result == 0)
            {
                return NotFound($"Test Directory  with Id = {evTestDefinitionModel.TD_ID} not found");
            }
            return NoContent();
        }


        [HttpDelete("DeleteEVTestDefinition/{Id}")]
        public async Task<ActionResult<int>> DeleteEVTestDefinition(int Id)
        {
            var result = (await _tDRepository.DeleteEVTestDefinition(Id));
            if (result == 0)
            {
                return NotFound($"Test Directory  with Id = {Id} not found");
            }
            return NoContent();
        }

        //[HttpGet("GetAllEVTestDefinition/{PageNumber}/{RowsOfPage}")]
        //public async Task<ActionResult> GetAllEVTestDefinition(int PageNumber, int RowsOfPage)
        //{
        //    return Ok(await _tDRepository.GetAllEVTestDefinition(PageNumber, RowsOfPage));
        //
        //}

        [HttpGet("GetAllEVTestDefinition")]
        public async Task<ActionResult> GetAllEVTestDefinition()
        {
            return Ok(await _tDRepository.GetAllEVTestDefinition());

        }

        [HttpGet("GetEVDefinitionById/{Id}")]
        public async Task<ActionResult<EVTestDefinitionModel>> GetEVDefinitionById(int Id)
        {
            var result = await _tDRepository.GetEVDefinitionById(Id);
            if (result == null)
                return NotFound($"Test with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-EVReferenceRanges")]
        public async Task<ActionResult<int>> InserEVReferenceRanges(List<EVReferenceRangeModel> eVReferenceRangeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming the repository method can handle a list of assignments
            return Ok(await _tDRepository.InserEVReferenceRanges(eVReferenceRangeModel));
        }

        [HttpPut("Update-EVReferenceRanges")]
        public async Task<ActionResult<int>> UpdateEVReferenceRanges(List<EVReferenceRangeModel> eVReferenceRangeModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming InsertOrUpdateEVReferenceRanges handles both insert and update operations
            var result = await _tDRepository.UpdateEVReferenceRanges(eVReferenceRangeModels);
            return Ok(result);
        }

        [HttpGet("FetchEVTDByReferenceRange/{tCode}")]
        public async Task<IActionResult> FetchEVTDByReferenceRange(string tCode)
        {
            if (string.IsNullOrEmpty(tCode))
            {
                return BadRequest("Test code (tCode) cannot be null or empty.");
            }
            var evProfile = await _tDRepository.FetchEVTDByReferenceRange(tCode);

            if (evProfile == null)
            {
                return NotFound($"No profile found for the provided test code: {tCode}");
            }

            return Ok(evProfile);
        }

        [HttpGet("FetchEVTDReferenceRangeByType/{sType}")]
        public async Task<IActionResult> FetchEVTDReferenceRangeByType(string sType)
        {
            if (string.IsNullOrEmpty(sType))
            {
                return BadRequest("Test code (sType) cannot be null or empty.");
            }
            var evProfile = await _tDRepository.FetchEVTDReferenceRangeBySType(sType);

            if (evProfile == null)
            {
                return NotFound($"No profile found for the provided test code: {sType}");
            }

            return Ok(evProfile);
        }

        [HttpDelete("DeleteEVReferenceRange/{Id}")]
        public async Task<ActionResult<int>> DeleteEVReferenceRange(int Id)
        {
            var result = (await _tDRepository.DeleteEVReferenceRange(Id));
            if (result == 0)
            {
                return NotFound($"Reference Range  with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllEVReferenceRange/{sType}")]
        public async Task<ActionResult> GetAllEVReferenceRange(string sType)
        {
            return Ok(await _tDRepository.GetAllEVReferenceRange(sType));
        }

        [HttpPost("Insert-EVTestDefinitionProfile")]
        public async Task<ActionResult<int>> InserEVTestDefinitionProfile(List<EVProfileGTDModel> eVProfileGTDModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming the repository method can handle a list of assignments
            return Ok(await _tDRepository.InserEVTestDefinitionProfile(eVProfileGTDModels));
        }

        [HttpPut("Update-EVTestDefinitionProfile")]
        public async Task<ActionResult<int>> UpdateEVTestDefinitionProfile(List<EVProfileGTDModel> eVProfileGTDModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming InsertOrUpdateEVTestDefinitionProfile handles both insert and update operations
            var result = await _tDRepository.UpdateEVTestDefinitionProfile(eVProfileGTDModels);
            return Ok(result);
        }

        [HttpGet("GetAllEVTestDefinitionProfile")]
        public async Task<ActionResult> GetAllEVTestDefinitionProfile()
        {
            return Ok(await _tDRepository.GetAllEVTestDefinitionProfile());

        }

        [HttpGet("FetchEVProfileFromGTD/{tCode}")]
        public async Task<IActionResult> FetchEVProfileFromGTD(string tCode)
        {
            if (string.IsNullOrEmpty(tCode))
            {
                return BadRequest("Test code (tCode) cannot be null or empty.");
            }
            var evProfile = await _tDRepository.FetchEVProfileFromGTD(tCode);

            if (evProfile == null)
            {
                return NotFound($"No profile found for the provided test code: {tCode}");
            }

            return Ok(evProfile);
        }

        [HttpGet("FetchProfileByGTDTCode/{gtdTCode}")]
        public async Task<IActionResult> FetchProfileByGTDTCode(string gtdTCode)
        {
            if (string.IsNullOrEmpty(gtdTCode))
            {
                return BadRequest("Test code (gtdTCode) cannot be null or empty.");
            }
            var evProfile = await _tDRepository.FetchProfileByGTDTCode(gtdTCode);

            if (evProfile == null)
            {
                return NotFound($"No profile found for the provided test code: {gtdTCode}");
            }

            return Ok(evProfile);
        }

        [HttpGet("GetAllEVProfiles/{search}")]
        public async Task<ActionResult> GetAllEVProfiles(string search)
        {
            return Ok(await _tDRepository.GetAllEVProfiles(search));
        }

        [HttpDelete("DeleteEVProfile/{Id}")]
        public async Task<ActionResult<int>> DeleteEVProfile(int Id)
        {
            var result = (await _tDRepository.DeleteEVProfile(Id));
            if (result == 0)
            {
                return NotFound($"Profile with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpPost("Insert-anlmethod")]
        public async Task<ActionResult<int>> InsertAnlMethod([FromBody] ANLMethodModel aNLMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _tDRepository.InsertAnlMethod(aNLMethod));
        }

        [HttpPut("Update-anlmethod/{Id}")]
        public async Task<ActionResult<int>> UpdateAnlMethod(int Id, [FromBody] ANLMethodModel aNLMethod)
        {
            if (Id != aNLMethod.ANL_MTHD_ID)
                return BadRequest("SP_TYPE_ID mismatch");

            var result = await _tDRepository.UpdateAnlMethod(Id, aNLMethod);
            if (result == 0)
            {
                return NotFound($"ANL with Id = {aNLMethod.ANL_MTHD_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("delete-anlmethod/{Id}")]
        public async Task<ActionResult<int>> DeleteAnlMethod(int Id)
        {
            var result = await _tDRepository.DeleteAnlMethod(Id);
            if (result == 0)
            {
                return NotFound($"ANL with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllANLMethod")]
        public async Task<ActionResult> GetAllANLMethod()
        {
            return Ok(await _tDRepository.GetAllANLMethod());
        }

        [HttpGet("GetANLMethodById/{Id}")]
        public async Task<ActionResult<ANLMethodModel>> GetANLMethodById(int Id)
        {
            var result = await _tDRepository.GetANLMethodById(Id);
            if (result == null)
                return NotFound($"ANL Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-evsubheader")]
        public async Task<ActionResult<int>> InsertEVSubHeader([FromBody] EVSubHeaderModel eVSubHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _tDRepository.InsertEVSubHeader(eVSubHeader));
        }

        [HttpPut("Update-evsubheader/{Id}")]
        public async Task<ActionResult<int>> UpdateEVSubHeader(int Id, [FromBody] EVSubHeaderModel eVSubHeader)
        {
            if (Id != eVSubHeader.EV_SUBHDR_ID)
                return BadRequest("SP_TYPE_ID mismatch");

            var result = await _tDRepository.UpdateEVSubHeader(Id, eVSubHeader);
            if (result == 0)
            {
                return NotFound($"ANL with Id = {eVSubHeader.EV_SUBHDR_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("delete-evsubheader/{Id}")]
        public async Task<ActionResult<int>> DeleteEVSubHeader(int Id)
        {
            var result = await _tDRepository.DeleteEVSubHeader(Id);
            if (result == 0)
            {
                return NotFound($"EV Sub Header with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllEVSubHeader")]
        public async Task<ActionResult> GetAllEVSubHeader()
        {
            return Ok(await _tDRepository.GetAllEVSubHeader());

        }

        [HttpGet("GetEVSubHeaderById/{Id}")]
        public async Task<ActionResult<EVSubHeaderModel>> GetEVSubHeaderById(int Id)
        {
            var result = await _tDRepository.GetEVSubHeaderById(Id);
            if (result == null)
                return NotFound($"EV Sub Header Id = {Id} not found");
            return result;
        }

        #endregion

        #region CG Test Definition and Profile
        [HttpPost("InsertCGTestDefinition")]
        public async Task<ActionResult<int>> InsertCGTestDefinition([FromBody] CGTestDefinitionModel cgTestDefinitionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _tDRepository.InsertCGTestDefinition(cgTestDefinitionModel));
        }

        [HttpPut("UpdateCGTestDefinition/{Id}")]
        public async Task<ActionResult<int>> UpdateCGTestDefinition(int Id, [FromBody] CGTestDefinitionModel cgTestDefinitionModel)
        {
            if (Id != cgTestDefinitionModel.TD_ID)
                return BadRequest("Test Directory ID mismatch");

            var result = (await _tDRepository.UpdateCGTestDefinition(Id, cgTestDefinitionModel));

            if (result == 0)
            {
                return NotFound($"Test Directory  with Id = {cgTestDefinitionModel.TD_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("DeleteCGTestDefinition/{Id}")]
        public async Task<ActionResult<int>> DeleteCGTestDefinition(int Id)
        {
            var result = (await _tDRepository.DeleteCGTestDefinition(Id));
            if (result == 0)
            {
                return NotFound($"Test Directory  with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllCGTestDefinition")]
        public async Task<ActionResult> GetAllCGTestDefinition()
        {
            return Ok(await _tDRepository.GetAllCGTestDefinition());
        }

        [HttpGet("GetCGDefinitionById/{Id}")]
        public async Task<ActionResult<CGTestDefinitionModel>> GetCGDefinitionById(int Id)
        {
            var result = await _tDRepository.GetCGDefinitionById(Id);
            if (result == null)
                return NotFound($"Test with Id = {Id} not found");
            return result;
        }

        //[HttpPost("Insert-CGTestDefinitionProfile")]
        //public async Task<ActionResult<int>> InserCGTestDefinitionProfile(List<CGProfileGTDModel> cgProfileGTDModels)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // Assuming the repository method can handle a list of assignments
        //    return Ok(await _tDRepository.InserCGTestDefinitionProfile(cgProfileGTDModels));
        //}

        //[HttpPut("Update-CGTestDefinitionProfile")]
        //public async Task<ActionResult<int>> UpdateCGTestDefinitionProfile(List<CGProfileGTDModel> cgProfileGTDModels)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // Assuming InsertOrUpdateCGTestDefinitonProfile handles both insert and update operations
        //    var result = await _tDRepository.UpdateCGTestDefinitionProfile(cgProfileGTDModels);
        //    return Ok(result);
        //}

        [HttpPost("managecgprofile")]
        public async Task<IActionResult> ManageCGTestDefinitionProfile([FromBody] List<CGProfileGTDModel> cgProfileGTDModels)
        {
            if (cgProfileGTDModels == null || !cgProfileGTDModels.Any())
            {
                return BadRequest("The input list cannot be null or empty.");
            }

            var (rowsInserted, rowsUpdated) = await _tDRepository.ManageCGTestDefinitionProfileAsync(cgProfileGTDModels);

            return Ok(new
            {
                RowsInserted = rowsInserted,
                RowsUpdated = rowsUpdated,
                Message = "Operation completed successfully."
            });

        }

        [HttpGet("GetAllCGTestDefinitionProfile")]
        public async Task<ActionResult> GetAllCGTestDefinitionProfile()
        {
            return Ok(await _tDRepository.GetAllCGTestDefinitionProfile());

        }

        [HttpGet("FetchCGProfileFromGTD/{tCode}")]
        public async Task<IActionResult> FetchCGProfileFromGTD(string tCode)
        {
            if (string.IsNullOrEmpty(tCode))
            {
                return BadRequest("Test code (tCode) cannot be null or empty.");
            }
            var evProfile = await _tDRepository.FetchCGProfileFromGTD(tCode);

            if (evProfile == null)
            {
                return NotFound($"No profile found for the provided test code: {tCode}");
            }

            return Ok(evProfile);
        }

        [HttpGet("FetchCGProfileByGTDTCode/{gtdTCode}")]
        public async Task<IActionResult> FetchCGProfileByGTDTCode(string gtdTCode)
        {
            if (string.IsNullOrEmpty(gtdTCode))
            {
                return BadRequest("Test code (gtdTCode) cannot be null or empty.");
            }
            var cgProfile = await _tDRepository.FetchCGProfileByGTDTCode(gtdTCode);

            if (cgProfile == null)
            {
                return NotFound($"No profile found for the provided test code: {gtdTCode}");
            }

            return Ok(cgProfile);
        }

        [HttpGet("GetAllCGProfiles/{search}")]
        public async Task<ActionResult> GetAllCGProfiles(string search)
        {
            return Ok(await _tDRepository.GetAllCGProfiles(search));
        }

        [HttpDelete("DeleteCGProfile/{Id}")]
        public async Task<ActionResult<int>> DeleteCGProfile(int Id)
        {
            var result = (await _tDRepository.DeleteCGProfile(Id));
            if (result == 0)
            {
                return NotFound($"Profile with Id = {Id} not found");
            }
            return NoContent();
        }
        #endregion


        #region Test Dirrectory Add Reference
        [HttpGet("GetAllTDDiv")]
        public async Task<ActionResult> GetAllTDDiv()
        {
            return Ok(await _tDRepository.GetAllTDDiv());
        }

        [HttpGet("GetAllSectByDiv/{Id}")]
        public async Task<ActionResult<TDDivision>> GetAllSectByDiv(int Id)
        {
            return Ok(await _tDRepository.GetAllSectByDiv(Id));
        }

        [HttpGet("getAllTestDirectiveByDiv/{Id}")]
        public async Task<ActionResult> getAllTestDirectiveByDiv(int Id)
        {
            var D = await _tDRepository.GetTestDirectory(Id);
            return Ok(D);
        }

        [HttpGet("GetAllTestsByDivision/{Id}")]
        public async Task<ActionResult> GetAllTestsByDivision(int Id)
        {
            var result = await _tDRepository.GetAllTestsByDivision(Id);
            return Ok(result);

        }

        [HttpGet("GetAllTDReferenceRange")]
        public async Task<ActionResult> GetAllTDReferenceRange()
        {
            var D = await _tDRepository.GetAllReferenceRange();
            return Ok(D);
        }

        [HttpPost("Insert-ReferenceRange")]
        public async Task<ActionResult<TDReferenceRangeModel>> InsertReferenceRange(List<TDReferenceRangeModel> _refRange)
        {
            var result = await _tDRepository.InsertReferenceRange(_refRange);
            return await Task.FromResult<ActionResult<TDReferenceRangeModel>>(Ok(result));
        }

        #endregion

        #region Test Directory Profile

        [HttpGet("GetTestDirectoryProfile")]
        public async Task<ActionResult> GetTestDirectoryProfile()
        {
            try
            {
                return Ok(await _tDRepository.GetTestDirectoryProfile());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        [HttpGet("GetTestDirectoryGTD/{id}")]
        public async Task<ActionResult> GetTestDirectoryGTD(string id) => Ok(await _tDRepository.GetTestDirectoryGTD(id));

        [HttpGet("GetTestDirectoryByDProfile")]
        public async Task<ActionResult> GetTestDirectoryByDProfile()
        {
            try
            {
                return Ok(await _tDRepository.GetTestDirectoryByDProfile());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet("GetSearchTestDirectoryByDProfile/{Search}")]
        public async Task<ActionResult> GetSearchTestDirectoryByDProfile(string Search)
        {
            try
            {
                return Ok(await _tDRepository.GetSearchTestDirectoryByDProfile(Search));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("Insert-TestDirectoryProfile")]
        public async Task<ActionResult<GTDModel>> InsertTestDirectoryProfile(List<GTDModel> _gtdModel)
        {
            try
            {
                var result = await _tDRepository.InsertTestDirectoryProfile(_gtdModel);
                return await Task.FromResult<ActionResult<GTDModel>>(Ok(result));

            }
            catch (Exception ex)
            {
                return await Task.FromResult<ActionResult<GTDModel>>(StatusCode(500, "Internal Server Error! Please Contact Admin! " + ex.Message));
            }
        }
        #endregion
    }
}


