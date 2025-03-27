using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDataRepository _datarepository;
        public AccountRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }


        public int InvokeBilling()
        {
            int rowsAffected = 0;
            try
            {
                int queryType = (int)QueryTypeEnum.Insert;
                var parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
                rowsAffected= _datarepository.ExecuteNonQuery(SPConstant.SP_ManageBilling, parameters);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }


        public int DeleteBilling()
        {
            int rowsAffected = 0;
            try
            {
                int queryType = (int)QueryTypeEnum.Delete;
                var parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
                rowsAffected = _datarepository.ExecuteNonQuery(SPConstant.SP_ManageBilling, parameters);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }



        public async Task<IEnumerable<BillingModel>> GetBillingData(string cn)
        {
            try
            {
                int queryType = (int)QueryTypeEnum.GetById;
                var parameters = ParameterGenerator.CreateParameterList(cn, queryType, "CN", "QueryType");
                return (await _datarepository.ExecuteQueryAsync<BillingModel>(SPConstant.SP_ManageBilling, parameters)).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ClientNumberModel>> GetClientNumber()
        {
            try
            {
                int queryType = (int)QueryTypeEnum.GetAll;
                var parameters = ParameterGenerator.CreateParameterList( queryType, "QueryType");
                return (await _datarepository.ExecuteQueryAsync<ClientNumberModel>(SPConstant.SP_ManageBilling, parameters)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
