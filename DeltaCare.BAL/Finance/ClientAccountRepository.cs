using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Account
{
    public class ClientAccountRepository : IClientAccountRepository
    {
        private readonly IDataRepository _datarepository;
        public ClientAccountRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }

        public async Task<int> InsertClientAccountEntry(ClientAccountEntryModel clientAccountEntry)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            clientAccountEntry.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ClientAccountEntryModel>(clientAccountEntry);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_ClientAccountEntry, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateClientAccountEntry(int Id, ClientAccountEntryModel clientAccountEntry)
        {
            try
            {
                clientAccountEntry.CLNTACNT_ID = Id;
                int queryType = (int)QueryTypeEnum.Update;
                clientAccountEntry.QueryType = queryType;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ClientAccountEntryModel>(clientAccountEntry);
                DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_ClientAccountEntry, parameterCollection);
                var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
                return await Task.FromResult(getData.returnData);
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

        public async Task<IEnumerable<ClientAccountDataEntryModel>> GetDataEntryList(int id, string companyNo)
        {
            try
            {
                ClientAccountDataEntry clientAccountDataEntry = new ClientAccountDataEntry();
                clientAccountDataEntry.QueryType = (int)QueryTypeEnum.GetAll;
                clientAccountDataEntry.CompanyNo = companyNo;
                clientAccountDataEntry.Id = id;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ClientAccountDataEntry>(clientAccountDataEntry);
                return (await _datarepository.ExecuteQueryAsync<ClientAccountDataEntryModel>(SPConstant.SP_ClientAccountDataEntry, parameterCollection)).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ClientAccountDataEntryModel>> GetClientAccountList(string companyNo)
        {
            try
            {
                int queryType = (int)QueryTypeEnum.GetAll;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, companyNo, "QueryType", "CompanyNo");
                return (await _datarepository.ExecuteQueryAsync<ClientAccountDataEntryModel>(SPConstant.SP_ClientAccountDetails, parameterCollection)).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<ClientAccountDataEntryModel>> GetClientAccountStatement(int id,string companyNo, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                AccountStatemntFilter obj = new AccountStatemntFilter();
                obj.Id = id;
                obj.CompanyNo = companyNo;
                obj.FromDate = fromDate != null ? fromDate.Value.ToString("yyyy-MM-dd") : null;
                obj.ToDate = toDate != null ? toDate.Value.ToString("yyyy-MM-dd") : null;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(obj);
                return (await _datarepository.ExecuteQueryAsync<ClientAccountDataEntryModel>(SPConstant.Sp_ClientAccountStatement, parameterCollection)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<ClientAccountDataEntryModel> GetDataEntryById(int id)
        {
            try
            {
                int queryType = (int)QueryTypeEnum.GetById;
                var parameters = ParameterGenerator.CreateParameterList(queryType, id, "QueryType", "Id");
                return (await _datarepository.ExecuteQueryAsync<ClientAccountDataEntryModel>(SPConstant.SP_ClientAccountDataEntry, parameters)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ClientAccountDataEntryModel> GetClientAccountById(int id,string companyNo)
        {
            try
            {
                var parameters = ParameterGenerator.CreateParameterList(id, companyNo, "Id","CompanyNo");
                return (await _datarepository.ExecuteQueryAsync<ClientAccountDataEntryModel>(SPConstant.SP_ClientAccountDetails, parameters)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ClientAccountCrossCheckModel>> GetClientAccountCrossCheckList(ClientAccountCrossCheckDataModel clientAccountCrossCheckDataModel)
        {

            clientAccountCrossCheckDataModel.QueryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(clientAccountCrossCheckDataModel);
            return (await _datarepository.ExecuteQueryAsync<ClientAccountCrossCheckModel>(SPConstant.Sp_ManageCrossCheck, parameterCollection)).ToList();


        }
        public async Task<IEnumerable<ClientAccountCurrentStatusModel>> GetClientAccountCurrentStatusList(string companyNo)
        {

            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, companyNo, "QueryType", "CompanyNo");
            return (await _datarepository.ExecuteQueryAsync<ClientAccountCurrentStatusModel>(SPConstant.Sp_ManageCurrentStatus, parameterCollection)).ToList();

        }

    }
}
