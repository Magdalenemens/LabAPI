using DeltaCare.Entity.Model;
using System.Collections.Generic;

namespace DeltaCare.BAL
{
    public interface IMasterRepository
    {
        Task<int> InsertSpecimentypes(SpecimentypeModel specimentype);
        Task<int> UpdateSpecimentypes(int Id, SpecimentypeModel specimentype);
        Task<int> DeleteSpecimentypes(int Id);
        Task<IEnumerable<SpecimentypeModel>> GetAllSpecimentypes();
        Task<SpecimentypeModel> GetSpecimentypesById(int Id);

        Task<int> InsertDivision(DivisionModel division);
        Task<int> UpdateDivision(int Id, DivisionModel division);
        Task<int> DeleteDivision(int Id);
        Task<IEnumerable<DivisionModel>> GetAllDivision();
        Task<DivisionModel> GetDivisionById(int Id);

        Task<int> InsertSection(SectionModel section);
        Task<int> UpdateSection(int Id, SectionModel section);
        Task<int> DeleteSection(int Id);
        Task<IEnumerable<SectionModel>> GetAllSection();
        Task<SectionModel> GetSectionById(int Id);

        Task<int> InsertWorkCenter(WorkCenterModel workCenter);
        Task<int> UpdateWorkCenter(int Id, WorkCenterModel workCenter);
        Task<int> DeleteWorkCenter(int Id);
        Task<IEnumerable<WorkCenterModel>> GetAllWorkCenter();
        Task<WorkCenterModel> GetWorkCenterById(int Id);

        Task<int> InsertTestSite(TestSiteModel testSite);
        Task<int> UpdateTestSite(int Id, TestSiteModel testSite);
        Task<int> DeleteTestSite(int Id);
        Task<IEnumerable<TestSiteModel>> GetAllTestSite();
        Task<TestSiteModel> GetTestSiteById(int Id);

        Task<int> InsertAccnPrefix(AccnPrefixModel accnPrefix);
        Task<int> UpdateAccnPrefix(int Id, AccnPrefixModel accnPrefix);
        Task<int> DeleteAccnPrefix(int Id);
        Task<IEnumerable<AccnPrefixModel>> GetAllAccnPrefix();
        Task<AccnPrefixModel> GetAccnPrefixById(int Id);

        Task<int> InsertResultsTemplates(ResultsTemplatesModel resultsTemplates);
        Task<int> UpdateResultsTemplates(int Id, ResultsTemplatesModel resultsTemplates);
        Task<int> DeleteResultsTemplates(int Id);
        Task<IEnumerable<ResultsTemplatesModel>> GetAllResultsTemplates();
        Task<ResultsTemplatesModel> GetResultsTemplatesById(int Id);

        Task<int> InsertReportMainHeader(ReportMainHeaderModel reportMainHeader);
        Task<int> UpdateReportMainHeader(int Id, ReportMainHeaderModel reportMainHeader);
        Task<int> DeleteReportMainHeader(int Id);
        Task<IEnumerable<ReportMainHeaderModel>> GetAllReportMainHeader();
        Task<ReportMainHeaderModel> GetReportMainHeaderById(int Id);

        Task<int> InsertReportSubHeader(ReportSubHeaderModel reportSubHeader);
        Task<int> UpdateReportSubHeader(int Id, ReportSubHeaderModel reportSubHeader);
        Task<int> DeleteReportSubHeader(int Id);
        Task<IEnumerable<ReportSubHeaderModel>> GetAllReportSubHeader();
        Task<ReportSubHeaderModel> GetReportSubHeaderById(int Id);

        Task<int> InsertAccountManager(AccountManagerModel accountManager);
        Task<int> UpdateAccountManager(int Id, AccountManagerModel accountManager);
        Task<int> DeleteAccountManager(int Id);
        Task<IEnumerable<AccountManagerModel>> GetAllAccountManager();
        Task<AccountManagerModel> GetAccountManagerById(int Id);

        Task<int> InsertDriver(DriverModel accountManager);
        Task<int> UpdateDriver(int Id, DriverModel accountManager);
        Task<int> DeleteDriver(int Id);
        Task<IEnumerable<DriverModel>> GetAllDriver();
        Task<DriverModel> GetDriverById(int Id);

        Task<int> InsertSite(SiteModel site);      
        Task<int> UpdateSite(int Id, SiteModel site);
        Task<int> DeleteSite(int Id);
        Task<IEnumerable<SiteModel>> GetAllSite();
        Task<IEnumerable<SiteModel>> GetSiteBySiteTP();
        Task<SiteModel> GetSiteById(int Id);       

        Task<int> InsertSpecimenSite(SpecimenSiteModel specimenSite);
        Task<int> UpdateSpecimenSite(int Id, SpecimenSiteModel specimenSite);
        Task<int> DeleteSpecimenSite(int Id);
        Task<IEnumerable<SpecimenSiteModel>> GetAllSpecimenSite();
        Task<SpecimenSiteModel> GetSpecimenSiteById(int Id);

        Task<int> InsertPHStaff(PHStaffModel staff);
        Task<int> UpdatePHStaff(int Id, PHStaffModel staff);
        Task<int> DeletePHStaff(int Id);
        Task<IEnumerable<PHStaffModel>> GetAllPHStaff();
        Task<PHStaffModel> GetPHStaffById(int Id);

        Task<int> InsertResultType(ResultTypeModel resultType);
        Task<int> UpdateResultType(int Id, ResultTypeModel resultType);
        Task<int> DeleteResultType(int Id);
        Task<IEnumerable<ResultTypeModel>> GetAllResultType();
        Task<ResultTypeModel> GetResultTypeById(int Id);

        Task<int> InsertClient(ClientModel client);
        Task<int> UpdateClient(int Id, ClientModel client);
        Task<int> DeleteClient(int Id);
        Task<IEnumerable<ClientModel>> GetAllClients();
        Task<ClientModel> GetClientById(int Id);
        Task<ClientModel> GetClientByCN(string CN);//GET_CLNT_FL

        Task<int> InsertSpecialPricesFromSourceToDestination(List<SpecialPricesModel> specialPricesList);
        Task<int> DeleteSpecialPrices(int Id);
        Task<IEnumerable<SpecialPricesModel>> GetAllTests();
        Task<SpecialPricesModel> GetSpecialPricesByCode(int? Id, string code);
        Task<CLNT_SPModel> GetSpecialPricesByParams(string cn, string tcode);//GET_CLNT_SP

        Task<int> InsertAR(ARTemplateModel ar);
        Task<int> UpdateAR(ARTemplateModel ar);
        Task<int> DeleteAR(int SP_TYPE_ID);
        Task<IEnumerable<ARTemplateModel>> GetAllAR();
        Task<ARTemplateModel> GetARById(int SP_TYPE_ID);

        Task<int> InsertCNLCD(CNLCDModel CNLCD);
        Task<int> UpdateCNLCD(CNLCDModel CNLCD);
        Task<int> DeleteCNLCD(int SP_TYPE_ID);
        Task<IEnumerable<CNLCDModel>> GetAllCNLCD();
        Task<CNLCDModel> GetCNLCDById(int SP_TYPE_ID);

        Task<int> InsertMN(MNModel MN);
        Task<int> UpdateMN(MNModel MN);
        Task<int> DeleteMN(int SP_TYPE_ID);
        Task<IEnumerable<MNModel>> GetAllMN();
        Task<MNModel> GetMNById(int SP_TYPE_ID);

        Task<int> InsertMNI(MNIModel MNI);
        Task<int> UpdateMNI(MNIModel MNI);
        Task<int> DeleteMNI(int SP_TYPE_ID);
        Task<IEnumerable<MNIModel>> GetAllMNI();
        Task<MNIModel> GetMNIById(int SP_TYPE_ID);

        Task<IEnumerable<SpecialPricesModel>> GetSpecialpricesByClient(string FromCN, string ToCN);
        Task<IEnumerable<CompanyModel>> GetAllCompany();
        Task<int> InsertEvsampletest(EVSampleTestModel evsampletest);
        Task<int> UpdateEvsampletest(int Id, EVSampleTestModel evsampletest);
        Task<int> DeleteEvsampletest(int Id);
        Task<IEnumerable<EVSampleTestModel>> GetAllEVSampletest();

        #region Order Entry
        Task<IEnumerable<ReferenceRangesModel>> GetReferenceRanges();
        Task<int> InsertDoctor(DoctorFileModel doctorFileModel);
        Task<int> UpdateDoctor(int Id, DoctorFileModel doctorFileModel);
        Task<int> DeleteDoctor(int DOC_FL_ID);
        Task<IEnumerable<DoctorFileModel>> GetAllDoctorFile();
        Task<DoctorFileModel> GetDoctorFileById(int Id);
        Task<DoctorFileModel> GetDoctorFileByDrNo(string DRNO);

        Task<IEnumerable<LocationsFileModel>> GetAllLocationsFile();
        Task<LocationsFileModel> GetLocationsFileById(int Id);
        Task<LocationsFileModel> GetLocationsFileByLoc(string LOC);
        Task<IEnumerable<SharedTableModel>> GetSharedTable();

        #endregion       
    }
}
