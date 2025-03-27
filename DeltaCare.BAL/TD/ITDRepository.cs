using DeltaCare.Entity.Model;
using static DeltaCare.Entity.Model.EVSetUpModel;

namespace DeltaCare.BAL
{
    public interface ITDRepository
    {
        Task<IEnumerable<TestDirectoryModel>> GetAllTestDirectory();
        Task<int> InsertTestDirectory(TestDirectoryModel objTd);
        Task<int> UpdateTestDirectory(int Id, TestDirectoryModel objTdModel);
        Task<int> UpdateTestDirectoryList(IEnumerable<PriceMasterListModel> testDirectoryList);
        Task<int> DeleteTestDirectory(int Id);
        Task<IEnumerable<TDComboModel>> GetTestDirectoryCombosByTable(string Tablename, string ID);
        Task<IEnumerable<TDModel>> GetTestDirectoryByTCode(string TCODE); //GET_TD
        Task<IEnumerable<TD_GTDModel>> GetGroupTestsDetailedandTestDirectory(string REQ_CODE);//GET_V_TD_GTD
        Task<IEnumerable<TD_GTDModel>> GetGroupTestsDetailedandTestDirectoryParams(string REQ_CODE, string TCODE);//GET_V_TD_GTD_TCODE
        Task<IEnumerable<TD_GTModel>> GetTestDirectoryandGroupTestsByTCODE(string TCODE);//usp_TD_GT


        #region AP Test Definition
        Task<int> InsertAPTestDefinition(APTestDefinitionModel apTestDefinitionModel);
        Task<int> UpdateAPTestDefinition(int Id, APTestDefinitionModel apTestDefinitionModel);
        Task<int> DeleteAPTestDefinition(int Id);
        Task<IEnumerable<APTestDefinitionModel>> GetAllAPTestDefinition();
        #endregion

        #region EV Test Definition and Profile
        Task<int> InsertEVTestDefinition(EVTestDefinitionModel apTestDefinitionModel);
        Task<int> UpdateEVTestDefinition(int Id, EVTestDefinitionModel apTestDefinitionModel);
        Task<int> DeleteEVTestDefinition(int Id);
        Task<IEnumerable<EVTestDefinitionModel>> GetAllEVTestDefinition();
        Task<EVTestDefinitionModel> GetEVDefinitionById(int id);
        Task<int> InserEVReferenceRanges(List<EVReferenceRangeModel> eVReferenceRangeModel);
        Task<int> UpdateEVReferenceRanges(List<EVReferenceRangeModel> eVReferenceRangeModel);
        Task<IEnumerable<EVReferenceRangeModel>> FetchEVTDByReferenceRange(string tCode);
        Task<EVReferenceRangeModel> FetchEVTDReferenceRangeBySType(string SType);
        Task<int> DeleteEVReferenceRange(int Id);
        Task<IEnumerable<EVReferenceRangeModel>> GetAllEVReferenceRange(string sType);
        //Task<IEnumerable<EVTestDefinitionModel>> GetAllEVTestDefinition(int PageNumber, int RowOfPage);        
        Task<int> InserEVTestDefinitionProfile(List<EVProfileGTDModel> eVProfileGTDModels);
        Task<int> UpdateEVTestDefinitionProfile(List<EVProfileGTDModel> eVProfileGTDModels);
        Task<IEnumerable<EVTestDefinitionProfileModel>> GetAllEVTestDefinitionProfile();
        Task<IEnumerable<EVProfileGTDModel>> FetchEVProfileFromGTD(string tCode);
        Task<EVProfileGTDModel> FetchProfileByGTDTCode(string gtdTCode);
        Task<IEnumerable<EVProfileGTDModel>> GetAllEVProfiles(string search);
        Task<int> DeleteEVProfile(int Id);
        Task<int> InsertAnlMethod(ANLMethodModel aNLMethod);
        Task<int> UpdateAnlMethod(int Id, ANLMethodModel aNLMethod);
        Task<int> DeleteAnlMethod(int Id);
        Task<IEnumerable<ANLMethodModel>> GetAllANLMethod();
        Task<ANLMethodModel> GetANLMethodById(int Id);
        Task<int> InsertEVSubHeader(EVSubHeaderModel eVSubHeader);
        Task<int> UpdateEVSubHeader(int Id, EVSubHeaderModel eVSubHeader);
        Task<int> DeleteEVSubHeader(int Id);
        Task<IEnumerable<EVSubHeaderModel>> GetAllEVSubHeader();
        Task<EVSubHeaderModel> GetEVSubHeaderById(int Id);
        #endregion

        #region CG Test Definition and Profile
        Task<int> InsertCGTestDefinition(CGTestDefinitionModel cgTestDefinitionModel);
        Task<int> UpdateCGTestDefinition(int Id, CGTestDefinitionModel cgTestDefinitionModel);
        Task<int> DeleteCGTestDefinition(int Id);
        Task<IEnumerable<CGTestDefinitionModel>> GetAllCGTestDefinition();
        Task<CGTestDefinitionModel> GetCGDefinitionById(int id);
        //Task<int> InserCGTestDefinitionProfile(List<CGProfileGTDModel> cgProfileGTDModels);
        //Task<int> UpdateCGTestDefinitionProfile(List<CGProfileGTDModel> cgProfileGTDModels);
        Task<(int RowsInserted, int RowsUpdated)> ManageCGTestDefinitionProfileAsync(List<CGProfileGTDModel> cgProfileGTDModels);
        Task<IEnumerable<CGTestDefinitionProfileModel>> GetAllCGTestDefinitionProfile();
        Task<IEnumerable<CGProfileGTDModel>> FetchCGProfileFromGTD(string tCode);
        Task<CGProfileGTDModel> FetchCGProfileByGTDTCode(string gtdTCode);
        Task<IEnumerable<CGProfileGTDModel>> GetAllCGProfiles(string search);
        Task<int> DeleteCGProfile(int Id);
        #endregion

        #region Test Directory Reference Range
        Task<IEnumerable<TDDivision>> GetAllTDDiv();
        Task<IEnumerable<TDDivision>> GetAllSectByDiv(int div);
        Task<IEnumerable<TDModel>> GetTestDirectory(int Div);
        Task<IEnumerable<TDModel>> GetAllTestsByDivision(int Div);
        Task<IEnumerable<TDReferenceRangeModel>> GetAllReferenceRange();
        Task<int> InsertReferenceRange(List<TDReferenceRangeModel> refRange);

        #endregion

        #region Test Directory - Profile
        Task<IEnumerable<TDProfileModel>> GetTestDirectoryProfile();
        Task<IEnumerable<TDGTDModel>> GetTestDirectoryGTD(string testId);
        Task<IEnumerable<TDProfileModel>> GetTestDirectoryByDProfile();
        Task<IEnumerable<TDProfileModel>> GetSearchTestDirectoryByDProfile(string Search);
        Task<int> InsertTestDirectoryProfile(List<GTDModel> tdProfileList);

        #endregion


    }
}
