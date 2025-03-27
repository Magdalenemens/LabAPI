using Dapper;
using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DbConnection = DeltaCare.DAL.DbConnection;
using QRCoder;
using System.Diagnostics;
using System.Linq;
namespace DeltaCare.BAL.Clinical.AP
{
    public class ClinicalRepository : IClinicalRepository
    {

        private readonly IDataRepository _datarepository;
        private DbConnection _dbconnection;

        public ClinicalRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
            _dbconnection = DbConnection.Instance;
        }

        #region AnatomicPathology
        public async Task<IEnumerable<AnatomicModel>> GetAllAnatomicPathology()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<AnatomicModel>(SPConstant.Sp_ManageAnatomicPathology, parameters)).ToList();
        }

        public async Task<AnatomicModel> GetAnatomicPathologyById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Id, queryType, "ORD_NO", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<AnatomicModel>(SPConstant.Sp_ManageAnatomicPathology, parameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<ResultsTemplatesModel>> GetRTForAnatomyPathology()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ResultsTemplatesModel>(SPConstant.Sp_ManageRTForAP, parameterCollection)).ToList();
        }

        public async Task<ResultsTemplatesModel> GetRTForAnatomyPathologyById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "RS_TMPLT_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ResultsTemplatesModel>(SPConstant.Sp_ManageRTForAP, parameterCollection)).FirstOrDefault();

        }

        public async Task<IEnumerable<ClinicalFindingModel>> GetAllClinicalFindings()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ClinicalFindingModel>(SPConstant.Sp_ManageClinicalFinding, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<ClinicalFindingModel>> GetClinicalFindingByAccessionNumber(string accessionnumber)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(accessionnumber, queryType, "ACCN", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ClinicalFindingModel>(SPConstant.Sp_ManageClinicalFinding, parameters)).ToList();
        }

        public async Task<int> InsertPathFinding(PathFindingModel pathFinding)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            pathFinding.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PathFindingModel>(pathFinding);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.Sp_ManageClinicalFinding, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdatePathFinding(int id, PathFindingModel pathFinding)
        {
            pathFinding.CLNCFNDG_ID = id;
            int queryType = (int)QueryTypeEnum.Update;
            pathFinding.QueryType = queryType;


            // Example of preparing parameters
            var parameterCollection = new List<QueryParameterForSqlMapper>
{
                new QueryParameterForSqlMapper { Name = "@QueryType", Value = queryType, DbType = DbType.Int32 },
    new QueryParameterForSqlMapper { Name = "@CLNCFNDG_ID", Value = id, DbType = DbType.Int32 },
    new QueryParameterForSqlMapper { Name = "@AX_NMBR", Value = pathFinding.AX_NMBR, DbType = DbType.String }
};

            // Debug printout
            foreach (var param in parameterCollection)
            {
                Console.WriteLine($"Parameter: {param.Name}, Value: {param.Value}, DbType: {param.DbType}");
            }

            IEnumerable<PathFindingModel> result = await _datarepository.ExecuteQueryAsync<PathFindingModel>(SPConstant.Sp_ManageClinicalFinding, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }



        public async Task<int> DeleteClinicalFindingById(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;

            var parameters = ParameterGenerator.CreateParameterList(Id, queryType, "CLNCFNDG_ID ", "QueryType");
            IEnumerable<ClinicalFindingModel> result = await _datarepository.ExecuteQueryAsync<ClinicalFindingModel>(SPConstant.Sp_ManagePathFinding, parameters);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<PathFindingModel>> GetAllPathFindingsByAxisType(string axisType)
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(axisType, queryType, "AX", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<PathFindingModel>(SPConstant.Sp_ManagePathFinding, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<ClinicalFindingModel>> SearchAllPathFinding(string Query)
        {
            int queryType = (int)QueryTypeEnum.Search;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Query, queryType, "Query", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ClinicalFindingModel>(SPConstant.Sp_ManagePathFinding, parameters)).ToList();
        }



        #endregion

        #region Anatomic Pathology Receiving

        public async Task<APReceivingModel> GetAPReceivingByAccessionNumber(string accessionNumber)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(accessionNumber, queryType, "ACCN", "QueryType");
            var result = await _datarepository.ExecuteQueryAsync<APReceivingModel>(SPConstant.Sp_ManageAPReceiving, parameters);

            // Assuming the query should return a single record
            return result.FirstOrDefault();
        }

        //public async Task<IEnumerable<CytogeneticLoginARModel>> GetAnatomicPathologyReceivingAR()
        //{
        //    int queryType = (int)QueryTypeEnum.GetAll;
        //    IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
        //    return (await _datarepository.ExecuteQueryAsync<CytogeneticLoginARModel>(SPConstant.Sp_ManageCytogeneticsLogin, parameters)).ToList();
        //}

        public async Task<APReceivingModel> InsertAPReceiving(APReceivingModel aPReceiving)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(aPReceiving.ACCN, queryType, "ACCN", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<APReceivingModel>(SPConstant.Sp_ManageAPReceiving, parameters)).FirstOrDefault();
        }


        public async Task<int> UpdateAPReceiving(int Id, APReceivingModel aPReceiving)
        {
            int queryType = (int)QueryTypeEnum.Update;

            // Create a list to hold the parameters for the SQL query
            var parameterCollection = new List<QueryParameterForSqlMapper>
    {
        new QueryParameterForSqlMapper { Name = "@AP_CASES_ID", Value = aPReceiving.AP_CASES_ID, DbType = DbType.Int32 },
        new QueryParameterForSqlMapper { Name = "@PTHGST_NAM", Value = aPReceiving.PTHGST_NAM, DbType = DbType.String },
        new QueryParameterForSqlMapper { Name = "@CLN_IND", Value = aPReceiving.CLN_IND, DbType = DbType.String },
        new QueryParameterForSqlMapper { Name = "@PRCVD_ID", Value = aPReceiving.PRCVD_ID, DbType = DbType.String },
        new QueryParameterForSqlMapper { Name = "@PRCVD_DTTM", Value = aPReceiving.PRCVD_DTTM, DbType = DbType.DateTime },
        new QueryParameterForSqlMapper { Name = "@ORD_NO", Value = aPReceiving.ORD_NO, DbType = DbType.String },
        new QueryParameterForSqlMapper { Name = "@REQ_CODE", Value = aPReceiving.REQ_CODE, DbType = DbType.String },
        new QueryParameterForSqlMapper { Name = "@ORD_DTL_ID", Value = aPReceiving.ORD_DTL_ID, DbType = DbType.Int32 },
        new QueryParameterForSqlMapper { Name = "@SITE_NO", Value = aPReceiving.SITE_NO, DbType = DbType.String },
        new QueryParameterForSqlMapper { Name = "@SECT", Value = aPReceiving.SECT, DbType = DbType.String },
        new QueryParameterForSqlMapper { Name = "@U_ID", Value = aPReceiving.U_ID, DbType = DbType.String },
        new QueryParameterForSqlMapper { Name = "@QueryType", Value = queryType, DbType = DbType.Int32 }
    };

            // Execute the query and process the result
            var result = await _datarepository.ExecuteQueryAsync<APReceivingModel>(SPConstant.Sp_ManageAPReceiving, parameterCollection);

            // Return 1 if the update is successful, otherwise 0
            return result != null && result.Any() ? 1 : 0;
        }


        public async Task<IEnumerable<APReceivingModel>> GetAllAPReceiving()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<APReceivingModel>(SPConstant.Sp_ManageAPReceiving, parameters)).ToList();

        }


        #endregion

        #region MicroBiology

        public async Task<IEnumerable<MicroBiologyModel>> GetAllMicroBiology()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MicroBiologyModel>(SPConstant.Sp_ManageMicroBiology, parameters)).ToList();
        }

        public async Task<IEnumerable<mbSearch>> GetAllMicroBiologySearch()
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<mbSearch>(SPConstant.Sp_ManageMicroBiology, parameters)).ToList();
        }



        public async Task<MicroBiologyModel> GetMicroBiologyById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Id, queryType, "ARF_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MicroBiologyModel>(SPConstant.Sp_ManageMicroBiology, parameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<ResultsTemplatesModel>> GetRTForMicroBiology()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ResultsTemplatesModel>(SPConstant.Sp_ManageRTForMB, parameterCollection)).ToList();
        }

        public async Task<ResultsTemplatesModel> GetRTForMicroBiologyById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "TNO", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ResultsTemplatesModel>(SPConstant.Sp_ManageRTForMB, parameterCollection)).FirstOrDefault();

        }

        public async Task<IEnumerable<MBIsolModel>> GetForMicroBiologyISolByArfId(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Id, queryType, "ARF_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MBIsolModel>(SPConstant.Sp_ManageMicroBiologyIsol, parameters)).ToList();
        }

        public async Task<IEnumerable<MBIsolModel>> GetForMicroBiologyAllISol()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MBIsolModel>(SPConstant.Sp_ManageMicroBiologyIsol, parameters)).ToList();
        }


        public async Task<IEnumerable<MBIsolModel>> GetForMicroBiologySearchISol(string Search)
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Search, queryType, "SEARCH", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MBIsolModel>(SPConstant.Sp_ManageMicroBiologyIsol, parameters)).ToList();
        }

        public async Task<IEnumerable<MBSensitivityARModel>> GetForMicroBiologyARSensitivity()
        {
            int queryType = (int)QueryTypeEnum.Update;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MBSensitivityARModel>(SPConstant.Sp_ManageMicroBiologyIsol, parameters)).ToList();
        }


        public async Task<IEnumerable<MBSensitivityModel>> GetForMicroBiologyAllSensitivity()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MBSensitivityModel>(SPConstant.Sp_ManageMicroBiologySensitivity, parameters)).ToList();
        }
        public async Task<IEnumerable<MBSensitivityModel>> GetForMicroBiologySearchSensitivity(string Search)
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Search, queryType, "SEARCH", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MBSensitivityModel>(SPConstant.Sp_ManageMicroBiologySensitivity, parameters)).ToList();
        }


        public async Task<IEnumerable<MBSensitivityModel>> GetForMicroBiologyAllGTDSensitivity()
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MBSensitivityModel>(SPConstant.Sp_ManageMicroBiologySensitivity, parameters)).ToList();
        }

        private int GetIsolCount()
        {
            int queryType = (int)QueryTypeEnum.Insert;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return _datarepository.ExecuteQueryAsync<int>(SPConstant.Sp_ManageMicroBiologyIsol, parameters).Result.ToList()[0];
        }

        private int DeleteIsolByOrderNo(string ArfId)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(ArfId, queryType, "@ARF_ID", "QueryType");
            return _datarepository.ExecuteQueryAsync<int>(SPConstant.Sp_ManageMicroBiologyIsol, parameters).Result.ToList()[0];
        }
        public async Task<int> InsertMicoBiologyIsol(List<MBIsolModel> mbIsolList)
        {
            int rowsAffected = 0;
            try
            {
                int iCounter = 0;
                List<MBIsolInsertModel> mBIsolInsertModels = new List<MBIsolInsertModel>();
                foreach (var isol in mbIsolList)
                {
                    var mbIsolInsertModel = new MBIsolInsertModel();
                    mbIsolInsertModel.ORD_NO = isol.ORD_NO;
                    mbIsolInsertModel.REQ_CODE = isol.REQ_CODE.TrimStart().TrimStart();
                    mbIsolInsertModel.NO = string.Empty;
                    mbIsolInsertModel.ISOL_CD = isol.ISOL_CD.TrimStart().TrimStart();
                    mbIsolInsertModel.R_STS = isol.R_STS.TrimStart().TrimEnd() == string.Empty ? "RS" : isol.R_STS;
                    mbIsolInsertModel.F = string.Empty;
                    mBIsolInsertModels.Add(mbIsolInsertModel);
                    iCounter++;
                }

                DeleteIsolByOrderNo(mbIsolList[0].ARF_ID.ToString());
                DataTable dtMbIsol = CommonHelper.ToDataTable(mBIsolInsertModels);
                if (dtMbIsol.Columns.Contains("QueryType"))
                    dtMbIsol.Columns.Remove("QueryType");

                rowsAffected = await _datarepository.ExecuteDataTable(SPConstant.Sp_InsertMicroBiologyIsol, dtMbIsol, SPConstant.type_InsertMBIsol);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }


        private int GetSensitivityCount()
        {
            int queryType = (int)QueryTypeEnum.Insert;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return _datarepository.ExecuteQueryAsync<int>(SPConstant.Sp_ManageMicroBiologySensitivity, parameters).Result.ToList()[0];
        }

        private int DeleteSensitivityByOrderNo(string ArfId)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(ArfId, queryType, "@ARF_ID", "QueryType");
            return _datarepository.ExecuteQueryAsync<int>(SPConstant.Sp_ManageMicroBiologySensitivity, parameters).Result.ToList()[0];
        }


        public async Task<int> InsertMicroBiologySensitivity(List<MBSensitivityModel> mbSensitivityModel)
        {
            int rowsAffected = 0;
            try
            {
                int iCounter = 0;
                List<MBSensitivityInsertModel> mBListSensitivityInsertModel = new List<MBSensitivityInsertModel>();
                foreach (var sen in mbSensitivityModel)
                {
                    var mbSensitivityInsertModel = new MBSensitivityInsertModel();
                    mbSensitivityInsertModel.ORD_NO = sen.ORD_NO;
                    mbSensitivityInsertModel.NO = string.Empty;
                    mbSensitivityInsertModel.REQ_CODE = sen.REQ_CODE.TrimStart().TrimStart();
                    mbSensitivityInsertModel.SREQ_CODE = sen.SREQ_CODE.TrimStart().TrimStart();
                    mbSensitivityInsertModel.ISOL_CD = sen.ISOLATE.TrimStart().TrimStart();
                    mbSensitivityInsertModel.TCODE = sen.TCODE.TrimStart().TrimStart();
                    mbSensitivityInsertModel.RESULT = sen.RESULT.TrimStart().TrimStart();
                    mbSensitivityInsertModel.SPRS = sen.SPRS;
                    mbSensitivityInsertModel.R_STS = sen.R_STS.TrimStart().TrimEnd() == string.Empty ? "RS" : sen.R_STS;
                    mbSensitivityInsertModel.AF = string.Empty; mbSensitivityInsertModel.MIC = sen.MIC;
                    if (sen.MIC != string.Empty || sen.RESULT != string.Empty)
                        mBListSensitivityInsertModel.Add(mbSensitivityInsertModel);
                    iCounter++;
                }
                DeleteSensitivityByOrderNo(mbSensitivityModel[0].ARF_ID.ToString());
                DataTable dtMbSensitivity = CommonHelper.ToDataTable(mBListSensitivityInsertModel);
                if (dtMbSensitivity.Columns.Contains("QueryType"))
                    dtMbSensitivity.Columns.Remove("QueryType");
                rowsAffected = await _datarepository.ExecuteDataTable(SPConstant.Sp_InsertMicroBiologySensitivity, dtMbSensitivity, SPConstant.type_InsertMBSensitivity);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }


        public async Task<IEnumerable<MBSensitivityModel>> GetForMicroBiologyAllSensitivityData(string Id)
        {
            int queryType = (int)QueryTypeEnum.Update;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Id, queryType, "@ARF_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<MBSensitivityModel>(SPConstant.Sp_ManageMicroBiologySensitivity, parameters)).ToList();
        }

        public async Task<IEnumerable<MicrobiologyListModel>> GetAllMicroBiologyList(MicrobiologySearchModel mbListSearch)
        {
            try
            {
                int queryType = (int)QueryTypeEnum.Delete;

                IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
                var listFilter = (await _datarepository.ExecuteQueryAsync<MicrobiologyListModel>(SPConstant.Sp_ManageMicroBiology, parameters)).ToList();

                if (mbListSearch.cn != string.Empty)
                {
                    listFilter = (from mb in listFilter
                                  where mb.CN.Contains(mbListSearch.cn) //&& (mb.SITE_NO == mbListSearch.sitE_NO)
                                  select mb).ToList();
                }
                if (mbListSearch.sitE_NO != string.Empty)
                {
                    listFilter = (from mb in listFilter
                                  where mb.SITE_NO.Contains(mbListSearch.sitE_NO) //&& (mb.SITE_NO == mbListSearch.sitE_NO)
                                  select mb).ToList();
                }

                if (mbListSearch.ordeR_FDTTM != string.Empty && mbListSearch.ordeR_TDTTM != string.Empty)
                {
                    DateTime dtFrom = Convert.ToDateTime(mbListSearch.ordeR_FDTTM);
                    DateTime dtTo = Convert.ToDateTime(mbListSearch.ordeR_TDTTM);
                    listFilter = (from mb in listFilter
                                  where DateTime.Parse(mb.ORDER_DTTMSTR).Date >= dtFrom.Date && DateTime.Parse(mb.ORDER_DTTMSTR).Date <= dtTo.Date
                                  select mb).ToList();
                }

                //return (await _datarepository.ExecuteQueryAsync<MicrobiologyListModel>(SPConstant.Sp_ManageMicroBiology, parameters)).ToList();
                return listFilter;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<MicrobiologyListModel>> GetAllMicroBiologyListQRcode(MicrobiologySearchModel mbListSearch)
        {
            try
            {
                int queryType = (int)QueryTypeEnum.Delete;

                IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
                var listFilter = (await _datarepository.ExecuteQueryAsync<MicrobiologyListModel>(SPConstant.Sp_ManageMicroBiology, parameters)).ToList();

                if (mbListSearch.cn != string.Empty)
                {
                    listFilter = (from mb in listFilter
                                  where mb.CN.Contains(mbListSearch.cn) //&& (mb.SITE_NO == mbListSearch.sitE_NO)
                                  select mb).ToList();
                }
                if (mbListSearch.sitE_NO != string.Empty)
                {
                    listFilter = (from mb in listFilter
                                  where mb.SITE_NO.Contains(mbListSearch.sitE_NO) //&& (mb.SITE_NO == mbListSearch.sitE_NO)
                                  select mb).ToList();
                }

                if (mbListSearch.ordeR_FDTTM != string.Empty && mbListSearch.ordeR_TDTTM != string.Empty)
                {
                    DateTime dtFrom = Convert.ToDateTime(mbListSearch.ordeR_FDTTM);
                    DateTime dtTo = Convert.ToDateTime(mbListSearch.ordeR_TDTTM);
                    listFilter = (from mb in listFilter
                                  where DateTime.Parse(mb.ORDER_DTTMSTR).Date >= dtFrom.Date && DateTime.Parse(mb.ORDER_DTTMSTR).Date <= dtTo.Date
                                  select mb).ToList();
                }

                var c = listFilter.Take(25);

                return c;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GenerateQR(string Data)
        {
            // Generate the QR code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(20);
            byte[] data;
            using (MemoryStream m = new MemoryStream())
            {
                qrCodeImage.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                data = m.ToArray();
            }

            return Convert.ToBase64String(data);
        }

        private void DisplayQRCodeImage(string imagePath)
        {
            try
            {
                // Check if the file exists
                if (System.IO.File.Exists(imagePath))
                {
                    // Use the default image viewer to open and display the QR code image
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = imagePath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                else
                {
                    Console.WriteLine("QR code image not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        #endregion

        #region Cytogenetics

        public async Task<IEnumerable<CytogeneticsModel>> GetAllCytogenetics()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CytogeneticsModel>(SPConstant.Sp_ManageCytogenetics, parameters)).ToList();
        }

        public async Task<IEnumerable<rTestModel>> GetAllRTest()
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<rTestModel>(SPConstant.Sp_ManageCytogenetics, parameters)).ToList();
        }

        public async Task<IEnumerable<TxtNameModel>> GetTxtNames()
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TxtNameModel>(SPConstant.Sp_ManageCytogenetics, parameters)).ToList();
        }

        private int DeleteCytogeneticsTxtRes(string ArfId)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(ArfId, queryType, "@ARF_ID", "QueryType");
            return _datarepository.ExecuteQueryAsync<int>(SPConstant.Sp_ManageCytogenetics, parameters).Result.ToList()[0];
        }


        public async Task<int> InsertCytogeneticsTxtRes(List<TxtNameModel> txtNameModel)
        {
            int rowsAffected = 0;
            try
            {
                int iCounter = 0;
                List<CGInsertTxtResModel> cgListCGInsertTxtResModel = new List<CGInsertTxtResModel>();
                foreach (var sen in txtNameModel)
                {
                    var cgInsertTxtresInsertModel = new CGInsertTxtResModel();
                    cgInsertTxtresInsertModel.ARFID = sen.R_ArfId;
                    cgInsertTxtresInsertModel.R_SEQ = sen.R_SEQ;
                    cgInsertTxtresInsertModel.R_NAME = sen.R_NAME;
                    cgInsertTxtresInsertModel.TXT_RES = sen.R_Result;
                    cgListCGInsertTxtResModel.Add(cgInsertTxtresInsertModel);
                    iCounter++;
                }
                DeleteCytogeneticsTxtRes(cgListCGInsertTxtResModel[0].ARFID.ToString());
                DataTable dtCgInstertTxtRes = CommonHelper.ToDataTable(cgListCGInsertTxtResModel);
                if (dtCgInstertTxtRes.Columns.Contains("QueryType"))
                    dtCgInstertTxtRes.Columns.Remove("QueryType");
                rowsAffected = await _datarepository.ExecuteDataTable(SPConstant.Sp_InsertCytogeneticTxtRes, dtCgInstertTxtRes, SPConstant.type_InsertCGTxtRes);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }


        public async Task<IEnumerable<TxtNameModel>> getCgRTxtName(TxtNameModel txtNameModel)
        {
            CGInsertParamModel cgParamModel = new CGInsertParamModel();
            cgParamModel.R_STS = txtNameModel.R_STS;
            int queryType = (int)QueryTypeEnum.GetAll;
            cgParamModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CGInsertParamModel>(cgParamModel, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TxtNameModel>(SPConstant.usp_ManageCytogeneticsTxtRes, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<TxtNameModel>> getCgTxtNameByRes(TxtNameModel txtNameModel)
        {
            CGInsertParamModel cgParamModel = new CGInsertParamModel();
            cgParamModel.ARF_ID = int.Parse(txtNameModel.R_ArfId);
            int queryType = (int)QueryTypeEnum.GetById;
            cgParamModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CGInsertParamModel>(cgParamModel, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TxtNameModel>(SPConstant.usp_ManageCytogeneticsTxtRes, parameterCollection)).ToList();
        }


        public async Task<IEnumerable<CytogeneticListModel>> GetAllCytogeneticList(CytogeneticSearchModel cgListSearch)
        {
            try
            {
                int queryType = (int)QueryTypeEnum.GetByName;

                IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
                var listFilter = (await _datarepository.ExecuteQueryAsync<CytogeneticListModel>(SPConstant.Sp_ManageCytogenetics, parameters)).ToList();

                if (cgListSearch.cn != string.Empty)
                {
                    listFilter = (from mb in listFilter
                                  where mb.CN.Contains(cgListSearch.cn) //&& (mb.SITE_NO == cgListSearch.sitE_NO)
                                  select mb).ToList();
                }
                if (cgListSearch.sitE_NO != string.Empty)
                {
                    listFilter = (from mb in listFilter
                                  where mb.SITE_NO.Contains(cgListSearch.sitE_NO) //&& (mb.SITE_NO == cgListSearch.sitE_NO)
                                  select mb).ToList();
                }

                if (cgListSearch.ordeR_FDTTM != string.Empty && cgListSearch.ordeR_TDTTM != string.Empty)
                {
                    DateTime dtFrom = Convert.ToDateTime(cgListSearch.ordeR_FDTTM);
                    DateTime dtTo = Convert.ToDateTime(cgListSearch.ordeR_TDTTM);
                    listFilter = (from mb in listFilter
                                  where DateTime.Parse(mb.ORDER_DTTMSTR).Date >= dtFrom.Date && DateTime.Parse(mb.ORDER_DTTMSTR).Date <= dtTo.Date
                                  select mb).ToList();
                }

                //return (await _datarepository.ExecuteQueryAsync<MicrobiologyListModel>(SPConstant.Sp_ManageMicroBiology, parameters)).ToList();
                return listFilter;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<CytogeneticsModel> GetCytogeneticsById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Id, queryType, "ARF_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CytogeneticsModel>(SPConstant.Sp_ManageCytogeneticsList, parameters)).FirstOrDefault();
        }



        #endregion

        #region Cytogenetics Login

        public async Task<IEnumerable<CytogeneticLoginARModel>> GetCytogeneticLoginAR()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CytogeneticLoginARModel>(SPConstant.Sp_ManageCytogeneticsReceiving, parameters)).ToList();
        }

        public async Task<CytogeneticLoginModel> GetCytogeneticLogin(CytogeneticLoginModel cgLoginModel)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(cgLoginModel.ACCN, queryType, "ACCN", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CytogeneticLoginModel>(SPConstant.Sp_ManageCytogeneticsReceiving, parameters)).FirstOrDefault();
        }


        public async Task<int> UpdateCytogeneticLogin(CytogeneticLoginModel cgLoginModel)
        {
            int queryType = (int)QueryTypeEnum.Update;
            cgLoginModel.QueryType = queryType;
            if (cgLoginModel.RCVD_DTTM != string.Empty)
                cgLoginModel.RCVD_DTTM = Convert.ToDateTime(cgLoginModel.RCVD_DTTM).ToString();
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CytogeneticLoginModel>(cgLoginModel, "QueryType");
            IEnumerable<CytogeneticLoginModel> result = await _datarepository.ExecuteQueryAsync<CytogeneticLoginModel>(SPConstant.Sp_ManageCytogeneticsReceiving, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region Environmental Result

        public async Task<IEnumerable<EVResultModel>> GetAllEnvironmentalResult(string pSize)
        {
            int queryType = (int)QueryTypeEnum.GetAll;

            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(pSize, queryType, "ACCN", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVResultModel>(SPConstant.SP_ManageEnvironmentalResult, parameterCollection)).ToList();
        }


        public async Task<IEnumerable<EVResultARFModel>> GetAllEnvironmentalArfResult(string accn)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(accn.Replace("-", ""), queryType, "ACCN", "QueryType");
            var S = await _datarepository.ExecuteQueryAsync<EVResultARFModel>(SPConstant.SP_ManageEnvironmentalResult, parameterCollection);
            return (S).ToList();
        }

        public async Task<int> InsertEnvironmentalResult(List<EVResultARFModel> evResultModel)
        {
            int rowsAffected = 0;
            try
            {
                foreach (var evResult in evResultModel)
                {
                    evResult.ACCN = evResult.ACCN.Replace("-", "");
                    if (evResult.UPDATETYPE == "SAVE")
                    {
                        if (evResult.RESULT != string.Empty && evResult.R_STS == "OD")
                            evResult.R_STS = "RS";
                    }
                    evResult.REQ_CODE = evResult.TCODE;
                }

                DataTable dtResult = CommonHelper.ToDataTable(evResultModel);
                if (dtResult.Columns.Contains("QueryType"))
                    dtResult.Columns.Remove("QueryType");
                if (dtResult.Columns.Contains("UPDATETYPE"))
                    dtResult.Columns.Remove("UPDATETYPE");
                if (dtResult.Columns.Contains("TCODE"))
                    dtResult.Columns.Remove("TCODE");
                if (dtResult.Columns.Contains("FULL_NAMEPLUS"))
                    dtResult.Columns.Remove("FULL_NAMEPLUS");
                rowsAffected = await _datarepository.ExecuteDataTable(SPConstant.SP_UpdateEnvironmentalResult, dtResult, SPConstant.type_UpdateEVResult);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }


        public async Task<int> UpdateEnvironmentalResultInstrument(List<EVResultARFModel> evResultModel)
        {
            int rowsAffected = 0;
            try
            {
                foreach (var evResult in evResultModel)
                {
                    evResult.ACCN = evResult.ACCN.Replace("-", "");
                    if (evResult.UPDATETYPE == "SAVE")
                    {
                        if (evResult.RESULT != string.Empty && evResult.R_STS == "OD")
                            evResult.R_STS = "RS";
                    }
                    evResult.REQ_CODE = evResult.TCODE;
                }

                DataTable dtResult = CommonHelper.ToDataTable(evResultModel);
                if (dtResult.Columns.Contains("QueryType"))
                    dtResult.Columns.Remove("QueryType");
                if (dtResult.Columns.Contains("UPDATETYPE"))
                    dtResult.Columns.Remove("UPDATETYPE");
                if (dtResult.Columns.Contains("TCODE"))
                    dtResult.Columns.Remove("TCODE");
                if (dtResult.Columns.Contains("FULL_NAMEPLUS"))
                    dtResult.Columns.Remove("FULL_NAMEPLUS");
                rowsAffected = await _datarepository.ExecuteDataTable(SPConstant.SP_UpdateEVResultInstrument, dtResult, SPConstant.type_UpdateEVResult);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }



        public async Task<int> UpdateEVRResultStatus(string accn, EVResultStatusModel evResultModel)
        {
            int queryType = (int)QueryTypeEnum.Update;
            evResultModel.QueryType = queryType;
            evResultModel.ACCN = evResultModel.ACCN.Replace("-", "");
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(evResultModel);
            IEnumerable<EVResultStatusModel> result = await _datarepository.ExecuteQueryAsync<EVResultStatusModel>(SPConstant.SP_UpdateEnvironmentResultStatus, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> ResetEVRefRange(string accn, EVResultStatusModel evResultModel)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            evResultModel.QueryType = queryType;
            evResultModel.ACCN = evResultModel.ACCN.Replace("-", "");
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(evResultModel);
            IEnumerable<EVResultStatusModel> result = await _datarepository.ExecuteQueryAsync<EVResultStatusModel>(SPConstant.SP_UpdateEnvironmentResultStatus, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<evSearch>> GetAllEnvironmentalSearch(string OrderNo)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(OrderNo, queryType, "@ORD_NO", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<evSearch>(SPConstant.SP_ManageEnvironmentalResult, parameters)).ToList();
        }

        public async Task<IEnumerable<evSearch>> GetEnvironmentalDetails()
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<evSearch>(SPConstant.SP_ManageEnvironmentalResult, parameters)).ToList();
        }

        public async Task<EVDetailModel> GetEnvironmentalDetails(EvResultDetailModel evResultModel)
        {
            evResultModel.ACCN = evResultModel.ACCN.Replace("-", "");
            evResultModel.QueryType = (int)QueryTypeEnum.Update;

            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EvResultDetailModel>(evResultModel, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVDetailModel>(SPConstant.SP_ManageEnvironmentalResult, parameterCollection)).FirstOrDefault();
        }


        public async Task<IEnumerable<SignUserModel>> GetAllInstruments()
        {
            int queryType = (int)QueryTypeEnum.Insert;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType,"QueryType");
            return (await _datarepository.ExecuteQueryAsync<SignUserModel>(SPConstant.SP_ManageEnvironmentalResult, parameterCollection)).ToList();
        }


        #endregion
        #region Clinical Image

        public async Task<int> InsertClinicalImage(ClinicalImageModel clinicalImage)
        {
            clinicalImage.QueryType = (int)QueryTypeEnum.Insert;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ClinicalImageModel>(clinicalImage);
            int result = _datarepository.ExecuteNonQuery(SPConstant.sp_ClinicalImageEntry, parameterCollection);
            return await Task.FromResult(result);

        }
        public async Task<IEnumerable<ClinicalImageModel>> GetClinincalImages(ClinicalImageModel clinicalImage)
        {

            int queryType = (int)QueryTypeEnum.GetAll;
            clinicalImage.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ClinicalImageModel>(clinicalImage, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ClinicalImageModel>(SPConstant.sp_ClinicalImageEntry, parameterCollection)).ToList();
        }

        public async Task<int> DeleteClinicalImageById(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;

            var parameters = ParameterGenerator.CreateParameterList(Id, queryType, "@APP_IMAGES_ID ", "QueryType");
            var result = _datarepository.ExecuteNonQuery(SPConstant.sp_ClinicalImageEntry, parameters);
            return result;
        }
        #endregion

    }
}