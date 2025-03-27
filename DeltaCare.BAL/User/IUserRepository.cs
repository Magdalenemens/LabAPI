using DeltaCare.Entity.Model;

namespace DeltaCare.BAL
{
    public interface IUserRepository
    {
        Task<UserModel> GetById(int id);
        Task<IEnumerable<UserFLModel>> GetAllUser();
        Task<UserFLModel> GetUserById(int SP_TYPE_ID);
        Task<UserFLModel> GetUserId(string USER_ID);
        Task<int> InsertUser(UserFLModel objuserfl);
        Task<int> UpdateUser(int Id, UserFLModel objuserfl);
        Task<int> DeleteUser(int Id);
        Task<bool> VerifyPasswordAsync(int id, string password);
        Task<bool> ChangePasswordAsync(int id, string currentPassword, string newPassword);
        Task<UserFLModel> GetUserByUserIdAndPass(LoginModel loginModel);
        Task<IEnumerable<LoginFLModel>> GetAllUserLoginHistory();
        Task<IEnumerable<LoginFLModel>> GetUserLoginHistoryById(string id);
        Task<int> InsertUserLogInTime(LoginFLModel loginFLModel);
        Task<int> UpdateUserLogoutTime(int Id, LoginFLModel loginFLModel);
        Task<UserJobTypeModel> GetUserJobTypeById(string JobType);
        Task<IEnumerable<RoleModel>> GetAllRoles();
        Task<IEnumerable<UserJobTypeModel>> GetAllJobType();
        Task<IEnumerable<UserJobTypeModel>> GetAllUserJobType();
        Task<int> InsertPageRecord(PageTrackRecordModel pageTrackRecordModel);
        Task<List<GetPageTrackRecordModel>> GetPageRecord(string UserId);
        Task<IEnumerable<GetPageTrackRecordModel>> GetAllPageRecord();
    }
}
