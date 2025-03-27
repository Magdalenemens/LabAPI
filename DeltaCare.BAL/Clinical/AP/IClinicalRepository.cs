using DeltaCare.Entity.Model;

namespace DeltaCare.BAL.Clinical.AP
{
    public interface IClinicalRepository
    {
        #region AnatomicPathology
        Task<AnatomicModel> GetAnatomicPathologyById(int Id);
        Task<IEnumerable<AnatomicModel>> GetAllAnatomicPathology();
        Task<IEnumerable<ResultsTemplatesModel>> GetRTForAnatomyPathology();
        Task<ResultsTemplatesModel> GetRTForAnatomyPathologyById(int Id);
        Task<IEnumerable<ClinicalFindingModel>> GetAllClinicalFindings();
        Task<IEnumerable<ClinicalFindingModel>> GetClinicalFindingByAccessionNumber(string accessionnumber);
        Task<int> InsertPathFinding(PathFindingModel pathFinding);
        Task<int> UpdatePathFinding(int Id, PathFindingModel pathFinding);
        Task<int> DeleteClinicalFindingById(int Id);
        Task<IEnumerable<PathFindingModel>> GetAllPathFindingsByAxisType(string axisType);
        Task<IEnumerable<ClinicalFindingModel>> SearchAllPathFinding(string Query);
        #endregion

        #region
        Task<APReceivingModel> GetAPReceivingByAccessionNumber(string accessionnumber);
        Task<APReceivingModel> InsertAPReceiving(APReceivingModel aPReceiving);
        Task<int> UpdateAPReceiving(int Id, APReceivingModel aPReceiving);
        Task<IEnumerable<APReceivingModel>> GetAllAPReceiving();
        #endregion

        #region MicroBiology
        Task<MicroBiologyModel> GetMicroBiologyById(int Id);
        Task<IEnumerable<MicroBiologyModel>> GetAllMicroBiology();
        Task<IEnumerable<mbSearch>> GetAllMicroBiologySearch();
        Task<IEnumerable<ResultsTemplatesModel>> GetRTForMicroBiology();
        Task<ResultsTemplatesModel> GetRTForMicroBiologyById(int Id);
        Task<IEnumerable<MBIsolModel>> GetForMicroBiologyISolByArfId(int Id);

        Task<IEnumerable<MBIsolModel>> GetForMicroBiologyAllISol();
        Task<IEnumerable<MBIsolModel>> GetForMicroBiologySearchISol(string Search);
        Task<IEnumerable<MBSensitivityARModel>> GetForMicroBiologyARSensitivity();
        Task<IEnumerable<MBSensitivityModel>> GetForMicroBiologyAllSensitivity();
        Task<IEnumerable<MBSensitivityModel>> GetForMicroBiologySearchSensitivity(string Search);
        Task<IEnumerable<MBSensitivityModel>> GetForMicroBiologyAllGTDSensitivity();
        Task<int> InsertMicoBiologyIsol(List<MBIsolModel> mbIsolList);
        Task<int> InsertMicroBiologySensitivity(List<MBSensitivityModel> mbSensitivityModel);
        Task<IEnumerable<MBSensitivityModel>> GetForMicroBiologyAllSensitivityData(string Id);
        Task<IEnumerable<MicrobiologyListModel>> GetAllMicroBiologyList(MicrobiologySearchModel mbListSearch);
        Task<IEnumerable<MicrobiologyListModel>> GetAllMicroBiologyListQRcode(MicrobiologySearchModel mbListSearch);
        string GenerateQR(string Data);

        #endregion

        #region Cytogenetics
        Task<IEnumerable<CytogeneticsModel>> GetAllCytogenetics();
        Task<IEnumerable<rTestModel>> GetAllRTest();
        Task<int> InsertCytogeneticsTxtRes(List<TxtNameModel> txtNameModel);
        Task<IEnumerable<TxtNameModel>> getCgRTxtName(TxtNameModel txtNameModel);
        Task<IEnumerable<TxtNameModel>> getCgTxtNameByRes(TxtNameModel txtNameModel);
        Task<IEnumerable<CytogeneticLoginARModel>> GetCytogeneticLoginAR();
        Task<CytogeneticLoginModel> GetCytogeneticLogin(CytogeneticLoginModel cgLoginModel);
        Task<int> UpdateCytogeneticLogin(CytogeneticLoginModel cgLoginModel);
        Task<IEnumerable<CytogeneticListModel>> GetAllCytogeneticList(CytogeneticSearchModel mbListSearch);
        Task<CytogeneticsModel> GetCytogeneticsById(int Id);
       
        #endregion

        #region Environmental Result
        Task<IEnumerable<EVResultModel>> GetAllEnvironmentalResult(string pSize);

        Task<IEnumerable<EVResultARFModel>> GetAllEnvironmentalArfResult(string accn);
        Task<int> InsertEnvironmentalResult(List<EVResultARFModel> evResultModel);
        Task<int> UpdateEVRResultStatus(string UpdateEVRResultStatus, EVResultStatusModel evResultModel);

        Task<int> ResetEVRefRange(string RefRangeStatus, EVResultStatusModel evResultModel);
        Task<int> UpdateEnvironmentalResultInstrument(List<EVResultARFModel> evResultModel);
        Task<IEnumerable<evSearch>> GetAllEnvironmentalSearch(string OrderNo);
        Task<EVDetailModel> GetEnvironmentalDetails(EvResultDetailModel evResultModel);
        Task<IEnumerable<SignUserModel>> GetAllInstruments();

        #endregion
        #region Clinical Image

        Task<int> InsertClinicalImage(ClinicalImageModel clientImage);
        Task<IEnumerable<ClinicalImageModel>> GetClinincalImages(ClinicalImageModel clinicalImage);
        Task<int> DeleteClinicalImageById(int Id);
        #endregion


    }
}
