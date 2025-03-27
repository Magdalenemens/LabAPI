using DeltaCare.BAL;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRegistrationController : DeltaBaseController
    {
        private readonly IPRRepository  _pRRepository;
        public PatientRegistrationController(IPRRepository pRRepository)
        {
            _pRRepository = pRRepository;
        }
        [HttpGet("GetPatientRegistration")]
        public async Task<IActionResult> GetPatientRegistration()
        {
            var GetPR = await _pRRepository.GetPatientRegistration();
            if (GetPR == null)
                return NotFound(new { Message = "Patients not found!" });

            return Ok(new
            {
                GetPR
            });
        }

        [HttpGet("GetPatientRegistrationById/{id}")]
        public async Task<IActionResult> GetPatientRegistrationById(int id)
        {
            if (id == 0)
                return BadRequest();

            var GetPR = await _pRRepository.GetPatientRegistrationById(id);
            if (GetPR == null)
                return NotFound(new { Message = "Patient deatails not found!" });

            return Ok(new
            {
                GetPR
            });
        }
        [HttpGet("GetPatientRegistrationByPATID/{PAT_ID}")]//GetPR //PRSearch
        public async Task<IActionResult> GetPatientRegistrationByPATID(string PAT_ID)
        {
            if (string.IsNullOrEmpty( PAT_ID ))
                return BadRequest();

            var GetPR = await _pRRepository.GetPatientRegistrationByPATID(PAT_ID);
            if (GetPR == null)
                return NotFound(new { Message = "Patient deatails not found!" });

            return Ok(new
            {
                GetPR
            });
        }

        [HttpGet("GetPatientRegistrationByLike/{PAT_ID}")]//GetPR //PRSearch
        public async Task<IActionResult> GetPatientRegistrationByLike(string PAT_ID)
        {
            if (string.IsNullOrEmpty(PAT_ID))
                return BadRequest();

            var GetPR = await _pRRepository.GetPatientRegistrationByLike(PAT_ID);
            if (GetPR == null)
                return NotFound(new { Message = "Patient deatails not found!" });

            return Ok(new
            {
                GetPR
            });
        }


        [HttpPut("UpdatePatientRegistration")]
        public async Task<IActionResult> UpdatePatientRegistration([FromBody] PatientRegistrationModel patientRegistrationModel)//RegisterUserAccount
        {
            if (patientRegistrationModel == null)
                return BadRequest();
            if (string.IsNullOrEmpty(patientRegistrationModel.PAT_ID)) return BadRequest();
            

            int updatedValue = await _pRRepository.UpdatePatientRegistration(patientRegistrationModel);

            if (updatedValue == 0)
            {
                int InsertedValue = await _pRRepository.InsertPatientRegistration(patientRegistrationModel);
                if (InsertedValue != 0)
                {
                    return Ok(new
                    {
                        Message = "New Patient information has been created."
                    });
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (updatedValue > 0)
            {
                return Ok(new
                {
                    Message = "Patient information has been updated."
                });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
