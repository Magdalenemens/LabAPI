using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.PR
{
    public class PRRepository:IPRRepository
    {
        private readonly IDataRepository _datarepository;
        public PRRepository(IDataRepository dataRepository)
        {
            _datarepository = new DataRepository();
        }
        public async Task<IEnumerable<PatientRegistrationModel>> GetPatientRegistration()//
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<PatientRegistrationModel>(SPConstant.SP_ManagePatientRegistration, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<PatientRegistrationModel>> GetPatientRegistrationById(int id)//
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(id, queryType, "PR_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<PatientRegistrationModel>(SPConstant.SP_ManagePatientRegistration, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<PatientRegistrationModel>> GetPatientRegistrationByPATID(string PAT_ID)//GetPR 
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(PAT_ID,queryType, "PAT_ID", "QueryType");
            var TRU = (await _datarepository.ExecuteQueryAsync<PatientRegistrationModel>(SPConstant.SP_ManagePatientRegistration, parameterCollection)).ToList();
            return TRU;
        }
        public async Task<IEnumerable<PatientRegistrationModel>> GetPatientRegistrationByLike(string PAT_ID) //PRSearch
        {
            int queryType = 0;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(PAT_ID, queryType, "PAT_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<PatientRegistrationModel>(SPConstant.SP_ManagePatientRegistration, parameterCollection)).ToList();
        }



        public async Task<int> InsertPatientRegistration(PatientRegistrationModel  patientRegistrationModel)//RegisterUserAccount
        {
            int queryType = (int)QueryTypeEnum.Insert;
            patientRegistrationModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PatientRegistrationModel>(patientRegistrationModel);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.SP_ManagePatientRegistration, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdatePatientRegistration(PatientRegistrationModel  patientRegistrationModel)//RegisterUserAccount
        {
            int queryType = (int)QueryTypeEnum.Update;
            patientRegistrationModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PatientRegistrationModel>(patientRegistrationModel);
            IEnumerable<PatientRegistrationModel> result = await _datarepository.ExecuteQueryAsync<PatientRegistrationModel>(SPConstant.SP_ManagePatientRegistration, parameterCollection);

            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
