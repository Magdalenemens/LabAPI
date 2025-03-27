using DeltaCare.BAL;
using DeltaCare.BAL.Clinical.AP;
using DeltaCare.BAL.Clinical.AP_Reports;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicalController : DeltaBaseController
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IClinicalRepository _clinicalRepository;
        private readonly IGenLabRepository _genLabRepository;
        private readonly ILogger<ClinicalController> _logger;

        public ClinicalController(
            IDirectoryRepository directoryRepository,
            IClinicalRepository clinicalRepository,
            IGenLabRepository genLabRepository,
            ILogger<ClinicalController> logger)
        {
            _directoryRepository = directoryRepository;
            _clinicalRepository = clinicalRepository;
            _genLabRepository = genLabRepository;
            _logger = logger;
        }

        #region General Lab
        [HttpGet("GetAllAccnActiveResultsFile")]
        public async Task<IActionResult> GetAllAccnActiveResultsFile()
        {
            var GetAllAccnARF = await _genLabRepository.GetAllAccnActiveResultsFile();
            //if (GetAllAccnARF == null)
            //   return NotFound(new { Message = "ARF not found!" });
            return Ok(new
            {
                GetAllAccnARF
            });
        }

        [HttpGet("GetAccnActiveResultsFileList/{ACCN}")]
        public async Task<IActionResult> GetAccnActiveResultsFileList(string ACCN)
        {
            var GetAccnARFList = await _genLabRepository.GetAccnActiveResultsFileList(ACCN);
            //if (GetAccnARFList == null)
            //   return NotFound(new { Message = "ARF List not found!" });
            return Ok(new
            {
                GetAccnARFList
            });
        }

        [HttpPut("UpdateActiveResultsFileGenLab/{ACCN}/{REQ_CODE}/{LHF}/{ARF_ID}/{ORD_NO}/{RESULT}")]
        public async Task<ActionResult<int>> UpdateActiveResultsFileGenLab([FromBody] Object[] ARFs, string ACCN, string REQ_CODE, string LHF, int ARF_ID, string ORD_NO, string RESULT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _genLabRepository.UpdateActiveResultsFileGenLab(ARFs, ACCN, REQ_CODE, LHF, ARF_ID, ORD_NO, RESULT));
        }

        [HttpPut("UpdateNotesActiveResultsFileGenLab/{ARF_ID}/{ACCN}/{NOTES}")]
        public async Task<ActionResult<int>> UpdateNotesActiveResultsFileGenLab(int ARF_ID, string ACCN, string NOTES)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _genLabRepository.UpdateNotesActiveResultsFileGenLab(ARF_ID, ACCN, NOTES));
        }

        [HttpGet("GetAlphaResponsesByCD/{CD}")]
        public async Task<ActionResult<ARTemplateModel>> GetAlphaResponsesByCD(string CD)
        {
            try
            {
                var result = await _genLabRepository.GetAlphaResponsesByCD(CD);
                //if (result == null)
                //   return NotFound("CD Response not found");
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("GetAlphaValuesByCode/{TCODE}/{RESVAL}")]
        public async Task<ActionResult<AVTemplateModel>> GetAlphaValuesByCode(string TCODE, string RESVAL)
        {
            try
            {
                var result = await _genLabRepository.GetAlphaValuesByCode(TCODE, RESVAL);
                //if (result == null)
                //    return NotFound("CD Response not found");
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("GetInterpretiveValuesByCode/{TCODE}/{SEX}/{rsultvalue}")]
        public async Task<ActionResult<IVTemplateModel>> GetInterpretiveValuesByCode(string TCODE, string SEX, decimal rsultvalue)
        {
            try
            {
                var result = await _genLabRepository.GetInterpretiveValuesByCode(TCODE, SEX, rsultvalue);
                //if (result == null)
                //   return NotFound("CD Response not found");
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetAccnActiveResultsFileInterp/{ACCN}/{TCODE}")]
        public async Task<IActionResult> GetAccnActiveResultsFileInterp(string ACCN, string TCODE)
        {
            var GetAccnARFList = await _genLabRepository.GetAccnActiveResultsFileInterp(ACCN, TCODE);
            //if (GetAccnARFList == null)
            //   return NotFound(new { Message = "ARF List not found!" });
            return Ok(new
            {
                GetAccnARFList
            });
        }

        [HttpPost("InsertResultModified")]
        public async Task<ActionResult> InsertResultModified([FromBody] ResultModifiedModel resultModifiedModel)
        {
            return Ok(await _genLabRepository.InsertResultModified(resultModifiedModel));
        }

        [HttpPut("UpdateResultModified/{PAT_ID}/{ACCN}/{TCODE}/{CRESULT}/{CV_ID}/{RESULT}/{V_ID}")]
        public async Task<ActionResult<int>> UpdateResultModified(string PAT_ID, string ACCN, string TCODE, string CRESULT, string CV_ID, string RESULT, string V_ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _genLabRepository.UpdateResultModified(PAT_ID, ACCN, TCODE, CRESULT, CV_ID, RESULT, V_ID));
        }



        #endregion

        #region  AnatomicPathology
        /// <summary>
        /// Get All the Anatomic Results
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAnatomicPathology")]
        public async Task<ActionResult> GetAllAnatomicPathology()
        {
            return Ok(await _clinicalRepository.GetAllAnatomicPathology());
        }

        [HttpGet("GetAnatomicPathologyById/{Id}")]
        public async Task<ActionResult<AnatomicModel>> GetAnatomicPathologyById(int Id)
        {
            var result = await _clinicalRepository.GetAnatomicPathologyById(Id);
            if (result == null)
                return NotFound($"Patient Id = {Id} not found");
            return result;
        }

        [HttpGet("GetRTForAnatomyPathology")]
        public async Task<ActionResult> GetRTForAnatomyPathology()
        {
            return Ok(await _clinicalRepository.GetRTForAnatomyPathology());
        }

        [HttpGet("GetRTForAnatomyPathologyById/{Id}")]
        public async Task<ActionResult<ResultsTemplatesModel>> GetResultsTemplatesById(int Id)
        {
            var result = await _clinicalRepository.GetRTForAnatomyPathologyById(Id);
            if (result == null)
                return NotFound($"Results Templates with Id = {Id} not found");
            return result;
        }

        [HttpGet("GetAllClinicalFindings")]
        public async Task<ActionResult> GetAllClinicalFindings()
        {
            return Ok(await _clinicalRepository.GetAllClinicalFindings());
        }
        /// <summary>
        /// Fetch data by accession number to display T & M Axis Data
        /// </summary>
        /// <param name="accessionnumber"></param>
        /// <returns></returns>

        [HttpGet("GetClinicalFindingByAccessionNumber/{accessionnumber}")]
        public async Task<ActionResult> GetClinicalFindingByAccessionNumber(string accessionnumber)
        {
            return Ok(await _clinicalRepository.GetClinicalFindingByAccessionNumber(accessionnumber));
        }


        [HttpPost("Insert-pathfinding")]
        public async Task<ActionResult<int>> InsertPathFinding([FromBody] PathFindingModel pathFinding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _clinicalRepository.InsertPathFinding(pathFinding));
        }

        [HttpPut("Update-pathfinding/{Id}")]
        public async Task<ActionResult<int>> UpdatePathFinding(int Id, [FromBody] PathFindingModel pathFinding)
        {
            if (Id != pathFinding.CLNCFNDG_ID)
                return BadRequest("SP_TYPE_ID mismatch");

            var result = await _clinicalRepository.UpdatePathFinding(Id, pathFinding);
            if (result == 0)
            {
                return NotFound($"Clinical Finding with Id = {pathFinding.CLNCFNDG_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-clinicalfindingbyid/{Id}")]
        public async Task<ActionResult<int>> DeleteClinicalFindingById(int Id)
        {
            var result = await _clinicalRepository.DeleteClinicalFindingById(Id);
            if (result == 0)
            {
                return NotFound($"Path Axis with Id = {Id} not found");
            }
            return NoContent();
        }


        [HttpGet("GetAllPathFindingsByAxisType/{axisType}")]
        public async Task<ActionResult> GetAllPathFindingsByAxisType(string axisType)
        {
            return Ok(await _clinicalRepository.GetAllPathFindingsByAxisType(axisType));
        }

        [HttpGet("Search-allpathfinding/{query}")]
        public async Task<IActionResult> SearchAllPathFinding(string query)
        {
            // Validate the query
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query parameter cannot be null or empty.");
            }

            var result = await _clinicalRepository.SearchAllPathFinding(query);

            // Check if the result is null or empty
            if (result == null || !result.Any())
            {
                return NotFound($"No records found for the query: {query}");
            }
            return Ok(result);
        }

        #endregion
        /// <summary>
        /// Fetch data by accession number. 
        /// </summary>
        /// <param name="accessionnumber"></param>
        /// <returns></returns>

        [HttpGet("GetAPReceivingByAccessionNumber/{accessionnumber}")]
        public async Task<ActionResult> GetAPReceivingByAccessionNumber(string accessionnumber)
        {
            if (string.IsNullOrWhiteSpace(accessionnumber))
            {
                return BadRequest("Accession number cannot be null or empty.");
            }

            var result = await _clinicalRepository.GetAPReceivingByAccessionNumber(accessionnumber);
            return Ok(result);
        }


        #region Anatomic Pathology Receiving
        [HttpPost("Insert-APReceiving")]
        public async Task<ActionResult> InsertAPReceiving([FromBody] APReceivingModel aPReceiving)
        {
            return Ok(await _clinicalRepository.InsertAPReceiving(aPReceiving));
        }

        [HttpPost("UpdateAPReceiving/{id}")]
        public async Task<ActionResult<int>> UpdatAPReceiving(int id, [FromBody] APReceivingModel aPReceiving)
        {

            // Perform the partial update using the clinical repository
            var result = await _clinicalRepository.UpdateAPReceiving(id, aPReceiving);

            // Check if the update was successful
            if (result == 0)
            {
                return NotFound($"AP Receiving with Id = {aPReceiving.AP_CASES_ID} not found");
            }

            // Return a success response
            return NoContent();
        }

        [HttpGet("GetAllAPReceiving")]
        public async Task<ActionResult> GetAllAPReceiving()
        {
            return Ok(await _clinicalRepository.GetAllAPReceiving());
        }

        #endregion

        #region MicroBiology

        [HttpGet("GetAllMicroBiology")]
        public async Task<ActionResult> GetAllMicroBiology()
        {
            return Ok(await _clinicalRepository.GetAllMicroBiology());
        }


        [HttpGet("GetAllMicroBiologySearch")]
        public async Task<ActionResult> GetAllMicroBiologySearch()
        {
            return Ok(await _clinicalRepository.GetAllMicroBiologySearch());
        }

        [HttpGet("GetMicroBiologyById/{Id}")]
        public async Task<ActionResult<MicroBiologyModel>> GetMicroBiologyById(int Id)
        {
            var result = await _clinicalRepository.GetMicroBiologyById(Id);
            if (result == null)
                return NotFound($"Patient Id = {Id} not found");
            return result;
        }

        [HttpGet("GetRTForMicroBiology")]
        public async Task<ActionResult> GetRTForMicroBiology()
        {
            return Ok(await _clinicalRepository.GetRTForMicroBiology());
        }

        [HttpGet("GetRTForMicroBiologyById/{Id}")]
        public async Task<ActionResult<ResultsTemplatesModel>> GetResultsTemplatesMBById(int Id)
        {
            var result = await _clinicalRepository.GetRTForMicroBiologyById(Id);
            if (result == null)
                return NotFound($"Results Templates with Id = {Id} not found");
            return result;
        }

        [HttpGet("GetMicroBiologyISolByArfId/{Id}")]
        public async Task<ActionResult> GetMicroBiologyISolByArfId(int Id) => Ok(await _clinicalRepository.GetForMicroBiologyISolByArfId(Id));

        [HttpGet("GetForMicroBiologySearchISol/{Search}")]
        public async Task<ActionResult> GetForMicroBiologyAllISol(string Search) => Ok(await _clinicalRepository.GetForMicroBiologySearchISol(Search));

        [HttpGet("GetForMicroBiologyAllISol")]
        public async Task<ActionResult> GetForMicroBiologyAllISol() => Ok(await _clinicalRepository.GetForMicroBiologyAllISol());

        [HttpGet("GetForMicroBiologyARSensitivity")]
        public async Task<ActionResult> GetForMicroBiologyARSensitivity() => Ok(await _clinicalRepository.GetForMicroBiologyARSensitivity());


        [HttpGet("GetForMicroBiologySearchSensitivity/{Search}")]
        public async Task<ActionResult> GetForMicroBiologySearchSensitivity(string Search) => Ok(await _clinicalRepository.GetForMicroBiologySearchSensitivity(Search));

        [HttpGet("GetForMicroBiologyAllSensitivity")]
        public async Task<ActionResult> GetForMicroBiologyAllSensitivity() => Ok(await _clinicalRepository.GetForMicroBiologyAllSensitivity());

        [HttpGet("GetForMicroBiologyAllGTDSensitivity")]
        public async Task<ActionResult> GetForMicroBiologyAllGTDSensitivity() => Ok(await _clinicalRepository.GetForMicroBiologyAllGTDSensitivity());

        [HttpPost("Insert-MicroBiologyISol")]
        public async Task<ActionResult<MBIsolModel>> InsertMicroBiologyISol(List<MBIsolModel> _MBIsolModel)
        {
            try
            {
                var result = await _clinicalRepository.InsertMicoBiologyIsol(_MBIsolModel);
                return await Task.FromResult<ActionResult<MBIsolModel>>(Ok(result));

            }
            catch (Exception ex)
            {
                return await Task.FromResult<ActionResult<MBIsolModel>>(StatusCode(500, "Internal Server Error! Please Contact Admin! " + ex.Message));
            }
        }


        [HttpPost("Insert-MicroBiologySensitivity")]
        public async Task<ActionResult<MBIsolModel>> InsertMicroBiologySensitivity(List<MBSensitivityModel> _MBSensitivityModel)
        {
            try
            {
                var result = await _clinicalRepository.InsertMicroBiologySensitivity(_MBSensitivityModel);
                return await Task.FromResult<ActionResult<MBIsolModel>>(Ok(result));

            }
            catch (Exception ex)
            {
                return await Task.FromResult<ActionResult<MBIsolModel>>(StatusCode(500, "Internal Server Error! Please Contact Admin! " + ex.Message));
            }
        }

        [HttpGet("GetForMicroBiologyAllSensitivityData/{Id}")]
        public async Task<ActionResult> GetForMicroBiologyAllSensitivityData(string Id) => Ok(await _clinicalRepository.GetForMicroBiologyAllSensitivityData(Id));

        // Get All List Microbiology List 
        [HttpPost(("GetAllMicroBiologyList"))]
        public async Task<ActionResult> GetAllMicroBiologyList([FromBody] MicrobiologySearchModel mbListSearch)
        {
            return Ok(await _clinicalRepository.GetAllMicroBiologyList(mbListSearch));
        }


        // Get All List Microbiology List 
        [HttpPost(("GetAllMicroBiologyListQRCode"))]
        public async Task<ActionResult> GetAllMicroBiologyListQRCode([FromBody] MicrobiologySearchModel mbListSearch)
        {
            return Ok(await _clinicalRepository.GetAllMicroBiologyListQRcode(mbListSearch));
        }



        [HttpPost("GetQRcode")]
        public IActionResult GetQR([FromBody] mbListQRModel mbListQRSearch)
        {
            string data = mbListQRSearch.accn.Replace("-", "");
            string b64QRCode = _clinicalRepository.GenerateQR(data);
            var _cls = new Response();
            _cls.messages = b64QRCode;
            _cls.responsecode = 200;
            return Ok(JsonConvert.SerializeObject(_cls));
        }

        #endregion

        #region Cytogenetics

        [HttpGet("GetAllCytogenetics")]
        public async Task<ActionResult> GetAllCytogenetics()
        {
            return Ok(await _clinicalRepository.GetAllCytogenetics());
        }

        [HttpGet("GetAllRTest")]
        public async Task<ActionResult> GetAllRTest()
        {
            return Ok(await _clinicalRepository.GetAllRTest());
        }

        [HttpPost("GetAllTxtNms")]
        public async Task<ActionResult> GetAllTxtNms([FromBody] TxtNameModel txtNameModel)
        {
            var txtNameResult = await _clinicalRepository.getCgTxtNameByRes(txtNameModel);
            if (txtNameResult.ToList().Count == 0)
                txtNameResult = await _clinicalRepository.getCgRTxtName(txtNameModel);
            return Ok(txtNameResult);
        }

        [HttpPost("Insert-Cytogenetics_Txt_Res")]
        public async Task<ActionResult<TxtNameModel>> InsertCytogeneticsTxtRes(List<TxtNameModel> _txtNameModel)
        {
            try
            {
                var result = await _clinicalRepository.InsertCytogeneticsTxtRes(_txtNameModel);
                return await Task.FromResult<ActionResult<TxtNameModel>>(Ok(result));

            }
            catch (Exception ex)
            {
                return await Task.FromResult<ActionResult<TxtNameModel>>(StatusCode(500, "Internal Server Error! Please Contact Admin! " + ex.Message));
            }
        }

        [HttpPost("GetAllCgTxtNameByRes")]
        public async Task<ActionResult> GetAllCgTxtNameByRes([FromBody] TxtNameModel txtNameModel)
        {
            return Ok(await _clinicalRepository.getCgTxtNameByRes(txtNameModel));
        }

        // Get All List Cytogenetics List 
        [HttpPost(("GetAllCytogeneticsList"))]
        public async Task<ActionResult> GetAllCytogeneticsList([FromBody] CytogeneticSearchModel cgListSearch)
        {
            return Ok(await _clinicalRepository.GetAllCytogeneticList(cgListSearch));
        }


        [HttpGet("GetCytogeneticById/{Id}")]
        public async Task<ActionResult<CytogeneticsModel>> GetCytogeneticById(int Id)
        {
            var result = await _clinicalRepository.GetCytogeneticsById(Id);
            if (result == null)
                return NotFound($"Patient Id = {Id} not found");
            return result;
        }

        [HttpGet("GetCytogeneticLoginAR")]
        public async Task<ActionResult> GetCytogeneticLoginAR()
        {
            return Ok(await _clinicalRepository.GetCytogeneticLoginAR());
        }

        [HttpPost("GetCytogeneticLogin")]
        public async Task<ActionResult> GetCytogeneticLogin([FromBody] CytogeneticLoginModel cgLoginModel)
        {
            return Ok(await _clinicalRepository.GetCytogeneticLogin(cgLoginModel));
        }

        [HttpPatch("UpdateCytogeneticLogin")]
        public async Task<ActionResult<int>> UpdateCytogeneticLogin([FromBody] CytogeneticLoginModel cgLoginModel)
        {

            // Perform the partial update using the clinical repository
            var result = await _clinicalRepository.UpdateCytogeneticLogin(cgLoginModel);

            // Check if the update was successful
            if (result == 0)
            {
                return NotFound($"Cytogenetic Model with ACCN = {cgLoginModel.ACCN} not found");
            }

            // Return a success response
            return NoContent();
        }

        #endregion

        #region Environment Result

        [HttpGet("GetAllEnvironmentalResult/{pSize}")]
        public async Task<ActionResult> GetAllEnvironmentalResult(string pSize)
        {
            return Ok(await _clinicalRepository.GetAllEnvironmentalResult(pSize));
        }

     

        [HttpGet("GetAllEnvironmentalARFResult/{accn}")]
        public async Task<ActionResult> GetAllEnvironmentalARFResult(string accn)
        {
            return Ok(await _clinicalRepository.GetAllEnvironmentalArfResult(accn));
        }


        [HttpPost("InsertEnvironmentalResult")]
        public async Task<ActionResult<EVResultARFModel>> InsertEnvironmentalResult(List<EVResultARFModel> evResultModel)
        {
            try
            {
                var result = await _clinicalRepository.InsertEnvironmentalResult(evResultModel);
                return await Task.FromResult<ActionResult<EVResultARFModel>>(Ok(result));

            }
            catch (Exception ex)
            {
                return await Task.FromResult<ActionResult<EVResultARFModel>>(StatusCode(500, "Internal Server Error! Please Contact Admin! " + ex.Message));
            }
        }


        [HttpPost("UpdateEVResultInstrument")]
        public async Task<ActionResult<EVResultARFModel>> UpdateEVResultInstrument(List<EVResultARFModel> evResultModel)
        {
            try
            {
                var result = await _clinicalRepository.UpdateEnvironmentalResultInstrument(evResultModel);
                return await Task.FromResult<ActionResult<EVResultARFModel>>(Ok(result));

            }
            catch (Exception ex)
            {
                return await Task.FromResult<ActionResult<EVResultARFModel>>(StatusCode(500, "Internal Server Error! Please Contact Admin! " + ex.Message));
            }
        }


        [HttpPatch("UpdateevResult/{accn}")]
        public async Task<ActionResult<int>> UpdateEVResultStatus(string accn, [FromBody] EVResultStatusModel evResultModel)
        {
            // Validate the Id and partialModel
            if (accn != evResultModel.ACCN)
            {
                return BadRequest("ACCN mismatch");
            }

            // Perform the partial update using the clinical repository
            var result = await _clinicalRepository.UpdateEVRResultStatus(accn, evResultModel);

            // Check if the update was successful
            if (result == 0)
            {
                return NotFound($"Environmental Result Model with accn = {accn} not found");
            }

            // Return a success response
            return NoContent();
        }


        [HttpPatch("ResetEVRefRange/{accn}")]
        public async Task<ActionResult<int>> ResetEVRefRange(string accn, [FromBody] EVResultStatusModel evResultModel)
        {
            // Validate the Id and partialModel
            if (accn != evResultModel.ACCN)
            {
                return BadRequest("ACCN mismatch");
            }

            // Perform the partial update using the clinical repository
            var result = await _clinicalRepository.ResetEVRefRange(accn, evResultModel);

            // Check if the update was successful
            if (result == 0)
            {
                return NotFound($"Environmental Result Model with accn = {accn} not found");
            }

            // Return a success response
            return NoContent();
        }


        [HttpGet("GetAllEnvironmentalSearch/{OrderNo}")]
        public async Task<ActionResult> GetAllEnvironmentalSearch(string OrderNo)
        {
            return Ok(await _clinicalRepository.GetAllEnvironmentalSearch(OrderNo));
        }

        [HttpPost("GetEnvironmentalDetails")]
        public async Task<ActionResult<EVDetailModel>> GetEnvironmentalDetails([FromBody] EvResultDetailModel evResultModel)
        {
            return Ok(await _clinicalRepository.GetEnvironmentalDetails(evResultModel));
        }

        [HttpGet("GetAllInstruments")]
        public async Task<ActionResult> GetAllInstruments()
        {
            return Ok(await _clinicalRepository.GetAllInstruments());
        }

        #endregion

        #region Clinical Image

        [HttpPost("Insert-ClinicalImage")]
        public async Task<IActionResult> UploadFile(ClinicalImageModel obj)
        {
                var status = await _clinicalRepository.InsertClinicalImage(obj);
                return Ok(new { Message = "File uploaded successfully." });
        }

        [HttpPost("GetAllClinicalImages")]
        public async Task<ActionResult> GetAllClinicalImages([FromBody] ClinicalImageModel clinicalImageModel)
        {
            return Ok(await _clinicalRepository.GetClinincalImages(clinicalImageModel));
        }

        [HttpDelete("Delete-clinicalImagebyid/{Id}")]
        public async Task<ActionResult<int>> DeleteClinicalImagebyId(int Id)
        {
            var result = await _clinicalRepository.DeleteClinicalImageById(Id);
            if (result == 0)
            {
                return NotFound($"clinical Image with Id = {Id} not found");
            }
            return NoContent();
        }
        #endregion

    }
}
