using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using System.Data;

namespace DeltaCare.BAL
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataRepository _dataRepository;

        public UserRepository(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;

        }
        public async Task<UserModel> GetById(int id)
        {
            UserModel user = new UserModel()
            {
                Id = 1,
                UserEmail = "test@gmail.com",
                UserName = "test",
                Role = "admin"
            };
            return await Task.FromResult(user);
        }

        #region User 
        public async Task<IEnumerable<UserFLModel>> GetAllUser()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> paramUserFLCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<UserFLModel>(SPConstant.Sp_ManageUsers, paramUserFLCollection)).ToList();
        }
        public async Task<UserFLModel> GetUserById(int USER_FL_ID)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> paramUserFLCollection = ParameterGenerator.CreateParameterList(USER_FL_ID, queryType, "USER_FL_ID", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<UserFLModel>(SPConstant.Sp_ManageUsers, paramUserFLCollection)).FirstOrDefault();

        }

        public async Task<UserFLModel> GetUserId(string USER_ID)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> paramUserFLCollection = ParameterGenerator.CreateParameterList(USER_ID, queryType, "USER_ID", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<UserFLModel>(SPConstant.Sp_ManageUsers, paramUserFLCollection)).FirstOrDefault();
        }

        public async Task<int> InsertUser(UserFLModel userFLModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            userFLModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<UserFLModel>(userFLModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.Sp_ManageUsers, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateUser(int Id, UserFLModel userFLModel)
        {
            userFLModel.USER_FL_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            userFLModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<UserFLModel>(userFLModel);
            IEnumerable<UserFLModel> result = await _dataRepository.ExecuteQueryAsync<UserFLModel>(SPConstant.Sp_ManageUsers, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else

            {
                return 0;
            }
        }

        public async Task<int> DeleteUser(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "user_Fl_id", "QueryType");
            IEnumerable<SiteModel> result = await _dataRepository.ExecuteQueryAsync<SiteModel>(SPConstant.Sp_ManageUsers, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> VerifyPasswordAsync(int id, string password)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            var parameterCollection = new List<QueryParameterForSqlMapper>
            {
                new QueryParameterForSqlMapper { Name = "@Id", Value = id, DbType = DbType.Int32 },
                new QueryParameterForSqlMapper { Name = "@CurrentPassword", Value = password, DbType = DbType.String },
                new QueryParameterForSqlMapper { Name = "@QueryType", Value =queryType, DbType = DbType.Int32 },
            };

            var result = await _dataRepository.ExecuteQueryAsync<bool>(SPConstant.Sp_ValidatePassword, parameterCollection);
            return result.Count() > 0; // Password is valid

        }

        public async Task<bool> ChangePasswordAsync(int id, string currentPassword, string newPassword)
        {
            int queryType = (int)QueryTypeEnum.Update;

            // Define the parameter collection for the stored procedure
            var parameterCollection = new List<QueryParameterForSqlMapper>
            {
                new QueryParameterForSqlMapper { Name = "@Id", Value = id, DbType = DbType.Int32 },
                new QueryParameterForSqlMapper { Name = "@CurrentPassword", Value = currentPassword, DbType = DbType.String },
                new QueryParameterForSqlMapper { Name = "@NewPassword", Value = newPassword, DbType = DbType.String },
                new QueryParameterForSqlMapper { Name = "@QueryType", Value = queryType, DbType = DbType.Int32 }
            };

            // Execute the stored procedure and get the result
            var result = await _dataRepository.ExecuteQueryAsync<bool>(SPConstant.Sp_ValidatePassword, parameterCollection);
            // Return true if the result indicates success, otherwise false
            return result.Count() > 0;
        }

        #endregion
        public async Task<UserFLModel> GetUserByUserIdAndPass(LoginModel loginModel)
        {
            int queryType = (int)QueryTypeEnum.GetByName;            
            var parameterCollection = new List<QueryParameterForSqlMapper>
            {
                new QueryParameterForSqlMapper { Name = "@USER_NAME", Value = loginModel.UserName, DbType = DbType.String },
                new QueryParameterForSqlMapper { Name = "@PASS_WORD", Value = loginModel.Password, DbType = DbType.String },
                new QueryParameterForSqlMapper { Name = "@QueryType", Value =queryType, DbType = DbType.Int32 },
            };
            return (await _dataRepository.ExecuteQueryAsync<UserFLModel>(SPConstant.Sp_ManageUsers, parameterCollection)).FirstOrDefault();
        }

        public async Task<IEnumerable<LoginFLModel>> GetAllUserLoginHistory()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> paramUserFLCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<LoginFLModel>(SPConstant.Sp_UserLoginHistory, paramUserFLCollection)).ToList();
        }

        public async Task<IEnumerable<LoginFLModel>> GetUserLoginHistoryById(string userId)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(userId, queryType, "U_ID", "QueryType");

            var loginHistory = await _dataRepository.ExecuteQueryAsync<LoginFLModel>(SPConstant.Sp_UserLoginHistory, parameters);

            return loginHistory;
        }

        public async Task<int> InsertUserLogInTime(LoginFLModel loginFLModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            loginFLModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<LoginFLModel>(loginFLModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.Sp_UserLoginHistory, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateUserLogoutTime(int Id, LoginFLModel loginFLModel)
        {
            loginFLModel.LOGIN_FL_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            loginFLModel.QueryType = queryType;
            TimeZoneInfo saudiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Riyadh");
            loginFLModel.OUT_DTTM = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, saudiTimeZone);
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<LoginFLModel>(loginFLModel);
            parameterCollection = parameterCollection.Select(p =>
            {
                if (p.Name == "IN_DTTM" || p.Name == "OUT_DTTM")
                {
                    p.DbType = DbType.DateTime;
                }
                return p;
            }).ToList();
            var result = await _dataRepository.ExecuteQueryAsync<int>(SPConstant.Sp_UserLoginHistory, parameterCollection);
            return result.Any() ? 1 : 0;
        }

        public async Task<IEnumerable<RoleModel>> GetAllRoles()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> paramUserFLCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<RoleModel>(SPConstant.Sp_ManageUserAccess, paramUserFLCollection)).ToList();
        }

        public async Task<IEnumerable<UserJobTypeModel>> GetAllJobType()
        {
            int queryType = (int)QueryTypeEnum.Search;
            IList<QueryParameterForSqlMapper> paramUserFLCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<UserJobTypeModel>(SPConstant.Sp_ManageUserJobType, paramUserFLCollection)).ToList();
        }
        public async Task<IEnumerable<UserJobTypeModel>> GetAllUserJobType()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> paramUserFLCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<UserJobTypeModel>(SPConstant.Sp_ManageUserJobType, paramUserFLCollection)).ToList();
        }
        public async Task<UserJobTypeModel> GetUserJobTypeById(string JobType)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> paramUserFLCollection = ParameterGenerator.CreateParameterList(JobType, queryType, "JOB_CD", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<UserJobTypeModel>(SPConstant.Sp_ManageUserJobType, paramUserFLCollection)).FirstOrDefault();

        }

        public async Task<int> InsertPageRecord(PageTrackRecordModel pageTrackRecordModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            pageTrackRecordModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PageTrackRecordModel>(pageTrackRecordModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.Sp_PageTracking, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }
        public async Task<List<GetPageTrackRecordModel>> GetPageRecord(string userId)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(userId, queryType, "USER_ID", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<GetPageTrackRecordModel>(SPConstant.Sp_PageTracking, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<GetPageTrackRecordModel>> GetAllPageRecord()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> paramUserFLCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<GetPageTrackRecordModel>(SPConstant.Sp_PageTracking, paramUserFLCollection)).ToList();
        }
    }
}
