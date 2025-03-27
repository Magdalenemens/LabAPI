using Dapper;
using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using System.Data;

namespace DeltaCare.BAL
{
    public class ConfigurationRepository : IConfigurationRepository
    {

        private readonly IDataRepository _datarepository;

        public ConfigurationRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }
        public async Task<int> InsertSystemConfig(SysConfigModel sysConfigModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            sysConfigModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SysConfigModel>(sysConfigModel);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_Configuration, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateSystemConfig(int Id, SysConfigModel sysConfigModel)
        {
            sysConfigModel.SYSCNFG_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            sysConfigModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SysConfigModel>(sysConfigModel, "QueryType"); ;
            IEnumerable<SysConfigModel> result = await _datarepository.ExecuteQueryAsync<SysConfigModel>(SPConstant.Sp_Configuration, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteSystemConfig(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            var parameters = ParameterGenerator.CreateParameterList(Id, queryType, "SYSCNFG_ID ", "QueryType");
            IEnumerable<SysConfigModel> result = await _datarepository.ExecuteQueryAsync<SysConfigModel>(SPConstant.Sp_Configuration, parameters);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<SysConfigModel>> GetAllSystemConfig()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SysConfigModel>(SPConstant.Sp_Configuration, parameters)).ToList();
        }

        public async Task<SysConfigModel> GetSystemConfigById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Id, queryType, "SP_TYPE_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SysConfigModel>(SPConstant.Sp_Configuration, parameters)).FirstOrDefault();
        }

        public async Task<int> InsertSiteTestsAssignment(List<SiteTestsAssignmentModel> siteTestsAssignments)
        {
            int totalRowsAffected = 0;

            foreach (var siteTests in siteTestsAssignments)
            {
                // Create a parameter collection for each site test
                IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>
        {
            new QueryParameterForSqlMapper
            {
                Name = "@REF_SITE",
                Value = siteTests.REF_SITE
            },
            new QueryParameterForSqlMapper
            {
                Name = "@TCODE",
                Value = siteTests.TCODE
            },
            new QueryParameterForSqlMapper
            {
                Name = "@TEST_ID",
                Value = siteTests.TEST_ID
            },
             new QueryParameterForSqlMapper
            {
                Name = "@REF_SITE_S",
                Value = siteTests.REF_SITE_S
            },
              new QueryParameterForSqlMapper
            {
                Name = "@SELECTED_REF_SITE",
                Value = siteTests.SELECTED_REF_SITE
            },
                new QueryParameterForSqlMapper
            {
                Name = "@ABRV",
                Value = siteTests.ABRV
            },
            new QueryParameterForSqlMapper
            {
                Name = "@QueryType",
                Value = (int)QueryTypeEnum.Insert
            }
        };

                // Execute the insert query for each item in the list
                var result = await _datarepository.ExecuteQueryAsync<int>(SPConstant.Sp_SiteTests, parameterCollection);

                // Accumulate the number of rows affected
                totalRowsAffected += result.FirstOrDefault();
            }

            return totalRowsAffected;
        }

        public async Task<int> DeleteSiteTestsAssignment(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "SITE_TESTS_ID", "QueryType");
            IEnumerable<SiteTestsAssignmentModel> result = await _datarepository.ExecuteQueryAsync<SiteTestsAssignmentModel>(SPConstant.Sp_SiteTests, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<SiteTestsAssignmentModel>> GetAllSiteTestsAssignment()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SiteTestsAssignmentModel>(SPConstant.Sp_SiteTests, parameterCollection)).ToList();
        }

        public async Task<SiteTestsAssignmentModel> GetSiteTestsAssignmentById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "SITE_TESTS_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SiteTestsAssignmentModel>(SPConstant.Sp_SiteTests, parameterCollection)).FirstOrDefault();
        }
    }
}