using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.UserAccess
{
    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly IDataRepository _dataRepository;

        public UserAccessRepository(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;

        }

        public async Task<IEnumerable<UserAccesseModel>> GetModuleAccessDetailsByUserId(string userId)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(userId, queryType, "USER_ID", "QueryType");

            var loginHistory = await _dataRepository.ExecuteQueryAsync<UserAccesseModel>(SPConstant.Sp_ManageUserAccess, parameters);

            return loginHistory;
        }
    }

}
