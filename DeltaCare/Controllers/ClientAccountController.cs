using DeltaCare.BAL.Account;
using DeltaCare.BAL.Clinical.AP;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAccountController : ControllerBase
    {
        private readonly IClientAccountRepository _clientAccountRepository;
        private readonly ILogger<ClinicalController> _logger;
        public ClientAccountController(IClientAccountRepository clientAccountRepository, ILogger<ClinicalController> logger)
        {
            _clientAccountRepository = clientAccountRepository;
            _logger = logger;
        }

        [HttpPost("Insert-clientaccount")]
        public async Task<ActionResult<int>> InsertClientAccountEntry(ClientAccountEntryModel clientAccountEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _clientAccountRepository.InsertClientAccountEntry(clientAccountEntry));
        }

        [HttpPut("Update-clientaccount/{Id}")]
        public async Task<ActionResult<int>> UpdateClientAccountEntry(int Id, ClientAccountEntryModel clientAccountEntry)
        {
            if (Id != clientAccountEntry.CLNTACNT_ID)
                return BadRequest("Client Account ID mismatch");

            var result = (await _clientAccountRepository.UpdateClientAccountEntry(Id, clientAccountEntry));
            if (result == 0)
            {
                return NotFound($" Clinet Account with Id = {clientAccountEntry.CLNTACNT_ID} not found");
            }
            return NoContent();
        }


        [HttpGet("GetClientAccountDataEntry/{Id}/{companyNo}")]
        public async Task<ActionResult> GetClientAccountDataEntry(int id, string companyNo)
        {
            
                return Ok(await _clientAccountRepository.GetDataEntryList(id, companyNo));
            
        }
        [HttpGet("GetClientAccount")]
        public async Task<ActionResult> GetClientAccount(string companyNo)
        {
                return Ok(await _clientAccountRepository.GetClientAccountList(companyNo));
        }
        [Route("GetClientAccountStatement")]
        [HttpGet]
        public async Task<ActionResult> GetClientAccountStatement(int Id,string CompanyNo, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            var result = await _clientAccountRepository.GetClientAccountStatement(Id,CompanyNo,FromDate, ToDate);
            if (result == null)
                return NotFound($"Results Client Account Statement with Id = {Id} not found");
            return Ok(result);
        }

        [HttpGet("GetClientAccountDataEntryById/{Id}")]
        public async Task<ActionResult<ClientAccountDataEntryModel>> GetClientAccountDataEntryById(int Id)
        {
            var result = await _clientAccountRepository.GetDataEntryById(Id);
            if (result == null)
                return NotFound($"Results Client Account with Id = {Id} not found");
            return result;
        }
        [HttpGet("GetClientAccountById/{Id}/{CompanyNo}")]
        public async Task<ActionResult<ClientAccountDataEntryModel>> GetClientAccountById(int Id,string CompanyNo)
        {
            var result = await _clientAccountRepository.GetClientAccountById(Id, CompanyNo);
            if (result == null)
                return NotFound($"Results Client Account with Id = {Id} not found");
            return result;
        }
        [HttpGet("GetClientAccountCrossCheck")]
        public async Task<ActionResult> GetClientAccountCrossCheck(int DisplayMonth,bool IsPositive, string CompanyNo)
        {
            ClientAccountCrossCheckDataModel obj = new ClientAccountCrossCheckDataModel();
            obj.IsPositive = IsPositive;
            obj.DisplayMonth = DisplayMonth;
            obj.CompanyNo = CompanyNo;
            return Ok(await _clientAccountRepository.GetClientAccountCrossCheckList(obj));
        }

        [HttpGet("GetClientAccountCurrentStatus")]
        public async Task<ActionResult> GetClientAccountCurrentStatus(string companyNo)
        {
            return Ok(await _clientAccountRepository.GetClientAccountCurrentStatusList( companyNo));
        }
    }
}
