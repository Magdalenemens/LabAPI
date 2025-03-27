using DeltaCare.BAL;
using DeltaCare.BAL.Account;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : DeltaBaseController
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IGenLabRepository _genLabRepository;
        private readonly ILogger<AccountController> _logger;
        public AccountController(
            IDirectoryRepository directoryRepository,
            IAccountRepository accountRepository,
            IGenLabRepository genLabRepository,
            ILogger<AccountController> logger)
        {
            _directoryRepository = directoryRepository;
            _accountRepository = accountRepository;
            _genLabRepository = genLabRepository;
            _logger = logger;
        }

        [HttpGet("InvokeBilling")]
        public ActionResult<int> InvokeBilling()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_accountRepository.InvokeBilling());
        }


        /// <summary>
        /// Delete the billing data from Header & parent table
        /// </summary>
        /// <returns>No of records effected/deleted</returns>
        [HttpGet("DeleteBilling")]
        public ActionResult<int> DeleteBilling()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_accountRepository.DeleteBilling());
        }


        [HttpGet("GetBillingData/{cn}")]
        public async Task<ActionResult<BillingModel>> GetBillingData(string cn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _accountRepository.GetBillingData(cn));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetClientNumber")]
        public async Task<ActionResult<ClientNumberModel>> GetClientNumber()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _accountRepository.GetClientNumber());
        }
    }
}
