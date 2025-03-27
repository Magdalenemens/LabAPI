using Dapper;
using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using System.Data;

namespace DeltaCare.BAL
{
    public class MasterRepository : IMasterRepository
    {

        private readonly IDataRepository _datarepository;

        public MasterRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }
        public async Task<int> InsertSpecimentypes(SpecimentypeModel specimentype)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            specimentype.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SpecimentypeModel>(specimentype);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_Specimentype, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateSpecimentypes(int Id, SpecimentypeModel specimentype)
        {
            specimentype.SP_TYPE_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            specimentype.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SpecimentypeModel>(specimentype, "QueryType"); ;
            IEnumerable<SpecimentypeModel> result = await _datarepository.ExecuteQueryAsync<SpecimentypeModel>(SPConstant.Sp_Specimentype, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteSpecimentypes(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            var parameters = ParameterGenerator.CreateParameterList(Id, queryType, "SP_TYPE_ID ", "QueryType");
            IEnumerable<SpecimentypeModel> result = await _datarepository.ExecuteQueryAsync<SpecimentypeModel>(SPConstant.Sp_Specimentype, parameters);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<SpecimentypeModel>> GetAllSpecimentypes()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SpecimentypeModel>(SPConstant.Sp_Specimentype, parameters)).ToList();
        }

        public async Task<SpecimentypeModel> GetSpecimentypesById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Id, queryType, "SP_TYPE_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SpecimentypeModel>(SPConstant.Sp_Specimentype, parameters)).FirstOrDefault();
        }

        public async Task<int> InsertDivision(DivisionModel division)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            division.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<DivisionModel>(division);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_Division, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateDivision(int Id, DivisionModel division)
        {
            division.LAB_DIV_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            division.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<DivisionModel>(division, "QueryType");
            IEnumerable<DivisionModel> result = await _datarepository.ExecuteQueryAsync<DivisionModel>(SPConstant.Sp_Division, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteDivision(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "LAB_DIV_ID", "QueryType");
            IEnumerable<DivisionModel> result = await _datarepository.ExecuteQueryAsync<DivisionModel>(SPConstant.Sp_Division, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<DivisionModel>> GetAllDivision()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<DivisionModel>(SPConstant.Sp_Division, parameterCollection)).ToList();
        }

        public async Task<DivisionModel> GetDivisionById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "LAB_DIV_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<DivisionModel>(SPConstant.Sp_Division, parameterCollection)).FirstOrDefault();

        }

        public async Task<int> InsertSection(SectionModel section)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            section.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SectionModel>(section);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_Section, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateSection(int Id, SectionModel section)
        {
            section.LAB_SECT_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            section.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SectionModel>(section);
            IEnumerable<SectionModel> result = await _datarepository.ExecuteQueryAsync<SectionModel>(SPConstant.Sp_Section, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteSection(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "LAB_SECT_ID", "QueryType");
            IEnumerable<SectionModel> result = await _datarepository.ExecuteQueryAsync<SectionModel>(SPConstant.Sp_Section, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<SectionModel>> GetAllSection()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SectionModel>(SPConstant.Sp_Section, parameterCollection)).ToList();
        }

        public async Task<SectionModel> GetSectionById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "LAB_SECT_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SectionModel>(SPConstant.Sp_Section, parameterCollection)).FirstOrDefault();

        }

        public async Task<int> InsertWorkCenter(WorkCenterModel workCenter)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            workCenter.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<WorkCenterModel>(workCenter);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_WorkCenter, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateWorkCenter(int Id, WorkCenterModel workCenter)
        {
            workCenter.LAB_WC_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            workCenter.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<WorkCenterModel>(workCenter);
            IEnumerable<WorkCenterModel> result = await _datarepository.ExecuteQueryAsync<WorkCenterModel>(SPConstant.Sp_WorkCenter, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteWorkCenter(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "LAB_WC_ID", "QueryType");
            IEnumerable<WorkCenterModel> result = await _datarepository.ExecuteQueryAsync<WorkCenterModel>(SPConstant.Sp_WorkCenter, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<WorkCenterModel>> GetAllWorkCenter()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<WorkCenterModel>(SPConstant.Sp_WorkCenter, parameterCollection)).ToList();
        }

        public async Task<WorkCenterModel> GetWorkCenterById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "LAB_WC_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<WorkCenterModel>(SPConstant.Sp_WorkCenter, parameterCollection)).FirstOrDefault();

        }

        public async Task<int> InsertTestSite(TestSiteModel testSite)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            testSite.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<TestSiteModel>(testSite);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_TestSite, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateTestSite(int Id, TestSiteModel testSite)
        {
            testSite.LAB_TS_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            testSite.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<TestSiteModel>(testSite);
            IEnumerable<TestSiteModel> result = await _datarepository.ExecuteQueryAsync<TestSiteModel>(SPConstant.Sp_TestSite, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteTestSite(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "LAB_TS_ID", "QueryType");
            IEnumerable<TestSiteModel> result = await _datarepository.ExecuteQueryAsync<TestSiteModel>(SPConstant.Sp_TestSite, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<TestSiteModel>> GetAllTestSite()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TestSiteModel>(SPConstant.Sp_TestSite, parameterCollection)).ToList();
        }

        public async Task<TestSiteModel> GetTestSiteById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "LAB_TS_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TestSiteModel>(SPConstant.Sp_TestSite, parameterCollection)).FirstOrDefault();
        }

        public async Task<int> InsertAccnPrefix(AccnPrefixModel accnPrefix)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            accnPrefix.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<AccnPrefixModel>(accnPrefix);
            await _datarepository.ExecuteQueryAsync(SPConstant.Sp_AccnPrefix, parameterCollection);
            return 1;
        }

        public async Task<int> UpdateAccnPrefix(int Id, AccnPrefixModel accnPrefix)
        {
            accnPrefix.ACCNPRFX_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            accnPrefix.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<AccnPrefixModel>(accnPrefix);
            IEnumerable<AccnPrefixModel> result = await _datarepository.ExecuteQueryAsync<AccnPrefixModel>(SPConstant.Sp_AccnPrefix, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteAccnPrefix(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "ACCNPRFX_ID", "QueryType");
            IEnumerable<AccnPrefixModel> result = await _datarepository.ExecuteQueryAsync<AccnPrefixModel>(SPConstant.Sp_AccnPrefix, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<AccnPrefixModel>> GetAllAccnPrefix()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<AccnPrefixModel>(SPConstant.Sp_AccnPrefix, parameterCollection)).ToList();
        }

        public async Task<AccnPrefixModel> GetAccnPrefixById(int Id)
        {

            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "ACCNPRFX_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<AccnPrefixModel>(SPConstant.Sp_AccnPrefix, parameterCollection)).FirstOrDefault();

        }

        public async Task<int> InsertResultsTemplates(ResultsTemplatesModel resultsTemplates)
        {

            int queryType = (int)QueryTypeEnum.Insert;
            resultsTemplates.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ResultsTemplatesModel>(resultsTemplates);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_ResultsTemplates, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateResultsTemplates(int Id, ResultsTemplatesModel resultsTemplates)
        {
            resultsTemplates.RS_TMPLT_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            resultsTemplates.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ResultsTemplatesModel>(resultsTemplates);
            IEnumerable<ResultsTemplatesModel> result = await _datarepository.ExecuteQueryAsync<ResultsTemplatesModel>(SPConstant.Sp_ResultsTemplates, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteResultsTemplates(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "RS_TMPLT_ID", "QueryType");
            IEnumerable<ResultsTemplatesModel> result = await _datarepository.ExecuteQueryAsync<ResultsTemplatesModel>(SPConstant.Sp_ResultsTemplates, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<ResultsTemplatesModel>> GetAllResultsTemplates()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ResultsTemplatesModel>(SPConstant.Sp_ResultsTemplates, parameterCollection)).ToList();
        }

        public async Task<ResultsTemplatesModel> GetResultsTemplatesById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "TNO", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ResultsTemplatesModel>(SPConstant.Sp_ResultsTemplates, parameterCollection)).FirstOrDefault();

        }

        public async Task<int> InsertReportMainHeader(ReportMainHeaderModel reportMainHeader)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            reportMainHeader.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ReportMainHeaderModel>(reportMainHeader);
            await _datarepository.ExecuteQueryAsync(SPConstant.Sp_ReportMainHeader, parameterCollection);
            return 1;
        }

        public async Task<int> UpdateReportMainHeader(int Id, ReportMainHeaderModel reportMainHeader)
        {
            reportMainHeader.RPT_MHDR_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            reportMainHeader.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ReportMainHeaderModel>(reportMainHeader, "QueryType");
            IEnumerable<ReportMainHeaderModel> result = await _datarepository.ExecuteQueryAsync<ReportMainHeaderModel>(SPConstant.Sp_ReportMainHeader, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteReportMainHeader(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "RPT_MHDR_ID", "QueryType");
            IEnumerable<ReportMainHeaderModel> result = await _datarepository.ExecuteQueryAsync<ReportMainHeaderModel>(SPConstant.Sp_ReportMainHeader, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<ReportMainHeaderModel>> GetAllReportMainHeader()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ReportMainHeaderModel>(SPConstant.Sp_ReportMainHeader, parameterCollection)).ToList();
        }

        public async Task<ReportMainHeaderModel> GetReportMainHeaderById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "RPT_MHDR_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ReportMainHeaderModel>(SPConstant.Sp_ReportMainHeader, parameterCollection)).FirstOrDefault();
        }

        public async Task<int> InsertReportSubHeader(ReportSubHeaderModel reportSubHeader)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            reportSubHeader.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ReportSubHeaderModel>(reportSubHeader);
            await _datarepository.ExecuteQueryAsync(SPConstant.Sp_ReportSubHeader, parameterCollection);
            return 1;
        }

        public async Task<int> UpdateReportSubHeader(int Id, ReportSubHeaderModel reportSubHeader)
        {
            reportSubHeader.RPT_SHDR_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            reportSubHeader.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ReportSubHeaderModel>(reportSubHeader);
            IEnumerable<ReportSubHeaderModel> result = await _datarepository.ExecuteQueryAsync<ReportSubHeaderModel>(SPConstant.Sp_ReportSubHeader, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteReportSubHeader(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "RPT_SHDR_ID", "QueryType");
            IEnumerable<ReportSubHeaderModel> result = await _datarepository.ExecuteQueryAsync<ReportSubHeaderModel>(SPConstant.Sp_ReportSubHeader, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<ReportSubHeaderModel>> GetAllReportSubHeader()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ReportSubHeaderModel>(SPConstant.Sp_ReportSubHeader, parameterCollection)).ToList();
        }

        public async Task<ReportSubHeaderModel> GetReportSubHeaderById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "RPT_SHDR_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ReportSubHeaderModel>(SPConstant.Sp_ReportSubHeader, parameterCollection)).FirstOrDefault();

        }

        public async Task<int> InsertAccountManager(AccountManagerModel accountManager)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            accountManager.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<AccountManagerModel>(accountManager);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_AccountManager, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateAccountManager(int Id, AccountManagerModel accountManager)
        {
            accountManager.SALESMEN_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            accountManager.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<AccountManagerModel>(accountManager);
            IEnumerable<AccountManagerModel> result = await _datarepository.ExecuteQueryAsync<AccountManagerModel>(SPConstant.Sp_AccountManager, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteAccountManager(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "SALESMEN_ID", "QueryType");
            IEnumerable<AccountManagerModel> result = await _datarepository.ExecuteQueryAsync<AccountManagerModel>(SPConstant.Sp_AccountManager, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<AccountManagerModel>> GetAllAccountManager()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<AccountManagerModel>(SPConstant.Sp_AccountManager, parameterCollection)).ToList();
        }

        public async Task<AccountManagerModel> GetAccountManagerById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "SALESMEN_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<AccountManagerModel>(SPConstant.Sp_AccountManager, parameterCollection)).FirstOrDefault();
        }

        public async Task<int> InsertDriver(DriverModel driver)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            driver.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<DriverModel>(driver);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_Driver, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateDriver(int Id, DriverModel driver)
        {
            driver.DRVRS_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            driver.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<DriverModel>(driver);
            IEnumerable<DriverModel> result = await _datarepository.ExecuteQueryAsync<DriverModel>(SPConstant.Sp_Driver, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteDriver(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "DRVRS_ID", "QueryType");
            IEnumerable<DriverModel> result = await _datarepository.ExecuteQueryAsync<DriverModel>(SPConstant.Sp_Driver, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<DriverModel>> GetAllDriver()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<DriverModel>(SPConstant.Sp_Driver, parameterCollection)).ToList();
        }

        public async Task<DriverModel> GetDriverById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "SITE_DTL_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<DriverModel>(SPConstant.Sp_Driver, parameterCollection)).FirstOrDefault();
        }

        public async Task<int> InsertSite(SiteModel site)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            site.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SiteModel>(site);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_Site, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }


        public async Task<int> UpdateSite(int Id, SiteModel site)
        {
            site.SITE_DTL_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            site.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SiteModel>(site);
            IEnumerable<SiteModel> result = await _datarepository.ExecuteQueryAsync<SiteModel>(SPConstant.Sp_Site, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteSite(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "SITE_DTL_ID", "QueryType");
            IEnumerable<SiteModel> result = await _datarepository.ExecuteQueryAsync<SiteModel>(SPConstant.Sp_Site, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<SiteModel>> GetAllSite()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SiteModel>(SPConstant.Sp_Site, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<SiteModel>> GetSiteBySiteTP()
        {
            int queryType = (int)QueryTypeEnum.Search;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SiteModel>(SPConstant.Sp_Site, parameterCollection)).ToList();
        }


        public async Task<SiteModel> GetSiteById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "SITE_DTL_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SiteModel>(SPConstant.Sp_Site, parameterCollection)).FirstOrDefault();
        }


        public async Task<int> InsertSpecimenSite(SpecimenSiteModel specimenSite)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            specimenSite.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SpecimenSiteModel>(specimenSite);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_SpecimenSites, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateSpecimenSite(int Id, SpecimenSiteModel specimenSite)
        {
            specimenSite.SP_SITE_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            specimenSite.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<SpecimenSiteModel>(specimenSite);
            IEnumerable<SpecimenSiteModel> result = await _datarepository.ExecuteQueryAsync<SpecimenSiteModel>(SPConstant.Sp_SpecimenSites, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteSpecimenSite(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "SP_SITE_ID", "QueryType");
            IEnumerable<SpecimenSiteModel> result = await _datarepository.ExecuteQueryAsync<SpecimenSiteModel>(SPConstant.Sp_SpecimenSites, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<SpecimenSiteModel>> GetAllSpecimenSite()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SpecimenSiteModel>(SPConstant.Sp_SpecimenSites, parameterCollection)).ToList();
        }

        public async Task<SpecimenSiteModel> GetSpecimenSiteById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "SP_SITE_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SpecimenSiteModel>(SPConstant.Sp_SpecimenSites, parameterCollection)).FirstOrDefault();
        }

        public async Task<int> InsertPHStaff(PHStaffModel staff)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            staff.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PHStaffModel>(staff);
            await _datarepository.ExecuteQueryAsync(SPConstant.Sp_Phlebotomy, parameterCollection);
            return 1;
        }

        public async Task<int> UpdatePHStaff(int Id, PHStaffModel staff)
        {
            staff.PH_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            staff.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PHStaffModel>(staff);
            IEnumerable<PHStaffModel> result = await _datarepository.ExecuteQueryAsync<PHStaffModel>(SPConstant.Sp_Phlebotomy, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeletePHStaff(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "PH_ID", "QueryType");
            IEnumerable<PHStaffModel> result = await _datarepository.ExecuteQueryAsync<PHStaffModel>(SPConstant.Sp_Phlebotomy, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<PHStaffModel>> GetAllPHStaff()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<PHStaffModel>(SPConstant.Sp_Phlebotomy, parameterCollection)).ToList();
        }

        public async Task<PHStaffModel> GetPHStaffById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "PH_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<PHStaffModel>(SPConstant.Sp_Phlebotomy, parameterCollection)).FirstOrDefault();
        }

        public async Task<int> InsertResultType(ResultTypeModel resultType)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            resultType.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ResultTypeModel>(resultType);
            await _datarepository.ExecuteQueryAsync(SPConstant.Sp_ResultType, parameterCollection);
            return 1;
        }

        public async Task<int> UpdateResultType(int Id, ResultTypeModel resultType)
        {
            resultType.RESTYPE_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            resultType.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ResultTypeModel>(resultType);
            IEnumerable<ResultTypeModel> result = await _datarepository.ExecuteQueryAsync<ResultTypeModel>(SPConstant.Sp_ResultType, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteResultType(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "RESTYPE_ID", "QueryType");
            IEnumerable<ResultTypeModel> result = await _datarepository.ExecuteQueryAsync<ResultTypeModel>(SPConstant.Sp_ResultType, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<ResultTypeModel>> GetAllResultType()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ResultTypeModel>(SPConstant.Sp_ResultType, parameterCollection)).ToList();
        }

        public async Task<ResultTypeModel> GetResultTypeById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "RESTYPE_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ResultTypeModel>(SPConstant.Sp_ResultType, parameterCollection)).FirstOrDefault();
        }

        public async Task<int> InsertClient(ClientModel client)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            client.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ClientModel>(client);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_Clients, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateClient(int Id, ClientModel client)
        {
            client.CLNT_FL_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            client.QueryType = queryType;

            // Create the parameter list but exclude the SNO parameter
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator
                .CreateParameterList<ClientModel>(client, "QueryType")
                .Where(p => p.Name != nameof(client.SNO)) // Exclude 'SNO'
                .ToList();

            IEnumerable<ClientModel> result = await _datarepository.ExecuteQueryAsync<ClientModel>(
                SPConstant.Sp_Clients, parameterCollection);

            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public async Task<int> DeleteClient(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            var parameters = ParameterGenerator.CreateParameterList(Id, queryType, "CLNT_FL_ID ", "QueryType");
            IEnumerable<ClientModel> result = await _datarepository.ExecuteQueryAsync<ClientModel>(SPConstant.Sp_Clients, parameters);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<ClientModel>> GetAllClients()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ClientModel>(SPConstant.Sp_Clients, parameters)).ToList();
        }

        public async Task<ClientModel> GetClientById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "CLNT_FL_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ClientModel>(SPConstant.Sp_Clients, parameterCollection)).FirstOrDefault();
        }

        public async Task<ClientModel> GetClientByCN(string CN)//GET_CLNT_FL
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(CN, queryType, "CN", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ClientModel>(SPConstant.Sp_Clients, parameterCollection)).FirstOrDefault();
        }

        public async Task<int> InsertSpecialPricesFromSourceToDestination(List<SpecialPricesModel> specialPricesList)
        {

            // Ensure your DataTable column types match those expected by your TVP in SQL
            var dt = new DataTable();
            dt.Columns.Add("CLNT_SP_ID", typeof(int));
            dt.Columns.Add("DSCNT", typeof(decimal));
            dt.Columns.Add("DPRICE", typeof(decimal));
            dt.Columns.Add("DT", typeof(string));

            // Populate the DataTable from specialPricesList
            foreach (var x in specialPricesList)
            {
                dt.Rows.Add(x.CLNT_SP_ID, x.DSCNT, x.DPRICE, x.DT);
            };

            // Prepare parameters for stored procedure call
            IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>
            {
                new QueryParameterForSqlMapper
                {
                Name = "@SpecialPriceData",
                Value = dt.AsTableValuedParameter("ManageSpecialPrice_Type"), // Replace YourTVPTypeName with the actual SQL TVP type name
                DbType = DbType.Object // Specify the correct DbType for a TVP; adjust as necessary
                },
                 new QueryParameterForSqlMapper
                {
                Name = "@FROMCN",
                Value = specialPricesList[0].CN, // Replace YourTVPTypeName with the actual SQL TVP type name
                DbType = DbType.String // Specify the correct DbType for a TVP; adjust as necessary
                }, new QueryParameterForSqlMapper
                {
                Name = "@TOCN",
                Value = specialPricesList[0].TOCN,
                DbType = DbType.String // Specify the correct DbType for a TVP; adjust as necessary
                }
            };


            var result = await _datarepository.ExecuteQueryAsync<int>(SPConstant.Sp_CopyFromSourceToDestintation, parameterCollection).ConfigureAwait(false);


            return result.FirstOrDefault();
        }

        public async Task<int> DeleteSpecialPrices(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            var parameters = ParameterGenerator.CreateParameterList(Id, queryType, "CLNT_SP_ID ", "QueryType");
            IEnumerable<SpecialPricesModel> result = await _datarepository.ExecuteQueryAsync<SpecialPricesModel>(SPConstant.Sp_SpecialPrices, parameters);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<SpecialPricesModel>> GetAllTests()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SpecialPricesModel>(SPConstant.Sp_GetAllTests, parameters)).ToList();
        }

        public async Task<SpecialPricesModel> GetSpecialPricesByCode(int? Id, string code)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "CLNT_SP_ID", "QueryType");
            parameterCollection.Add(new QueryParameterForSqlMapper { Name = "CODE", Value = code });
            var results = await _datarepository.ExecuteQueryAsync<SpecialPricesModel>(SPConstant.Sp_SpecialPrices, parameterCollection);
            return results.FirstOrDefault();
        }

        public async Task<CLNT_SPModel> GetSpecialPricesByParams(string cn, string tcode)//GET_CLNT_SP
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(cn, queryType, "CN", "QueryType");
            parameterCollection.Add(new QueryParameterForSqlMapper { Name = "TCODE", Value = tcode });
            var results = await _datarepository.ExecuteQueryAsync<CLNT_SPModel>(SPConstant.Sp_SpecialPrices, parameterCollection);
            return results.FirstOrDefault();
        }


        public async Task<int> InsertAR(ARTemplateModel aRTemplate)
        {

            int queryType = (int)QueryTypeEnum.Insert;
            aRTemplate.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ARTemplateModel>(aRTemplate);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_ARTemplate, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateAR(ARTemplateModel aRTemplate)
        {
            int queryType = (int)QueryTypeEnum.Update;
            aRTemplate.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ARTemplateModel>(aRTemplate);
            IEnumerable<ARTemplateModel> result = await _datarepository.ExecuteQueryAsync<ARTemplateModel>(SPConstant.Sp_ARTemplate, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteAR(int AR_ID)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(AR_ID, queryType, "AR_ID", "QueryType");
            IEnumerable<ARTemplateModel> result = await _datarepository.ExecuteQueryAsync<ARTemplateModel>(SPConstant.Sp_ARTemplate, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<ARTemplateModel>> GetAllAR()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ARTemplateModel>(SPConstant.Sp_ARTemplate, parameterCollection)).ToList();
        }

        public async Task<ARTemplateModel> GetARById(int AR_ID)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(AR_ID, queryType, "AR_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ARTemplateModel>(SPConstant.Sp_ARTemplate, parameterCollection)).FirstOrDefault();

        }

        public async Task<int> InsertCNLCD(CNLCDModel CNLCD)
        {

            int queryType = (int)QueryTypeEnum.Insert;
            CNLCD.QueryType = queryType;
            IList<QueryParameterForSqlMapper> pCNLCDameterCollection = ParameterGenerator.CreateParameterList<CNLCDModel>(CNLCD);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_CNLCDTemplate, pCNLCDameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }
        public async Task<int> UpdateCNLCD(CNLCDModel CNLCD)
        {
            int queryType = (int)QueryTypeEnum.Update;
            CNLCD.QueryType = queryType;
            IList<QueryParameterForSqlMapper> pCNLCDameterCollection = ParameterGenerator.CreateParameterList<CNLCDModel>(CNLCD);
            IEnumerable<CNLCDModel> result = await _datarepository.ExecuteQueryAsync<CNLCDModel>(SPConstant.Sp_CNLCDTemplate, pCNLCDameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> DeleteCNLCD(int CNLCD_ID)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> pCNLCDameterCollection = ParameterGenerator.CreateParameterList(CNLCD_ID, queryType, "CNLCD_ID", "QueryType");
            IEnumerable<CNLCDModel> result = await _datarepository.ExecuteQueryAsync<CNLCDModel>(SPConstant.Sp_CNLCDTemplate, pCNLCDameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<CNLCDModel>> GetAllCNLCD() //GET_CNLCD
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> pCNLCDameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CNLCDModel>(SPConstant.Sp_CNLCDTemplate, pCNLCDameterCollection)).ToList();
        }
        public async Task<CNLCDModel> GetCNLCDById(int CNLCD_ID)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> pCNLCDameterCollection = ParameterGenerator.CreateParameterList(CNLCD_ID, queryType, "CNLCD_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CNLCDModel>(SPConstant.Sp_CNLCDTemplate, pCNLCDameterCollection)).FirstOrDefault();

        }

        public async Task<int> InsertMN(MNModel MN)
        {

            int queryType = (int)QueryTypeEnum.Insert;
            MN.QueryType = queryType;
            IList<QueryParameterForSqlMapper> pMNameterCollection = ParameterGenerator.CreateParameterList<MNModel>(MN);

            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_MNTemplate, pMNameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);

        }
        public async Task<int> UpdateMN(MNModel MN)
        {
            int queryType = (int)QueryTypeEnum.Update;
            MN.QueryType = queryType;
            IList<QueryParameterForSqlMapper> pMNameterCollection = ParameterGenerator.CreateParameterList<MNModel>(MN);
            IEnumerable<MNModel> result = await _datarepository.ExecuteQueryAsync<MNModel>(SPConstant.Sp_MNTemplate, pMNameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> DeleteMN(int MN_ID)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> pMNameterCollection = ParameterGenerator.CreateParameterList(MN_ID, queryType, "MN_ID", "QueryType");
            IEnumerable<MNModel> result = await _datarepository.ExecuteQueryAsync<MNModel>(SPConstant.Sp_MNTemplate, pMNameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<MNModel>> GetAllMN()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> pMNameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MNModel>(SPConstant.Sp_MNTemplate, pMNameterCollection)).ToList();
        }
        public async Task<MNModel> GetMNById(int MN_ID)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> pMNameterCollection = ParameterGenerator.CreateParameterList(MN_ID, queryType, "MN_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MNModel>(SPConstant.Sp_MNTemplate, pMNameterCollection)).FirstOrDefault();

        }

        public async Task<int> InsertMNI(MNIModel MNI)
        {

            int queryType = (int)QueryTypeEnum.Insert;
            MNI.QueryType = queryType;
            IList<QueryParameterForSqlMapper> pMNIameterCollection = ParameterGenerator.CreateParameterList<MNIModel>(MNI);

            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_MNITemplate, pMNIameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);

        }
        public async Task<int> UpdateMNI(MNIModel MNI)
        {
            int queryType = (int)QueryTypeEnum.Update;
            MNI.QueryType = queryType;
            IList<QueryParameterForSqlMapper> pMNIameterCollection = ParameterGenerator.CreateParameterList<MNIModel>(MNI);
            IEnumerable<MNIModel> result = await _datarepository.ExecuteQueryAsync<MNIModel>(SPConstant.Sp_MNITemplate, pMNIameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> DeleteMNI(int MNI_ID)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> pMNIameterCollection = ParameterGenerator.CreateParameterList(MNI_ID, queryType, "MNI_ID", "QueryType");
            IEnumerable<MNIModel> result = await _datarepository.ExecuteQueryAsync<MNIModel>(SPConstant.Sp_MNITemplate, pMNIameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<MNIModel>> GetAllMNI()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> pMNIameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MNIModel>(SPConstant.Sp_MNITemplate, pMNIameterCollection)).ToList();
        }
        public async Task<MNIModel> GetMNIById(int MNI_ID)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> pMNIameterCollection = ParameterGenerator.CreateParameterList(MNI_ID, queryType, "MNI_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MNIModel>(SPConstant.Sp_MNITemplate, pMNIameterCollection)).FirstOrDefault();

        }
        public async Task<IEnumerable<SpecialPricesModel>> GetSpecialpricesByClient(string FromCN, string ToCN)
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>
           {
               new QueryParameterForSqlMapper
               {
                   Name = "CN",
                   ParameterDirection = ParameterDirection.Input,
                   Value = FromCN,
                   DbType = DbType.String
               },
               new QueryParameterForSqlMapper
               {
                   Name = "toCN",
                   ParameterDirection = ParameterDirection.Input,
                   Value = ToCN,
                   DbType = DbType.String
               },
               new QueryParameterForSqlMapper
               {
                   Name = "QueryType",
                   ParameterDirection = ParameterDirection.Input,
                   Value =queryType,
                   DbType = DbType.Int32
               }
                };
            return (await _datarepository.ExecuteQueryAsync<SpecialPricesModel>(SPConstant.Sp_SpecialPrices, parameterCollection)).ToList();

        }

        public async Task<IEnumerable<CompanyModel>> GetAllCompany()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CompanyModel>(SPConstant.Sp_Company, parameters)).ToList();
        }
        public async Task<int> InsertEvsampletest(EVSampleTestModel evsampletest)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            evsampletest.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVSampleTestModel>(evsampletest);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_EVSampleTest, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateEvsampletest(int Id, EVSampleTestModel evsampletest)
        {
            evsampletest.EV_SMPLS_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            evsampletest.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVSampleTestModel>(evsampletest, "QueryType"); ;
            IEnumerable<EVSampleTestModel> result = await _datarepository.ExecuteQueryAsync<EVSampleTestModel>(SPConstant.Sp_EVSampleTest, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteEvsampletest(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            var parameters = ParameterGenerator.CreateParameterList(Id, queryType, "EV_SMPLS_ID ", "QueryType");
            IEnumerable<EVSampleTestModel> result = await _datarepository.ExecuteQueryAsync<EVSampleTestModel>(SPConstant.Sp_EVSampleTest, parameters);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<EVSampleTestModel>> GetAllEVSampletest()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVSampleTestModel>(SPConstant.Sp_EVSampleTest, parameters)).ToList();
        }

        #region Order Entry References
        public async Task<IEnumerable<ReferenceRangesModel>> GetReferenceRanges()
        {
            int queryType = (int)QueryTypeEnum.GetAll;

            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ReferenceRangesModel>("usp_ManageReferenceRanges", parameters)).ToList();
        }


        public async Task<int> InsertDoctor(DoctorFileModel doctorFileModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            doctorFileModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<DoctorFileModel>(doctorFileModel);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_ManageDoctors, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateDoctor(int Id, DoctorFileModel doctorFileModel)
        {
            doctorFileModel.DOC_FL_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            doctorFileModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<DoctorFileModel>(doctorFileModel);
            IEnumerable<DoctorFileModel> result = await _datarepository.ExecuteQueryAsync<DoctorFileModel>(SPConstant.Sp_ManageDoctors, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteDoctor(int DOC_FL_ID)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> pMNIameterCollection = ParameterGenerator.CreateParameterList(DOC_FL_ID, queryType, "DOC_FL_ID", "QueryType");
            IEnumerable<DoctorFileModel> result = await _datarepository.ExecuteQueryAsync<DoctorFileModel>(SPConstant.Sp_ManageDoctors, pMNIameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<DoctorFileModel>> GetAllDoctorFile()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<DoctorFileModel>(SPConstant.Sp_ManageDoctors, parameters)).ToList();
        }
        public async Task<DoctorFileModel> GetDoctorFileById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "DOC_FL_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<DoctorFileModel>(SPConstant.Sp_ManageDoctors, parameterCollection)).FirstOrDefault();
        }
        public async Task<DoctorFileModel> GetDoctorFileByDrNo(string DRNO)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(DRNO, queryType, "DRNO", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<DoctorFileModel>(SPConstant.Sp_ManageDoctors, parameterCollection)).FirstOrDefault();
        }

        public async Task<IEnumerable<LocationsFileModel>> GetAllLocationsFile()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<LocationsFileModel>(SPConstant.Sp_ManageLocationsFile, parameters)).ToList();
        }
        public async Task<LocationsFileModel> GetLocationsFileById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "LOCATION_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<LocationsFileModel>(SPConstant.Sp_ManageLocationsFile, parameterCollection)).FirstOrDefault();
        }
        public async Task<LocationsFileModel> GetLocationsFileByLoc(string LOC)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(LOC, queryType, "LOC", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<LocationsFileModel>(SPConstant.Sp_ManageLocationsFile, parameterCollection)).FirstOrDefault();
        }
        public async Task<IEnumerable<SharedTableModel>> GetSharedTable()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<SharedTableModel>(SPConstant.SP_GetSharedTable, parameters)).ToList();
        }
        #endregion

        //public async Task<int> InsertCopyfromClient(SpecialPrices sp)
        //{
        //    int queryType = (int)QueryTypeEnum.Insert;
        //    sp.QueryType = queryType;
        //    IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>
        //   {
        //       new QueryParameterForSqlMapper
        //       {
        //           Name = "fromCN",
        //           ParameterDirection = ParameterDirection.Input,
        //           Value = sp.CN,
        //           DbType = DbType.String
        //       },
        //       new QueryParameterForSqlMapper
        //       {
        //           Name = "toCN",
        //           ParameterDirection = ParameterDirection.Input,
        //           Value = sp.TOCN,
        //           DbType = DbType.String
        //       },
        //       new QueryParameterForSqlMapper
        //       {
        //           Name = "QueryType",
        //           ParameterDirection = ParameterDirection.Input,
        //           Value =queryType,
        //           DbType = DbType.Int32
        //       },
        //       // new QueryParameterForSqlMapper
        //       //{
        //       //    Name = "Data",
        //       //    ParameterDirection = ParameterDirection.Input,
        //       //    Value =sp.DATA,
        //       //    DbType = DbType.String
        //       //}
        //        };
        //    DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_SpecialPrices, parameterCollection);
        //    var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
        //    return await Task.FromResult(getData.returnData);

        //}
 
    }
}