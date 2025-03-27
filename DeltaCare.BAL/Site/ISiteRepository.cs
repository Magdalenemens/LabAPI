using DeltaCare.Entity.Model; 

namespace DeltaCare.BAL.Site
{
    public interface ISiteRepository
    {        
        Task<int> RegisterUserSites(List<UserSitesAccessModel> userSitesAccessModels);
        Task<IEnumerable<UserSitesAccessModel>> GetAllUserSites();
        Task<IEnumerable<UserSitesAccessModel>> GetSitesByUserId(string Id);
        Task<UserSitesAccessModel> GetSiteDetailBySiteNo(string siteNo);
        Task<int> DeleteUserSite(int Id);
        Task<IEnumerable<FindAllSitesModel>> FindAllSites(string search);
    }
}
