using DeltaCare.BAL;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;

namespace DeltaCare.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : DeltaBaseController
    {
        private readonly IMasterRepository _masterRepository;

        public MasterDataController(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        [HttpPost("Insert-specimentypes")]
        public async Task<ActionResult<int>> InsertSpecimentypes([FromBody] SpecimentypeModel specimentype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertSpecimentypes(specimentype));
        }

        [HttpPut("Update-specimentypes/{Id}")]
        public async Task<ActionResult<int>> UpdateSpecimentypes(int Id, [FromBody] SpecimentypeModel specimentype)
        {
            if (Id != specimentype.SP_TYPE_ID)
                return BadRequest("SP_TYPE_ID mismatch");

            var result = await _masterRepository.UpdateSpecimentypes(Id, specimentype);
            if (result == 0)
            {
                return NotFound($"Specimentype with Id = {specimentype.SP_TYPE_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("delete-specimentypes/{Id}")]
        public async Task<ActionResult<int>> DeleteSpecimentypes(int Id)
        {
            var result = await _masterRepository.DeleteSpecimentypes(Id);
            if (result == 0)
            {
                return NotFound($"Specimentype with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllSpecimentypes")]
        public async Task<ActionResult> GetAllSpecimentypes()
        {
            return Ok(await _masterRepository.GetAllSpecimentypes());
        }

        [HttpGet("GetSpecimentypesById/{Id}")]
        public async Task<ActionResult<SpecimentypeModel>> GetSpecimentypesById(int Id)
        {
            var result = await _masterRepository.GetSpecimentypesById(Id);
            if (result == null)
                return NotFound($"Specimentype Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-division")]
        public async Task<ActionResult<int>> InsertDivision([FromBody] DivisionModel division)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _masterRepository.InsertDivision(division));
        }

        [HttpPut("Update-division/{Id}")]
        public async Task<ActionResult<int>> UpdateDivision(int Id, [FromBody] DivisionModel division)
        {
            if (Id != division.LAB_DIV_ID)
                return BadRequest("DIV_ID mismatch");

            var result = await _masterRepository.UpdateDivision(Id, division);
            if (result == 0)
            {
                return NotFound($"Division with Id = {division.LAB_DIV_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-Divsion/{Id}")]
        public async Task<ActionResult<int>> DeleteDivision(int Id)
        {
            var result = await _masterRepository.DeleteDivision(Id);
            if (result == 0)
            {
                return NotFound($"Division with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllDivision")]
        public async Task<ActionResult> GetAllDivision()
        {
            return Ok(await _masterRepository.GetAllDivision());
        }

        [HttpGet("GetDivisionById/{Id}")]
        public async Task<ActionResult<DivisionModel>> GetDivisionById(int Id)
        {
            var result = await _masterRepository.GetDivisionById(Id);
            if (result == null)
                return NotFound($"Division with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-section")]
        public async Task<ActionResult<int>> InsertSection([FromBody] SectionModel section)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertSection(section));
        }

        [HttpPut("Update-section/{Id}")]
        public async Task<ActionResult<int>> UpdateSection(int Id, [FromBody] SectionModel section)
        {
            if (Id != section.LAB_SECT_ID)
                return BadRequest("LAB_SECT_ID mismatch");

            var result = (await _masterRepository.UpdateSection(Id, section));
            if (result == 0)
            {
                return NotFound($"Section with Id = {section.LAB_SECT_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-Section/{Id}")]
        public async Task<ActionResult<int>> DeleteSection(int Id)
        {
            var result = (await _masterRepository.DeleteSection(Id));
            if (result == 0)
            {
                return NotFound($"Section with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllSection")]
        public async Task<ActionResult> GetAllSection()
        {
            return Ok(await _masterRepository.GetAllSection());
        }

        [HttpGet("GetSectionById/{Id}")]
        public async Task<ActionResult<SectionModel>> GetSectionById(int Id)
        {
            var result = await _masterRepository.GetSectionById(Id);
            if (result == null)
                return NotFound($"Section with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-WorkCenter")]
        public async Task<ActionResult<int>> InsertWorkCenter([FromBody] WorkCenterModel workCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertWorkCenter(workCenter));
        }

        [HttpPut("Update-WorkCenter/{Id}")]
        public async Task<ActionResult<int>> UpdateWorkCenter(int Id, [FromBody] WorkCenterModel workCenter)
        {
            if (Id != workCenter.LAB_WC_ID)
                return BadRequest("WC_ID mismatch");

            var result = (await _masterRepository.UpdateWorkCenter(Id, workCenter));
            if (result == 0)
            {
                return NotFound($"WorkCenter with Id = {workCenter.LAB_WC_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-WorkCenter/{Id}")]
        public async Task<ActionResult<int>> DeleteWorkCenter(int Id)
        {
            var result = (await _masterRepository.DeleteWorkCenter(Id));
            if (result == 0)
            {
                return NotFound($"WorkCenter with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllWorkCenter")]
        public async Task<ActionResult> GetAllWorkCenter()
        {
            return Ok(await _masterRepository.GetAllWorkCenter());
        }

        [HttpGet("GetWorkCenterById/{Id}")]
        public async Task<ActionResult<WorkCenterModel>> GetWorkCenterById(int Id)
        {
            var result = await _masterRepository.GetWorkCenterById(Id);
            if (result == null)
                return NotFound($"WorkCenter with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-TestSite")]
        public async Task<ActionResult<int>> InsertTestSite([FromBody] TestSiteModel testSite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertTestSite(testSite));
        }

        [HttpPut("Update-TestSite/{Id}")]
        public async Task<ActionResult<int>> UpdateTestSite(int Id, [FromBody] TestSiteModel testSite)
        {
            if (Id != testSite.LAB_TS_ID)
                return BadRequest("TS_ID mismatch");

            var result = (await _masterRepository.UpdateTestSite(Id, testSite));
            if (result == 0)
            {
                return NotFound($"TestSite with Id = {testSite.LAB_TS_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-TestSite/{Id}")]
        public async Task<ActionResult<int>> DeleteTestSite(int Id)
        {
            var result = (await _masterRepository.DeleteTestSite(Id));
            if (result == 0)
            {
                return NotFound($"TestSite with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllTestSite")]
        public async Task<ActionResult> GetAllTestSite()
        {
            return Ok(await _masterRepository.GetAllTestSite());
        }

        [HttpGet("GetTestSiteById/{Id}")]
        public async Task<ActionResult<TestSiteModel>> GetTestSiteById(int Id)
        {
            var result = await _masterRepository.GetTestSiteById(Id);
            if (result == null)
                return NotFound($"TestSite with Id - {Id} not found");
            return result;
        }

        [HttpPost("Insert-accnprefix")]
        public async Task<ActionResult<int>> InsertAccnPrefix([FromBody] AccnPrefixModel accnPrefix)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertAccnPrefix(accnPrefix));
        }

        [HttpPut("Update-accnprefix/{Id}")]
        public async Task<ActionResult<int>> Updateaccnprefix(int Id, [FromBody] AccnPrefixModel accnPrefix)
        {
            if (Id != accnPrefix.ACCNPRFX_ID)
                return BadRequest("ACCNPRFX_ID mismatch");

            var result = (await _masterRepository.UpdateAccnPrefix(Id, accnPrefix));
            if (result == 0)
            {
                return NotFound($"AccnPrefix with Id = {accnPrefix.ACCNPRFX_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-accnprefix/{Id}")]
        public async Task<ActionResult<int>> DeleteAccnprefix(int Id)
        {
            var result = (await _masterRepository.DeleteAccnPrefix(Id));
            if (result == 0)
            {
                return NotFound($"AccnPrefix with Id = {Id} not found");
            }
            return NoContent();
        }
        //    
        [HttpGet("GetAllAccnPrefix")]
        public async Task<ActionResult> GetAllAccnPrefix()
        {
            return this.Ok(await _masterRepository.GetAllAccnPrefix());
        }

        [HttpGet("GetAccnPrefixById/{Id}")]
        public async Task<ActionResult<AccnPrefixModel>> GetAccnPrefixById(int Id)
        {
            var result = await _masterRepository.GetAccnPrefixById(Id);
            if (result == null)
                return NotFound($"AccnPrefix with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-resultstemplates")]
        public async Task<ActionResult<int>> InsertResultsTemplates([FromBody] ResultsTemplatesModel resultsTemplates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertResultsTemplates(resultsTemplates));
        }

        [HttpPut("Update-resultstemplates/{Id}")]
        public async Task<ActionResult<int>> UpdateResultsTemplates(int Id, [FromBody] ResultsTemplatesModel resultsTemplates)
        {
            if (Id != resultsTemplates.RS_TMPLT_ID)
                return BadRequest("RS_TMPLT_ID mismatch");

            var result = (await _masterRepository.UpdateResultsTemplates(Id, resultsTemplates));
            if (result == 0)
            {
                return NotFound($"ResultsTemplates with Id = {resultsTemplates.TNO} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-resultstemplates/{Id}")]
        public async Task<ActionResult<int>> DeleteResultsTemplates(int Id)
        {
            var result = (await _masterRepository.DeleteResultsTemplates(Id));
            if (result == 0)
            {
                return NotFound($"ResultsTemplates with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllResultsTemplates")]
        public async Task<ActionResult> GetAllResultsTemplates()
        {
            return Ok(await _masterRepository.GetAllResultsTemplates());
        }

        [HttpGet("GetResultsTemplatesById/{Id}")]
        public async Task<ActionResult<ResultsTemplatesModel>> GetResultsTemplatesById(int Id)
        {
            var result = await _masterRepository.GetResultsTemplatesById(Id);
            if (result == null)
                return NotFound($"Results Templates with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-reportmainheader")]
        public async Task<ActionResult<int>> InsertReportMainHeader([FromBody] ReportMainHeaderModel reportMainHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertReportMainHeader(reportMainHeader));
        }

        [HttpPut("Update-reportmainheader/{Id}")]
        public async Task<ActionResult<int>> UpdateReportMainHeader(int Id, [FromBody] ReportMainHeaderModel reportMainHeader)
        {
            if (Id != reportMainHeader.RPT_MHDR_ID)
                return BadRequest("RPT_MHDR_ID mismatch");

            var result = await _masterRepository.UpdateReportMainHeader(Id, reportMainHeader);
            if (result == 0)
            {
                return NotFound($"Report Main Header with Id = {reportMainHeader.RPT_MHDR_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-reportmainheader/{Id}")]
        public async Task<ActionResult<int>> DeleteReportMainHeader(int Id)
        {
            var result = await _masterRepository.DeleteReportMainHeader(Id);
            if (result == 0)
            {
                return NotFound($"Report Main Header with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllReportMainHeader")]
        public async Task<ActionResult> GetAllReportMainHeader()
        {
            return Ok(await _masterRepository.GetAllReportMainHeader());
        }

        [HttpGet("GetReportMainHeaderById/{Id}")]
        public async Task<ActionResult<ReportMainHeaderModel>> GetReportMainHeaderById(int Id)
        {
            var result = await _masterRepository.GetReportMainHeaderById(Id);
            if (result == null)
                return NotFound("Report Main Header not found");
            return result;
        }

        [HttpPost("Insert-reportsubheader")]
        public async Task<ActionResult<int>> InsertReportSubHeader([FromBody] ReportSubHeaderModel reportSubHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertReportSubHeader(reportSubHeader));
        }

        [HttpPut("Update-reportsubheader/{Id}")]
        public async Task<ActionResult<int>> UpdateReportSubHeader(int Id, [FromBody] ReportSubHeaderModel reportSubHeader)
        {
            if (Id != reportSubHeader.RPT_SHDR_ID)
                return BadRequest("RPT_SHDR_ID mismatch");

            var result = await _masterRepository.UpdateReportSubHeader(Id, reportSubHeader);
            if (result == 0)
            {
                return NotFound($"Report Sub Header with Id = {reportSubHeader.RPT_SHDR_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-reportsubheader/{Id}")]
        public async Task<ActionResult<int>> DeleteReportSubHeader(int Id)
        {
            var result = await _masterRepository.DeleteReportSubHeader(Id);
            if (result == 0)
            {
                return NotFound($"Report Main Header with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllReportSubHeader")]
        public async Task<ActionResult> GetAllReportSubHeader()
        {
            return Ok(await _masterRepository.GetAllReportSubHeader());
        }

        [HttpGet("GetReportSubHeaderById/{Id}")]
        public async Task<ActionResult<ReportSubHeaderModel>> GetReportSubHeaderById(int Id)
        {
            var result = await _masterRepository.GetReportSubHeaderById(Id);
            if (result == null)
                return NotFound($"Report Sub Header with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-accountmanager")]
        public async Task<ActionResult<int>> InsertAccountManager(AccountManagerModel accountManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertAccountManager(accountManager));
        }

        [HttpPut("Update-accountmanager/{Id}")]
        public async Task<ActionResult<int>> UpdateAccountManager(int Id, AccountManagerModel accountManager)
        {
            if (Id != accountManager.SALESMEN_ID)
                return BadRequest("Account Manager ID mismatch");

            var result = (await _masterRepository.UpdateAccountManager(Id, accountManager));
            if (result == 0)
            {
                return NotFound($"Account Manager with Id = {accountManager.SALESMEN_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-accountmanager/{Id}")]
        public async Task<ActionResult<int>> DeleteAccountManager(int Id)
        {
            var result = (await _masterRepository.DeleteAccountManager(Id));
            if (result == 0)
            {
                return NotFound($"Account Manager with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllAccountManager")]
        public async Task<ActionResult> GetAllAccountManager()
        {
            return this.Ok(await _masterRepository.GetAllAccountManager());
        }

        [HttpGet("GetAccountManagerById/{Id}")]
        public async Task<ActionResult<AccountManagerModel>> GetAccountManagerById(int Id)
        {
            var result = await _masterRepository.GetAccountManagerById(Id);
            if (result == null)
                return NotFound($"Account Manager with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-driver")]
        public async Task<ActionResult<int>> InsertDriver(DriverModel driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertDriver(driver));
        }

        [HttpPut("Update-driver/{Id}")]
        public async Task<ActionResult<int>> UpdateDriver(int Id, DriverModel driver)
        {
            if (Id != driver.DRVRS_ID)
                return BadRequest("Driver ID mismatch");

            var result = (await _masterRepository.UpdateDriver(Id, driver));
            if (result == 0)
            {
                return NotFound($"Driver with Id = {driver.DRVRS_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-driver/{Id}")]
        public async Task<ActionResult<int>> DeleteDriver(int Id)
        {
            var result = (await _masterRepository.DeleteDriver(Id));
            if (result == 0)
            {
                return NotFound($"Driver with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllDriver")]
        public async Task<ActionResult> GetAllDriver()
        {
            return this.Ok(await _masterRepository.GetAllDriver());
        }

        [HttpGet("GetDriverById/{Id}")]
        public async Task<ActionResult<DriverModel>> GetDriverById(int Id)
        {
            var result = await _masterRepository.GetDriverById(Id);
            if (result == null)
                return NotFound($"Driver with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-site")]
        public async Task<ActionResult<int>> InsertSite(SiteModel site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertSite(site));
        }

        [HttpPut("Update-site/{Id}")]
        public async Task<ActionResult<int>> UpdateSite(int Id, SiteModel site)
        {
            if (Id != site.SITE_DTL_ID)
                return BadRequest("Site ID mismatch");

            var result = (await _masterRepository.UpdateSite(Id, site));
            if (result == 0)
            {
                return NotFound($"Site with Id = {site.SITE_DTL_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-site/{Id}")]
        public async Task<ActionResult<int>> DeleteSite(int Id)
        {
            var result = (await _masterRepository.DeleteSite(Id));
            if (result == 0)
            {
                return NotFound($"Site with Id = {Id} not found");
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("GetAllSite")]
        public async Task<ActionResult> GetAllSite()
        {
            return this.Ok(await _masterRepository.GetAllSite());
        }

        [AllowAnonymous]
        [HttpGet("GetSiteBySiteTP")]
        public async Task<ActionResult> GetSiteBySiteTP()
        {
            return this.Ok(await _masterRepository.GetSiteBySiteTP());
        }

        [HttpGet("GetSiteById/{Id}")]
        public async Task<ActionResult<SiteModel>> GetSiteById(int Id)
        {
            var result = await _masterRepository.GetSiteById(Id);
            if (result == null)
                return NotFound($"Site with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-specimenSite")]
        public async Task<ActionResult<int>> InsertSpecimenSite([FromBody] SpecimenSiteModel specimenSite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertSpecimenSite(specimenSite));
        }

        [HttpPut("Update-specimenSite/{Id}")]
        public async Task<ActionResult<int>> UpdateSpecimenSite(int Id, [FromBody] SpecimenSiteModel specimenSite)
        {
            if (Id != specimenSite.SP_SITE_ID)
                return BadRequest("SP_SITE_ID mismatch");

            var result = (await _masterRepository.UpdateSpecimenSite(Id, specimenSite));
            if (result == 0)
            {
                return NotFound($"SPSite with Id = {specimenSite.SP_SITE_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-specimenSite/{Id}")]
        public async Task<ActionResult<int>> DeleteSpecimenSite(int Id)
        {
            var result = (await _masterRepository.DeleteSpecimenSite(Id));
            if (result == 0)
            {
                return NotFound($"SPSite with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllSpecimenSite")]
        public async Task<ActionResult> GetAllSpecimenSite()
        {
            return this.Ok(await _masterRepository.GetAllSpecimenSite());
        }

        [HttpGet("GetSpecimenSiteById/{Id}")]
        public async Task<ActionResult<SpecimenSiteModel>> GetSpecimenSiteById(int Id)
        {
            var result = await _masterRepository.GetSpecimenSiteById(Id);
            if (result == null)
                return NotFound($"SPSite with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-phstaff")]
        public async Task<ActionResult<int>> InsertPHStaff([FromBody] PHStaffModel staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertPHStaff(staff));
        }

        [HttpPut("Update-phstaff/{Id}")]
        public async Task<ActionResult<int>> UpdatePHStaff(int Id, [FromBody] PHStaffModel staff)
        {
            if (Id != staff.PH_ID)
                return BadRequest("PH_ID mismatch");

            var result = (await _masterRepository.UpdatePHStaff(Id, staff));
            if (result == 0)
            {
                return NotFound($"PHStaff with Id = {staff.PH_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-phstaff/{Id}")]
        public async Task<ActionResult<int>> DeletePHStaff(int Id)
        {
            var result = (await _masterRepository.DeletePHStaff(Id));
            if (result == 0)
            {
                return NotFound($"PHStaff with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllPHStaff")]
        public async Task<ActionResult> GetAllPHStaff()
        {
            return this.Ok(await _masterRepository.GetAllPHStaff());
        }

        [HttpGet("GetPHStaffById/{Id}")]
        public async Task<ActionResult<PHStaffModel>> GetPHStaffById(int Id)
        {
            var result = await _masterRepository.GetPHStaffById(Id);
            if (result == null)
                return NotFound($"PHStaff with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-resulttype")]
        public async Task<ActionResult<int>> InsertResultType([FromBody] ResultTypeModel resultType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertResultType(resultType));
        }

        [HttpPut("Update-resulttype/{Id}")]
        public async Task<ActionResult<int>> UpdateResultType(int Id, [FromBody] ResultTypeModel resultType)
        {
            if (Id != resultType.RESTYPE_ID)
                return BadRequest("RESTYPE_ID mismatch");

            var result = (await _masterRepository.UpdateResultType(Id, resultType));
            if (result == 0)
            {
                return NotFound($"ResultType with Id = {resultType.RESTYPE_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-resulttype/{Id}")]
        public async Task<ActionResult<int>> DeleteResultType(int Id)
        {
            var result = (await _masterRepository.DeleteResultType(Id));
            if (result == 0)
            {
                return NotFound($"ResultType with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllResultType")]
        public async Task<ActionResult> GetAllResultType()
        {
            return this.Ok(await _masterRepository.GetAllResultType());
        }

        [HttpGet("GetResultTypeById/{Id}")]
        public async Task<ActionResult<ResultTypeModel>> GetResultTypeById(int Id)
        {
            var result = await _masterRepository.GetResultTypeById(Id);
            if (result == null)
                return NotFound($"ResultType with Id = {Id} not found");
            return result;
        }

        [HttpPost("Insert-client")]
        public async Task<ActionResult<int>> InsertClient([FromBody] ClientModel client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertClient(client));
        }

        [HttpPut("Update-client/{Id}")]
        public async Task<ActionResult<int>> UpdateClient(int Id, [FromBody] ClientModel client)
        {
            if (Id != client.CLNT_FL_ID)
                return BadRequest("CLNT_FL_ID mismatch");

            var result = await _masterRepository.UpdateClient(Id, client);
            if (result == 0)
            {
                return NotFound($"Client with Id = {client.CLNT_FL_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("delete-client/{Id}")]
        public async Task<ActionResult<int>> DeleteClient(int Id)
        {
            var result = await _masterRepository.DeleteClient(Id);
            if (result == 0)
            {
                return NotFound($"Client with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllClients")]
        public async Task<ActionResult> GetAllClients()
        {
            var clients = await _masterRepository.GetAllClients(); // Fetch all clients

            // Get the max CN value (ensure CN is numeric)
            int maxCN = clients.Any() ? clients.Max(c => int.TryParse(c.CN, out var cn) ? cn : 0) + 1 : 1;

            return Ok(new
            {
                Clients = clients,
                MaxCN = maxCN // Send max CN as part of the response
            });
        }


        [HttpGet("GetClientById/{Id}")]
        public async Task<ActionResult<ClientModel>> GetClientById(int Id)
        {
            var result = await _masterRepository.GetClientById(Id);
            if (result == null)
                return NotFound($"Client Id = {Id} not found");
            return result;
        }

        [HttpGet("GetClientByCN/{CN}")]
        public async Task<IActionResult> GetClientByCN(string CN)//GET_CLNT_FL
        {
            var GET_CLNT_FL = await _masterRepository.GetClientByCN(CN);
            //if (GET_CLNT_FL == null)
            //   return NotFound(new { Message = $"Client CN = {CN} not found" });
            return Ok(new
            {
                GET_CLNT_FL
            });
        }


        [HttpPost("Insert-SpecialPricesFromSourceToDestination")]
        public async Task<ActionResult<int>> InsertSpecialPricesFromSourceToDestination([FromBody] List<SpecialPricesModel> specialPricesList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertSpecialPricesFromSourceToDestination(specialPricesList));
        }

        [HttpDelete("delete-specialprices/{Id}")]
        public async Task<ActionResult<int>> DeleteSpecialPrices(int Id)
        {
            var result = await _masterRepository.DeleteSpecialPrices(Id);
            if (result == 0)
            {
                return NotFound($"SpecialPrices with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllTests")]
        public async Task<ActionResult> GetAllTests()
        {
            return Ok(await _masterRepository.GetAllTests());
        }

        [HttpGet("GetSpecialPricesById/{Id}")]
        public async Task<ActionResult<SpecialPricesModel>> GetSpecialPricesByCode(int? Id, [FromQuery] string code)
        {
            var result = await _masterRepository.GetSpecialPricesByCode(Id, code);
            if (result == null)
                return NotFound($"SpecialPrices Id = {Id} not found");

            return result;
        }

        [HttpGet("GetSpecialPricesByParams/{cn}/{tcode}")]//GET_CLNT_SP
        public async Task<ActionResult<CLNT_SPModel>> GetSpecialPricesByParams(string cn, string tcode)
        {
            var result = await _masterRepository.GetSpecialPricesByParams(cn, tcode);
            if (result == null)
                return NotFound($"SpecialPrices tcode = {tcode} not found");

            return result;
        }


        //AR APIS
        [HttpPost("Insert-AR")]
        public async Task<ActionResult<int>> InsertAR([FromBody] ARTemplateModel aRTemplate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _masterRepository.InsertAR(aRTemplate));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("Update-AR/{Id}")]
        public async Task<ActionResult<int>> UpdateAR(int Id, [FromBody] ARTemplateModel aRTemplate)
        {
            try
            {
                if (Id != aRTemplate.AR_ID)
                    return BadRequest("AR_ID mismatch");

                var result = await _masterRepository.UpdateAR(aRTemplate);

                if (result == 0)
                {
                    return NotFound($"AR with Id = {aRTemplate.AR_ID} not found");
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete-AR/{AR_ID}")]
        public async Task<ActionResult<int>> DeleteAR(int AR_ID)
        {
            try
            {
                var result = await _masterRepository.DeleteAR(AR_ID);
                if (result == 0)
                {
                    return NotFound($"AR with Id = {AR_ID} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetAllAR")]
        public async Task<ActionResult> GetAllAR()
        {
            try
            {
                return Ok(await _masterRepository.GetAllAR());

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        [HttpGet("GetARById/{Id}")]
        public async Task<ActionResult<ARTemplateModel>> GetARById(int Id)
        {
            try
            {
                var result = await _masterRepository.GetARById(Id);

                if (result == null)
                    return NotFound("AR_Id not found");

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //CNLCD APIS
        [HttpPost("Insert-CNLCD")]
        public async Task<ActionResult<int>> InsertCNLCD([FromBody] CNLCDModel CNLCDTemplate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _masterRepository.InsertCNLCD(CNLCDTemplate));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("Update-CNLCD/{Id}")]
        public async Task<ActionResult<int>> UpdateCNLCD(int Id, [FromBody] CNLCDModel CNLCDTemplate)
        {
            try
            {
                if (Id != CNLCDTemplate.CNLCD_ID)
                    return BadRequest("CNLCD_ID mismatch");

                var result = await _masterRepository.UpdateCNLCD(CNLCDTemplate);

                if (result == 0)
                {
                    return NotFound($"CNLCD with Id = {CNLCDTemplate.CNLCD_ID} not found");
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete-CNLCD/{CNLCD_ID}")]
        public async Task<ActionResult<int>> DeleteCNLCD(int CNLCD_ID)
        {
            try
            {
                var result = await _masterRepository.DeleteCNLCD(CNLCD_ID);
                if (result == 0)
                {
                    return NotFound($"CNLCD with Id = {CNLCD_ID} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetAllCNLCD")]
        public async Task<ActionResult> GetAllCNLCD()//GET_CNLCD
        {
            try
            {
                return Ok(await _masterRepository.GetAllCNLCD());

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        [HttpGet("GetCNLCDById/{Id}")]
        public async Task<ActionResult<CNLCDModel>> GetCNLCDById(int Id)
        {
            try
            {
                var result = await _masterRepository.GetCNLCDById(Id);

                if (result == null)
                    return NotFound("CNLCD_Id not found");

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //MN APIS
        [HttpPost("Insert-MN")]
        public async Task<ActionResult<int>> InsertMN([FromBody] MNModel mN)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _masterRepository.InsertMN(mN));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("Update-MN/{Id}")]
        public async Task<ActionResult<int>> UpdateMN(int Id, [FromBody] MNModel mN)
        {
            try
            {
                if (Id != mN.MN_ID)
                    return BadRequest("MN_ID mismatch");

                var result = await _masterRepository.UpdateMN(mN);

                if (result == 0)
                {
                    return NotFound($"MN with Id = {mN.MN_ID} not found");
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete-MN/{MN_ID}")]
        public async Task<ActionResult<int>> DeleteMN(int MN_ID)
        {
            try
            {
                var result = await _masterRepository.DeleteMN(MN_ID);
                if (result == 0)
                {
                    return NotFound($"MN with Id = {MN_ID} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetAllMN")]
        public async Task<ActionResult> GetAllMN()
        {
            try
            {
                return Ok(await _masterRepository.GetAllMN());

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        [HttpGet("GetMNById/{Id}")]
        public async Task<ActionResult<MNModel>> GetMNById(int Id)
        {
            try
            {
                var result = await _masterRepository.GetMNById(Id);

                if (result == null)
                    return NotFound("MN_Id not found");

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //MNI APIS
        [HttpPost("Insert-MNI")]
        public async Task<ActionResult<int>> InsertMNI([FromBody] MNIModel MNI)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _masterRepository.InsertMNI(MNI));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("Update-MNI/{Id}")]
        public async Task<ActionResult<int>> UpdateMNI(int Id, [FromBody] MNIModel MNI)
        {
            try
            {
                if (Id != MNI.MNI_ID)
                    return BadRequest("MNI_ID mismatch");

                var result = await _masterRepository.UpdateMNI(MNI);

                if (result == 0)
                {
                    return NotFound($"MNI with Id = {MNI.MNI_ID} not found");
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("Delete-MNI/{MNI_ID}")]
        public async Task<ActionResult<int>> DeleteMNI(int MNI_ID)
        {
            try
            {
                var result = await _masterRepository.DeleteMNI(MNI_ID);
                if (result == 0)
                {
                    return NotFound($"MNI with Id = {MNI_ID} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetAllMNI")]
        public async Task<ActionResult> GetAllMNI()
        {
            try
            {
                return Ok(await _masterRepository.GetAllMNI());

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        [HttpGet("GetMNIById/{Id}")]
        public async Task<ActionResult<MNIModel>> GetMNIById(int Id)
        {
            try
            {
                var result = await _masterRepository.GetMNIById(Id);

                if (result == null)
                    return NotFound("MNI_Id not found");

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetSpecialpricesByClient/{FromCN}/{ToCN}")]
        public async Task<ActionResult<SpecialPricesModel>> GetSpecialpricesByClient(string FromCN, string ToCN)
        {
            try
            {
                return Ok(await _masterRepository.GetSpecialpricesByClient(FromCN, ToCN));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetAllCompany")]
        public async Task<ActionResult> GetAllCompany()
        {
            return Ok(await _masterRepository.GetAllCompany());
        }


        [HttpPost("Insert-evsampletest")]
        public async Task<ActionResult<int>> InsertEvsampletest([FromBody] EVSampleTestModel evsampletest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _masterRepository.InsertEvsampletest(evsampletest));
        }

        [HttpPut("Update-evsampletest/{Id}")]
        public async Task<ActionResult<int>> UpdateEvsampletest(int Id, [FromBody] EVSampleTestModel evsampletest)
        {
            if (Id != evsampletest.EV_SMPLS_ID)
                return BadRequest("SP_TYPE_ID mismatch");

            var result = await _masterRepository.UpdateEvsampletest(Id, evsampletest);
            if (result == 0)
            {
                return NotFound($"evsampletest with Id = {evsampletest.EV_SMPLS_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("delete-evsampletest/{Id}")]
        public async Task<ActionResult<int>> DeleteEvsampletest(int Id)
        {
            var result = await _masterRepository.DeleteEvsampletest(Id);
            if (result == 0)
            {
                return NotFound($"evsampletest with Id = {Id} not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllEVSampletest")]
        public async Task<ActionResult> GetAllEVSampletest()
        {
            return Ok(await _masterRepository.GetAllEVSampletest());
        }

        #region Order Entry References
        [HttpGet("GetReferenceRanges/{TCODE}/{RSTP}/{SEX}")]
        public async Task<IActionResult> GetReferenceRanges(string TCODE, string RSTP, string SEX)//GET_Ord_TP
        {
            var GetReferenceRanges = _masterRepository.GetReferenceRanges().Result.Where(x => x.TCODE == TCODE
                                    && x.RSTP == RSTP
                                    && x.SEX == SEX).ToList();
            if (GetReferenceRanges == null)
                return NotFound(new { Message = "Reference Ranges not found!" });
            return Ok(new
            {
                GetReferenceRanges
            });
        }

        [HttpPost("Insert-doctor")]
        public async Task<ActionResult<int>> InsertDoctor([FromBody] DoctorFileModel doctorFileModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _masterRepository.InsertDoctor(doctorFileModel));
        }

        [HttpPut("Update-doctor/{Id}")]
        public async Task<ActionResult<int>> UpdateDoctor(int Id, [FromBody] DoctorFileModel doctorFileModel)
        {
            if (Id != doctorFileModel.DOC_FL_ID)
                return BadRequest("DOC_FL_ID mismatch");

            var result = await _masterRepository.UpdateDoctor(Id, doctorFileModel);
            if (result == 0)
            {
                return NotFound($"Doctor with Id = {doctorFileModel.DOC_FL_ID} not found");
            }
            return NoContent();
        }

        [HttpDelete("Delete-doctor/{DOC_FL_ID}")]
        public async Task<ActionResult<int>> DeleteDoctor(int DOC_FL_ID)
        {
            try
            {
                var result = await _masterRepository.DeleteDoctor(DOC_FL_ID);
                if (result == 0)
                {
                    return NotFound($"Doctor with Id = {DOC_FL_ID} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetAllDoctorFile")]
        public async Task<ActionResult> GetAllDoctorFile()//GET_Ord_TP
        {
            var AllDoctorFile = await _masterRepository.GetAllDoctorFile();
            if (AllDoctorFile == null)
                return NotFound(new { Message = "Doctor File not found!" });
            return Ok(new
            {
                AllDoctorFile
            });
        }
        [HttpGet("GetAllDoctorFile/{Id}")]
        public async Task<IActionResult> GetDoctorFileById(int Id)//GET_Ord_TP
        {
            var GetAllDoctorFile = await _masterRepository.GetAllDoctorFile();
            if (GetAllDoctorFile == null)
                return NotFound(new { Message = "Doctor File not found!" });
            return Ok(new
            {
                GetAllDoctorFile
            });
        }
        [HttpGet("GetDoctorFileByDrNo/{DRNO}")]
        public async Task<IActionResult> GetDoctorFileByDrNo(string DRNO)//GET_Ord_TP
        {
            var GetDoctorFileByDrNo = await _masterRepository.GetDoctorFileByDrNo(DRNO);
            // if (GetDoctorFileByDrNo == null)
            //    return NotFound(new { Message = "Doctor File not found!" });
            return Ok(new
            {
                GetDoctorFileByDrNo
            });
        }


        [HttpGet("GetAllLocationsFile")]
        public async Task<IActionResult> GetAllLocationsFile()//GET_Ord_TP
        {
            var GetAllLocationsFile = await _masterRepository.GetAllLocationsFile();
            if (GetAllLocationsFile == null)
                return NotFound(new { Message = "Locations File not found!" });
            return Ok(new
            {
                GetAllLocationsFile
            });
        }
        [HttpGet("GetLocationsFileById/{Id}")]
        public async Task<IActionResult> GetLocationsFileById(int Id)//GET_Ord_TP
        {
            var GetLocationsFileById = await _masterRepository.GetLocationsFileById(Id);
            if (GetLocationsFileById == null)
                return NotFound(new { Message = "Locations File not found!" });
            return Ok(new
            {
                GetLocationsFileById
            });
        }
        [HttpGet("GetLocationsFileByLoc/{LOC}")]
        public async Task<IActionResult> GetLocationsFileByLoc(string LOC)//GET_Ord_TP
        {
            var GetLocationsFileByLoc = await _masterRepository.GetLocationsFileByLoc(LOC);
            //if (GetLocationsFileByLoc == null)
            //return Ok( new { GetLocationsFileByLoc = $"Locations File '{LOC}' not found!" });
            return Ok(new
            {
                GetLocationsFileByLoc
            });
        }
        [HttpGet("GetSharedTableNat")]
        public async Task<IActionResult> GetSharedTableNat()//GET_Ord_TP
        {
            var GetSharedTable = _masterRepository.GetSharedTable().Result.ToList();
            if (GetSharedTable == null)
                return NotFound(new { Message = "Shared table Ranges not found!" });
            return Ok(new
            {
                GetSharedTable
            });
        }
        #endregion

        //[HttpPost("Insert-CopyfromClient")]
        //public async Task<ActionResult<int>> InsertCopyfromClient([FromBody] SpecialPrices sp)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        return Ok(await _masterRepository.InsertCopyfromClient(sp));


        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An unexpected error occurred. Please try again later.");
        //    }
        //} 

    }
}
