using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Site
{
    public class SiteRepository : ISiteRepository
    {
        private readonly IDataRepository _datarepository;

        public SiteRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }

        public async Task<IEnumerable<UserSitesAccessModel>> GetAllUserSites()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<UserSitesAccessModel>(SPConstant.Sp_ManageUserSiteAccess, parameterCollection)).ToList();
        }
        
        public async Task<int> RegisterUserSites(List<UserSitesAccessModel> userSitesAccessModels)
        {
            if (userSitesAccessModels == null || !userSitesAccessModels.Any())
                throw new ArgumentException("The list cannot be null or empty.", nameof(userSitesAccessModels));

            var tasks = userSitesAccessModels.Select(async item =>
            {
                var parameters = new List<QueryParameterForSqlMapper>
                {
                    new QueryParameterForSqlMapper { Name = "@USER_SITES_ID", Value = item.USER_SITES_ID },
                    new QueryParameterForSqlMapper { Name = "@USER_ID", Value = item.USER_ID },
                    new QueryParameterForSqlMapper { Name = "@SITE_NO", Value = item.SITE_NO },
                    new QueryParameterForSqlMapper { Name = "@QueryType", Value = (int)QueryTypeEnum.Update } // Adjust based on actual intent
                };

                // Execute the stored procedure and return affected rows for each item
                var result = await _datarepository.ExecuteQueryAsync<int>(SPConstant.Sp_ManageUserSiteAccess, parameters);
                return result.ToList();
            });

            // Wait for all tasks to complete and sum the results to get the total affected row count
            var results = await Task.WhenAll(tasks);
            return results.Count();
        }

        public async Task<IEnumerable<UserSitesAccessModel>> GetSitesByUserId(string userId)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(userId, queryType, "USER_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<UserSitesAccessModel>(SPConstant.Sp_ManageUserSiteAccess, parameterCollection)).ToList();
        }
        public async Task<UserSitesAccessModel> GetSiteDetailBySiteNo(string siteNo)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(siteNo, queryType, "SITE_NO", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<UserSitesAccessModel>(SPConstant.Sp_ManageUserSiteAccess, parameters)).FirstOrDefault();
        }
        public async Task<int> DeleteUserSite(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "USER_SITES_ID", "QueryType");
            IEnumerable<UserSitesAccessModel> result = await _datarepository.ExecuteQueryAsync<UserSitesAccessModel>(SPConstant.Sp_ManageUserSiteAccess, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<FindAllSitesModel>> FindAllSites(string search)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(search, queryType, "@SEARCH", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<FindAllSitesModel>(SPConstant.Sp_ManageUserSiteAccess, parameters)).ToList();
        }
    }
}
