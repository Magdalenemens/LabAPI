using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL
{
    public interface IPRRepository
    {
        Task<IEnumerable<PatientRegistrationModel>> GetPatientRegistration();//
        Task<IEnumerable<PatientRegistrationModel>> GetPatientRegistrationById(int Id);//
        Task<IEnumerable<PatientRegistrationModel>> GetPatientRegistrationByPATID(string PAT_ID);//GetPR
        Task<IEnumerable<PatientRegistrationModel>> GetPatientRegistrationByLike(string PAT_ID);//PRSearch

        Task<int> InsertPatientRegistration(PatientRegistrationModel patientRegistrationModel);//RegisterUserAccount
        Task<int> UpdatePatientRegistration(PatientRegistrationModel patientRegistrationModel);
    }
}
