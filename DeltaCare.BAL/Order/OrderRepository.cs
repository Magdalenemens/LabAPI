using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Nodes;
using WordFileTest.NumberToArabicText;

//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeltaCare.BAL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDataRepository _dataRepository;
        private readonly ITDRepository _tDRepository;
        private readonly IGTRepository _gTRepository;
        private readonly IMasterRepository _masterRepository;
        private readonly IWebHostEnvironment _env;
        public OrderRepository(
            IDataRepository dataRepository,
             ITDRepository tDRepository,
             IGTRepository gTRepository,
             IMasterRepository masterRepository,
             IWebHostEnvironment env)
        {
            _dataRepository = dataRepository;
            _tDRepository = tDRepository;
            _gTRepository = gTRepository;
            _masterRepository = masterRepository;
            _env = env;
        }
        #region Get Order Transaction
        public async Task<IEnumerable<SiteModel>> GetRefSitefromSiteDetails(string SITE_NO)//ACCNPRFX
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, SITE_NO, "QueryType", "SITE_NO");
            return (await _dataRepository.ExecuteQueryAsync<SiteModel>(SPConstant.SP_GetRefSitefromSiteDetails, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<AccnPrefixModel>> GetAllAccessionPrefixes()//ACCNPRFX
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<AccnPrefixModel>(SPConstant.SP_GenerateAccnPrefix, parameterCollection)).ToList();
        }
        public async Task<int> UpdateAccessionPrefixesByOne(string prfx, AccnPrefixModel AccnPrefixModel)//UpdateACCNPRFX
        {
            int queryType = (int)QueryTypeEnum.Update;
            AccnPrefixModel.PRFX = prfx;
            AccnPrefixModel.QueryType = queryType;

            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<AccnPrefixModel>(AccnPrefixModel);
            IEnumerable<AccnPrefixModel> result = await _dataRepository.ExecuteQueryAsync<AccnPrefixModel>(SPConstant.SP_GenerateAccnPrefix, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<string> GenerateAccessionNumber(string REF_SITE, string prfx)
        {
            //string siteNo = "101";
            //string ref_site = REF_SITE;// "";

            //var refSitelist = GetRefSitefromSiteDetails(REF_LAB).Result.ToList();
            //foreach (var item in refSitelist)
            //{
            //    ref_site = item.REF_SITE;
            //}

            string accno = "";
            var GenerateAccessionNumber = GetAllAccessionPrefixes().Result
               .Where(x => x.PRFX == prfx && x.REF_SITE == REF_SITE).ToList();

            if (GenerateAccessionNumber == null)
                return null;

            //var dateToConvert = DateTime.Today; // Or any other date you want!
            //var stringResult = string.Format("{0}{1}", dateToConvert.ToString("yy"), dateToConvert.DayOfYear);

            string nxtacc = "";
            string accCurrentDate = "";
            string currentDate = "";
            string accCurrentYear = "";
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date.ToString("yyyy-MM-dd");
            currentDate = Convert.ToDateTime(date).ToString("yyyyMMdd");
            string currentYear = Convert.ToDateTime(date).ToString("yyyy");
            foreach (var item in GenerateAccessionNumber)
            {
                //string a = DateTime.Today.DayOfYear.ToString("000");
                accno = REF_SITE + item.DFLT_SET
                    .Replace("YY", DateTime.Now.Year.ToString("00").Substring(2, 2))
                    .Replace("XX", item.PRFX)
                    .Replace("MM", DateTime.Now.Month.ToString("00"))
                    //.Replace("DD", DateTime.Now.Day.ToString("00"));
                    .Replace("DDD", DateTime.Today.DayOfYear.ToString("000"));

                accCurrentDate = Convert.ToDateTime(item.CUR_DATE).ToString("yyyyMMdd");
                accCurrentYear = Convert.ToDateTime(item.CUR_DATE).ToString("yyyy");
                if (accCurrentDate.Equals(currentDate) && item.PRFX == "JD")
                {
                    //nxtacc = (Convert.ToInt16(item.NEXTACCN) + 1).ToString($"{item.LASTYRACCN}");
                    //accno += (Convert.ToInt16(item.NEXTACCN) + 1).ToString($"{item.LASTYRACCN}");
                    nxtacc = (Convert.ToInt16(item.NEXTACCN)).ToString($"{item.LASTYRACCN}");
                    accno += (Convert.ToInt16(item.NEXTACCN)).ToString($"{item.LASTYRACCN}");
                }
                else if (accCurrentYear.Equals(currentYear) && item.PRFX != "JD")
                {
                    //nxtacc = (Convert.ToInt16(item.NEXTACCN) + 1).ToString($"{item.LASTYRACCN}");
                    //accno += (Convert.ToInt16(item.NEXTACCN) + 1).ToString($"{item.LASTYRACCN}");
                    nxtacc = (Convert.ToInt16(item.NEXTACCN)).ToString($"{item.LASTYRACCN}");
                    accno += (Convert.ToInt16(item.NEXTACCN)).ToString($"{item.LASTYRACCN}");
                }
                else
                {
                    // nxtacc = (Convert.ToInt16(0) + 1).ToString($"{item.LASTYRACCN}");//Adding 1
                    //accno += (Convert.ToInt16(0) + 1).ToString($"{item.LASTYRACCN}");
                    nxtacc = (Convert.ToInt16(0) + 1).ToString($"{item.LASTYRACCN}");
                    accno += (Convert.ToInt16(0) + 1).ToString($"{item.LASTYRACCN}");
                }
            }


            AccnPrefixModel AccnPrefixModel = new AccnPrefixModel();
            AccnPrefixModel.NEXTACCN = !string.IsNullOrEmpty(nxtacc) ? (Convert.ToInt16(nxtacc) + 1).ToString() : (Convert.ToInt16(AccnPrefixModel.NEXTACCN) + 1).ToString();
            AccnPrefixModel.CUR_DATE = Convert.ToDateTime(date);
            AccnPrefixModel.CUR_YEAR = DateTime.Now.Year;
            AccnPrefixModel.LASTYRACCN = "";
            AccnPrefixModel.DFLT_SET = "";
            AccnPrefixModel.REF_SITE = REF_SITE;
            int updatedValue = await UpdateAccessionPrefixesByOne(prfx, AccnPrefixModel);
            return accno;// $"{accno}";
        }
        public async Task<IEnumerable<ORD_TRNSModel>> GetLastOrdersTransactions()//GET_NEW_ORD_NO
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<ORD_TRNSModel>(SPConstant.SP_GetLastOrdersTransactions, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<ORD_TRNSModel>> GetOrdersTransactionsByParams(string PAT_ID, ORD_TRNSModel oRD_TRNSModel)//GET_ORD_TRNS
        {
            oRD_TRNSModel.PAT_ID = PAT_ID;
            int queryType = (int)QueryTypeEnum.GetById;
            oRD_TRNSModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(oRD_TRNSModel, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<ORD_TRNSModel>(SPConstant.SP_ManageOrdersTransactions, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<ORD_TRNSModel>> GetAllOrdersTransactions()//Get All Orders Transactions
        {
            ORD_TRNSModel oRD_TRNSModel = new ORD_TRNSModel();
            int queryType = (int)QueryTypeEnum.GetAll;
            oRD_TRNSModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(oRD_TRNSModel, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<ORD_TRNSModel>(SPConstant.SP_ManageOrdersTransactions, parameterCollection)).ToList();
        }


        public async Task<IEnumerable<v_ORD_TRANSModel>> GetOrdersTransactionsDetailsByParams(string PAT_ID, string ORD_NO)//GET_v_ORD_TRANS
        {
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(PAT_ID, ORD_NO, "PAT_ID", "ORD_NO");
            return (await _dataRepository.ExecuteQueryAsync<v_ORD_TRANSModel>(SPConstant.SP_GetOrderTransactionsByJoin, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<ATRModel>> GetActiveTestsRequestByParams(string PAT_ID, string ORD_NO)//GET_ATR
        {
            ATRModel aTRModel = new ATRModel();
            int queryType = (int)QueryTypeEnum.GetById;
            aTRModel.QueryType = queryType;
            aTRModel.PAT_ID = PAT_ID;
            aTRModel.ORD_NO = ORD_NO;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ATRModel>(aTRModel);
            return (await _dataRepository.ExecuteQueryAsync<ATRModel>(SPConstant.SP_ManageActiveTestsRequest, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<ORD_DTLModel>> GetOrdersDetailsByParams(string PAT_ID, string ORD_NO)//GET_Ord_Dtl
        {
            ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
            int queryType = (int)QueryTypeEnum.GetById;
            oRD_DTLModel.QueryType = queryType;
            oRD_DTLModel.PAT_ID = PAT_ID;
            oRD_DTLModel.ORD_NO = ORD_NO;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ORD_DTLModel>(oRD_DTLModel);
            return (await _dataRepository.ExecuteQueryAsync<ORD_DTLModel>(SPConstant.SP_ManageOrdersDetails, parameterCollection)).ToList();
            // return (await _dataRepository.ExecuteQueryAsync<ORD_DTLModel>("usp_GetOrderDetails", parameterCollection)).ToList();
        }
        public async Task<IEnumerable<p_ORD_DTL_TD_GTModel>> GetOrdersDetailsByUnion(string PAT_ID, string ORD_NO)//GET_p_ORD_DTL_TD_GT
        {
            //p_ORD_DTL_TD_GTModel p_orD_DTL_TD_GTModel = new p_ORD_DTL_TD_GTModel();
            //int queryType = (int)QueryTypeEnum.GetById;
            //p_orD_DTL_TD_GTModel.QueryType = queryType;
            //p_orD_DTL_TD_GTModel.PAT_ID = PAT_ID;
            //p_orD_DTL_TD_GTModel.ORD_NO = ORD_NO;
            //IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<p_ORD_DTL_TD_GTModel>(p_orD_DTL_TD_GTModel);
            //return (await _dataRepository.ExecuteQueryAsync<p_ORD_DTL_TD_GTModel>(SPConstant.SP_GetOrdersDetailsByUnion, parameterCollection)).ToList();
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(PAT_ID, ORD_NO, "PAT_ID", "ORD_NO");
            return (await _dataRepository.ExecuteQueryAsync<p_ORD_DTL_TD_GTModel>(SPConstant.SP_GetOrdersDetailsByUnion, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<ORD_TPModel>> GetAllOrderType()//GET_Ord_TP
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<ORD_TPModel>(SPConstant.SP_ManageOrderType, parameterCollection)).ToList();

        }
        public async Task<IEnumerable<ORD_TRCModel>> GetOrderTrackingByOrdNo(string ORD_NO)//GET_Ord_TP
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(ORD_NO, queryType, "ORD_NO", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<ORD_TRCModel>(SPConstant.SP_ManageOrdersTracking, parameterCollection)).ToList();

        }

        public async Task<IEnumerable<ORD_TRCModel>> GetOrderTrackingByReqCode(string REQ_CODE)//GET_Ord_TP
        {
            int queryType = (int)QueryTypeEnum.Search;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(REQ_CODE, queryType, "REQ_CODE", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<ORD_TRCModel>(SPConstant.SP_ManageOrdersTracking, parameterCollection)).ToList();

        }
        public async Task<IEnumerable<ARFModel>> GetActiveResultFileDuringCollection(string ORD_NO, string REQ_CODE, int ATRID)
        {
            ARFModel aRFModel = new ARFModel();
            aRFModel.ORD_NO = ORD_NO;
            aRFModel.REQ_CODE = REQ_CODE;
            aRFModel.ATRID = ATRID;
            int queryType = (int)QueryTypeEnum.Search;
            aRFModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(aRFModel, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<ARFModel>(SPConstant.SP_ManageActiveResultsFile, parameterCollection)).ToList();
        }
        #endregion

        #region Order Entry Transactions
        public async Task<int> InsertOrdersTransactions(ORD_TRNSModel oRD_TRNSModel)//ADD_ORD_TRNS
        {
            int queryType = (int)QueryTypeEnum.Insert;
            oRD_TRNSModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ORD_TRNSModel>(oRD_TRNSModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.SP_ManageOrdersTransactions, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> InsertActiveTestsRequest(Object[] ATRs, string SEX, string CN, DateTime DOB, string DRNO, string LOC, string PRFX, string CLN_IND, string ORD_NO)//ADD_ATR
        {
            BadRequestResult badresult = new BadRequestResult();
            ATRModel aTRModel = new ATRModel();
            var ATR = ATRs.ToList();
            var ACCN = "";
            var PRFX_ = PRFX;
            PRFX = "";
            var REF_SITE = "";
            int CNTOrderSeq = 0;
            foreach (var objATRs in ATR)
            {
                string jsonString = JsonSerializer.Serialize(objATRs);
                var atrJson = JsonObject.Parse(jsonString);
                aTRModel.REQ_CODE = atrJson["reQ_CODE"].ToString();
                aTRModel.DPRICE = Convert.ToDecimal(atrJson["dprice"].ToString());
                aTRModel.DRAWN_DTTM = (atrJson["drawN_DTTM"].ToString() == null || atrJson["drawN_DTTM"].ToString() == "") ? null : Convert.ToDateTime(atrJson["drawN_DTTM"].ToString());
                aTRModel.DSCNT = Convert.ToDecimal(atrJson["dscnt"].ToString());
                aTRModel.DT = atrJson["dt"].ToString();
                aTRModel.FULL_NAME = atrJson["fulL_NAME"].ToString();
                aTRModel.LN = atrJson["ln"].ToString();
                aTRModel.R_STS = atrJson["r_STS"].ToString();
                aTRModel.S_TYPE = atrJson["s_TYPE"].ToString();
                aTRModel.STS = atrJson["sts"].ToString();
                aTRModel.UPRICE = Convert.ToDecimal(atrJson["uprice"].ToString());
                aTRModel.PAT_ID = atrJson["paT_ID"].ToString();
                aTRModel.ORD_NO = atrJson["orD_NO"].ToString() == "" ? ORD_NO : atrJson["orD_NO"].ToString();
                //aTRModel.GTNO = atrJson["gtno"].ToString();
                aTRModel.TEST_ID = atrJson["tesT_ID"].ToString();

                aTRModel.O_ID = atrJson["o_ID"].ToString();
                aTRModel.SECT = atrJson["sect"].ToString();
                aTRModel.CT = atrJson["ct"].ToString();

                aTRModel.SITE_NO = atrJson["sitE_NO"].ToString();
                aTRModel.ACCN = atrJson["accn"].ToString();//If the add test is triggered
                //}

                //Count the Order Test
                CNTOrderSeq += 1;

                //Get Site Number and ref site
                var getSITE_DTL = GetRefSitefromSiteDetails(aTRModel.SITE_NO).Result.FirstOrDefault();
                if (getSITE_DTL == null)
                    REF_SITE = "01";
                else
                    REF_SITE = getSITE_DTL.REF_SITE;

                //end

                //if (aTRModel == null)
                //    return badresult.StatusCode;
                if (string.IsNullOrEmpty(aTRModel.PAT_ID)) return badresult.StatusCode;

                /*Generate Accession Number disabled due to add test is implemented*/
                //aTRModel.ACCN = "";// await GenerateAccessionNumber(PRFX_);
                ACCN = aTRModel.ACCN;
                aTRModel.REQ_DTTM = DateTime.Now;


                int queryType = (int)QueryTypeEnum.Insert;
                aTRModel.QueryType = queryType;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ATRModel>(aTRModel);
                DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.SP_ManageActiveTestsRequest, parameterCollection);
                var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();

                //return Convert.ToInt32( getData);
                //Compute for the Invoice Total
                var GET_ATR = GetActiveTestsRequestByParams(aTRModel.PAT_ID, aTRModel.ORD_NO).Result;//.Result
                                                                                                     //.Where(x => x.PAT_ID == aTRModel.PAT_ID && x.ORD_NO == aTRModel.ORD_NO)
                                                                                                     //.OrderBy(x => x.LN).ToList();

                decimal TOT_VALUE = 0;
                decimal NET_VALUE = 0;
                decimal DSCAMNT = 0;
                decimal TOTDSCNT = 0;
                decimal GRAND_VAL = 0;
                foreach (var itemATR in GET_ATR)
                {
                    TOT_VALUE += Convert.ToDecimal(itemATR.UPRICE);
                    NET_VALUE += Convert.ToDecimal(itemATR.DPRICE);
                }
                DSCAMNT = TOT_VALUE - NET_VALUE;
                TOTDSCNT = (DSCAMNT / TOT_VALUE) * 100;

                GRAND_VAL = NET_VALUE;

                ORD_TRNSModel oRD_TRNSModel = new ORD_TRNSModel();
                oRD_TRNSModel.PAT_ID = aTRModel.PAT_ID;
                oRD_TRNSModel.ORD_NO = aTRModel.ORD_NO;
                oRD_TRNSModel.TOT_VALUE = TOT_VALUE;
                oRD_TRNSModel.NET_VALUE = NET_VALUE;
                oRD_TRNSModel.DSCAMNT = DSCAMNT;
                oRD_TRNSModel.TOTDSCNT = TOTDSCNT;
                oRD_TRNSModel.CLN_IND = CLN_IND;
                oRD_TRNSModel.GRAND_VAL = GRAND_VAL;

                await UpdateOrdersTransactions(oRD_TRNSModel);


                var ATR_ID = getData.returnData;// aTRModel.ATR_ID; //Get ATR_ID

                TDModel tDModel = new TDModel();
                tDModel.TCODE = aTRModel.REQ_CODE;
                aTRModel.ATR_ID = ATR_ID; //Set ATR ID for all tables related to Ordeers Transactions

                var GET_TD = _tDRepository.GetTestDirectoryByTCode(tDModel.TCODE).Result.FirstOrDefault(x => x.CT == "D");
                if (GET_TD == null)
                {
                    #region Search for the GTD and TD for the Subcomponents Super and Group Compnent

                    GTDModel gTDModel = new GTDModel();
                    //gTDModel.GTNO = aTRModel.GTNO;
                    gTDModel.GTNO = aTRModel.TEST_ID;
                    gTDModel.REQ_CODE = aTRModel.REQ_CODE;

                    if (gTDModel == null)
                        return badresult.StatusCode;

                    // var GET_GTD = (Object)null;


                    var GET_GT = _tDRepository.GetTestDirectoryByTCode(gTDModel.REQ_CODE).Result.FirstOrDefault();
                    //var GET_GT = _gTRepository.GetGroupTestsByReqCode(gTDModel.REQ_CODE).Result.FirstOrDefault();
                    //.FirstOrDefaultAsync(x => x.REQ_CODE == gTDModel.REQ_CODE);
                    //.Where(x => x.REQ_CODE == gTDModel.REQ_CODE && x.GTNO == gTDModel.GTNO)
                    //.OrderBy(x => x.GT_ID).ToListAsync();

                    #region Check if GP is S, G and D/Null


                    //if (GET_GT.GP == "G")
                    if (GET_GT.CT == "G")
                    {
                        if (PRFX != GET_GT.PRFX)
                        {
                            PRFX = GET_GT.PRFX;
                            //ACCN = "";// await GenerateAccessionNumber(GET_GT.PRFX);
                        }


                        ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
                        oRD_DTLModel.ORD_SEQ = CNTOrderSeq.ToString("00");
                        oRD_DTLModel.SITE_NO = aTRModel.SITE_NO;
                        oRD_DTLModel.PAT_ID = aTRModel.PAT_ID;
                        oRD_DTLModel.ORD_NO = aTRModel.ORD_NO;
                        oRD_DTLModel.ACCN = ACCN;// aTRModel.ACCN;
                                                 //oRD_DTLModel.ORD_CODE = aTRModel.SITE_NO;
                        oRD_DTLModel.REQ_CODE = GET_GT.TCODE;// GET_GT.REQ_CODE;
                        //oRD_DTLModel.DTNO = GET_GT.DTNO;
                        oRD_DTLModel.TEST_ID = GET_GT.TEST_ID;
                        //oRD_DTLModel.GTNO = GET_GT.GTNO;
                        oRD_DTLModel.CT = "D";// itemV_TD_GTD.CT;
                        oRD_DTLModel.REQ_DTTM = aTRModel.REQ_DTTM;
                        oRD_DTLModel.O_ID = aTRModel.O_ID;
                        oRD_DTLModel.DRAWN_DTTM = aTRModel.DRAWN_DTTM;
                        oRD_DTLModel.TRNS_DTTM = DateTime.Now;
                        oRD_DTLModel.PRTY = GET_GT.PRTY;
                        oRD_DTLModel.STS = (aTRModel.STS == "" || aTRModel.STS == "RT") ? "OD" : aTRModel.STS;// itemV_TD_GTD.STS;
                        oRD_DTLModel.R_STS = aTRModel.R_STS == "" ? "O" : aTRModel.R_STS;//itemV_TD_GTD.SITE_NO;
                        oRD_DTLModel.MDL = GET_GT.MDL;
                        oRD_DTLModel.DIV = GET_GT.DIV;
                        oRD_DTLModel.SECT = GET_GT.SECT;
                        //oRD_DTLModel.WC = GET_GT.WC;
                        oRD_DTLModel.TS = GET_GT.TS;
                        //oRD_DTLModel.X_ID = itemV_TD_GTD.X_ID;
                        //oRD_DTLModel.CNLD = itemV_TD_GTD.SITE_NO;
                        oRD_DTLModel.ATRID = aTRModel.ATR_ID;
                        oRD_DTLModel.REF_SITE = ACCN != "" ? REF_SITE : "";
                        // await ADD_Ord_Dtl(oRD_DTLModel);
                        await InsertOrdersDetails(oRD_DTLModel);

                        //var GET_V_TD_GTD = _tDRepository.GetGroupTestsDetailedandTestDirectory(GET_GT.REQ_CODE).Result
                        //            .Where(x => x.REQ_CODE == GET_GT.REQ_CODE && x.GTNO == GET_GT.GTNO).ToList();
                        var GET_V_TD_GTD = _tDRepository.GetGroupTestsDetailedandTestDirectory(GET_GT.TCODE).Result
                                   .Where(x => x.REQ_CODE == GET_GT.TCODE && x.GTNO == GET_GT.TEST_ID).ToList();



                        //var GET_V_TD_GTD1 = await _appDbContext.V_TD_GTD
                        //            .Where(x => x.REQ_CODE == GET_GT.REQ_CODE && x.GTNO == GET_GT[0].GTNO).ToListAsync();

                        foreach (var itemV_TD_GTD in GET_V_TD_GTD)
                        {

                            //and ARF
                            ARFModel aRFModel = new ARFModel();
                            aRFModel.SITE_NO = aTRModel.SITE_NO;
                            aRFModel.PAT_ID = aTRModel.PAT_ID;
                            aRFModel.ORD_NO = aTRModel.ORD_NO;
                            aRFModel.ACCN = ACCN;// aTRModel.ACCN;
                            aRFModel.CN = CN;
                            aRFModel.DOB = DOB;
                            aRFModel.SEX = SEX;
                            aRFModel.LOC = LOC;
                            aRFModel.DRNO = DRNO;
                            aRFModel.REQ_DTTM = aTRModel.REQ_DTTM;
                            aRFModel.DRAWN_DTTM = aTRModel.DRAWN_DTTM;
                            aRFModel.REQ_CODE = itemV_TD_GTD.REQ_CODE;
                            aRFModel.TCODE = itemV_TD_GTD.TCODE;
                            aRFModel.GTNO = itemV_TD_GTD.GTNO;
                            // aRFModel.DTNO = itemV_TD_GTD.DTNO.ToString();
                            //aRFModel.DTNO = itemV_TD_GTD.TEST_ID.ToString();
                            //aRFModel.SREQ_CODE = itemV_TD_GTD.SREQ_CODE;
                            aRFModel.S_TYPE = itemV_TD_GTD.S_TYPE;
                            //aRFModel.SP_SITE = itemV_TD_GTD.SP_SITE;
                            // aRFModel.PTN = itemV_TD_GTD.PTN;
                            //aRFModel.B_NO = itemV_TD_GTD.B_NO;
                            aRFModel.CT = "D";// aTRModel.SITE_NO;
                            aRFModel.RESULT = itemV_TD_GTD.RESULT;
                            //aRFModel.ORG_RES = itemV_TD_GTD.ORG_RES;
                            aRFModel.UNITS = itemV_TD_GTD.UNITS;
                            //aRFModel.F = itemV_TD_GTD.F;
                            //aRFModel.LRESULT = itemV_TD_GTD.LRESULT;
                            //aRFModel.LREQ_DATE = itemV_TD_GTD.LREQ_DATE;
                            aRFModel.PNDG = itemV_TD_GTD.PNDG;
                            aRFModel.TAT = itemV_TD_GTD.TAT;
                            aRFModel.STS = itemV_TD_GTD.STS;
                            aRFModel.PRTY = itemV_TD_GTD.PRTY;
                            aRFModel.MDL = itemV_TD_GTD.MDL;
                            aRFModel.RSTP = itemV_TD_GTD.RSTP;
                            //aRFModel.RES_DTTM = itemV_TD_GTD.RES_DTTM;
                            //aRFModel.VER_DTTM = itemV_TD_GTD.VER_DTTM;
                            //aRFModel.RSLD_DTTM = aTRModel.RSLD_DTTM;
                            aRFModel.DIV = itemV_TD_GTD.DIV;
                            aRFModel.SECT = itemV_TD_GTD.SECT;
                            aRFModel.WC = itemV_TD_GTD.WC;
                            aRFModel.TS = itemV_TD_GTD.TS;
                            //aRFModel.TST_ID = itemV_TD_GTD.TST_ID;
                            aRFModel.SEQ = itemV_TD_GTD.SEQ;
                            //aRFModel.RPT_NO = itemV_TD_GTD.RPT_NO;
                            aRFModel.DEC = itemV_TD_GTD.DEC;
                            //aRFModel.REF_LOW = aTRModel.SITE_NO;
                            //aRFModel.REF_HIGH = aTRModel.SITE_NO;
                            //aRFModel.CRTCL_LOW = aTRModel.SITE_NO;
                            //aRFModel.CRTCL_HIGH = aTRModel.SITE_NO;
                            //aRFModel.LHF = itemV_TD_GTD.LHF;
                            //aRFModel.AF = itemV_TD_GTD.AF;
                            //aRFModel.REF_RANGE = aTRModel.SITE_NO;
                            //aRFModel.REF_LC = aTRModel.SITE_NO;
                            //aRFModel.REF_HC = aTRModel.SITE_NO;
                            aRFModel.TNO = itemV_TD_GTD.TNO;
                            aRFModel.MHN = itemV_TD_GTD.MHN;
                            aRFModel.SHN = itemV_TD_GTD.SHN;
                            aRFModel.NO_SLD = 0;// itemV_TD_GTD.NO_SLD;
                                                //aRFModel.O_ID = itemV_TD_GTD.O_ID;
                                                //aRFModel.R_ID = itemV_TD_GTD.SITE_NO;
                                                //aRFModel.V_ID = itemV_TD_GTD.SITE_NO;
                                                //aRFModel.RSLD = itemV_TD_GTD.RSLD;
                                                //aRFModel.RSLD_ID = itemV_TD_GTD.SITE_NO;
                                                //aRFModel.VLDT = itemV_TD_GTD.SITE_NO;
                                                //aRFModel.VLDT_ID = itemV_TD_GTD.SITE_NO;
                            aRFModel.R_STS = aTRModel.R_STS == "" ? "O" : aTRModel.R_STS;// itemV_TD_GTD.RS;
                            aRFModel.BILL = itemV_TD_GTD.BILL;
                            aRFModel.UPRICE = itemV_TD_GTD.UPRICE;
                            //aRFModel.NOTES = itemV_TD_GTD.NOTES;
                            //aRFModel.NOTESB = itemV_TD_GTD.SITE_NO;
                            if (aRFModel.SEX == "M")
                            {
                                aRFModel.INTERP = itemV_TD_GTD.MINTERP;
                                aRFModel.NOTES = itemV_TD_GTD.MNOTES;
                            }
                            else if (aRFModel.SEX == "F")
                            {
                                aRFModel.INTERP = itemV_TD_GTD.FINTERP;
                                aRFModel.NOTES = itemV_TD_GTD.FNOTES;
                            }

                            //aRFModel.FN = itemV_TD_GTD.FN;
                            //aRFModel.S = itemV_TD_GTD.S;
                            //aRFModel.ARFID = itemV_TD_GTD.SITE_NO;
                            aRFModel.ATRID = aTRModel.ATR_ID;
                            aRFModel.PRID = 0;// itemV_TD_GTD.PRID;
                                              //aRFModel.VER = itemV_TD_GTD.VER;
                            aRFModel.P = "P";// itemV_TD_GTD.P;
                            aRFModel.LN = 1;// itemV_TD_GTD.LN;
                            aRFModel.CNT = 0;//  itemV_TD_GTD.CNT;
                                             //aRFModel.PF = itemV_TD_GTD.PF;
                            aRFModel.PR = itemV_TD_GTD.PR;
                            //aRFModel.WS = itemV_TD_GTD.WS;
                            aRFModel.LAST_UPDT = DateTime.Now;
                            //aRFModel.UPDT_TIME = itemV_TD_GTD.SITE_NO;
                            aRFModel.REF_SITE = ACCN != "" ? REF_SITE : "";
                            aRFModel.O_ID = aTRModel.O_ID;
                            await InsertActiveResultsFile(aRFModel);

                            // await ADD_ARF(aRFModel);

                        }


                    }
                    else
                    {

                        var GET_GTD = await _gTRepository.GetGroupTestsDetailedByParams(gTDModel.GTNO, gTDModel.REQ_CODE);//.Result // await _appDbContext.GTD
                                                                                                                          //.Where(x => x.REQ_CODE == gTDModel.REQ_CODE && x.GTNO == gTDModel.GTNO)
                                                                                                                          //.OrderBy(x => x.GTD_ID).ToList();

                        foreach (var itemGTD in GET_GTD)
                        {
                            if (!string.IsNullOrEmpty(itemGTD.DTNO.Trim()))
                            {

                                var GET_V_TD_GTD = _tDRepository.GetGroupTestsDetailedandTestDirectory(itemGTD.REQ_CODE).Result //await _appDbContext.V_TD_GTD
                                     .Where(x => x.REQ_CODE == itemGTD.REQ_CODE && x.TCODE == itemGTD.TCODE && x.TEST_ID != ""
                                     ).ToList();

                                foreach (var itemV_TD_GTD in GET_V_TD_GTD)
                                {

                                    if (PRFX != itemV_TD_GTD.PRFX)
                                    {
                                        PRFX = itemV_TD_GTD.PRFX;
                                        //ACCN = "";// await GenerateAccessionNumber(itemV_TD_GTD.PRFX);
                                    }
                                    // var ACCN = await GenerateAccessionNumber(itemV_TD_GTD.PRFX);

                                    //Add ORD_DTL, ORD_TRC
                                    ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
                                    oRD_DTLModel.ORD_SEQ = CNTOrderSeq.ToString("00");
                                    oRD_DTLModel.SITE_NO = aTRModel.SITE_NO;
                                    oRD_DTLModel.PAT_ID = aTRModel.PAT_ID;
                                    oRD_DTLModel.ORD_NO = aTRModel.ORD_NO;
                                    oRD_DTLModel.ACCN = ACCN;// aTRModel.ACCN;
                                                             //oRD_DTLModel.ORD_CODE = aTRModel.SITE_NO;
                                    oRD_DTLModel.REQ_CODE = itemV_TD_GTD.TCODE;
                                    //oRD_DTLModel.DTNO = itemV_TD_GTD.DTNO;
                                    //oRD_DTLModel.GTNO = itemV_TD_GTD.GTNO;
                                    oRD_DTLModel.TEST_ID = itemV_TD_GTD.TEST_ID;
                                    oRD_DTLModel.CT = "D";// itemV_TD_GTD.CT;
                                    oRD_DTLModel.REQ_DTTM = aTRModel.REQ_DTTM;
                                    oRD_DTLModel.O_ID = aTRModel.O_ID;
                                    oRD_DTLModel.DRAWN_DTTM = aTRModel.DRAWN_DTTM;
                                    oRD_DTLModel.TRNS_DTTM = DateTime.Now;// aTRModel.TRNS_DTTM;
                                                                          //oRD_DTLModel.TRNS_ID = aTRModel.TRNS_ID;
                                    ///oRD_DTLModel.RCVD_DTTM = DateTime.Now; //itemV_TD_GTD.RCVD_DTTM;
                                    //oRD_DTLModel.RCVD_ID = aTRModel.RCVD_ID;
                                    //oRD_DTLModel.PRCVD_DTTM = itemV_TD_GTD.PRCVD_DTTM;
                                    //oRD_DTLModel.PRCVD_ID = itemV_TD_GTD.SITE_NO;
                                    //oRD_DTLModel.VER_DTTM = itemV_TD_GTD.SITE_NO;
                                    //oRD_DTLModel.RSLD = aTRModel.RSLD;
                                    //oRD_DTLModel.VLDT = itemV_TD_GTD.SITE_NO;
                                    oRD_DTLModel.PRTY = itemV_TD_GTD.PRTY;
                                    //oRD_DTLModel.STS = "OD";// itemV_TD_GTD.STS;
                                    //oRD_DTLModel.R_STS = "O";//itemV_TD_GTD.SITE_NO;
                                    oRD_DTLModel.STS = aTRModel.STS == "" ? "OD" : aTRModel.STS;// itemV_TD_GTD.STS;
                                    oRD_DTLModel.R_STS = aTRModel.R_STS == "" ? "O" : aTRModel.R_STS;//itemV_TD_GTD.SITE_NO;
                                    oRD_DTLModel.MDL = itemV_TD_GTD.MDL;
                                    oRD_DTLModel.DIV = itemV_TD_GTD.DIV;
                                    oRD_DTLModel.SECT = itemV_TD_GTD.SECT;
                                    oRD_DTLModel.WC = itemV_TD_GTD.WC;
                                    oRD_DTLModel.TS = itemV_TD_GTD.TS;
                                    //oRD_DTLModel.X_ID = itemV_TD_GTD.X_ID;
                                    //oRD_DTLModel.CNLD = itemV_TD_GTD.SITE_NO;
                                    oRD_DTLModel.ATRID = aTRModel.ATR_ID;
                                    oRD_DTLModel.REF_SITE = ACCN != "" ? REF_SITE : "";
                                    await InsertOrdersDetails(oRD_DTLModel);
                                    //await ADD_Ord_Dtl(oRD_DTLModel);

                                    //and ARF
                                    ARFModel aRFModel = new ARFModel();
                                    aRFModel.SITE_NO = aTRModel.SITE_NO;
                                    aRFModel.PAT_ID = aTRModel.PAT_ID;
                                    aRFModel.ORD_NO = aTRModel.ORD_NO;
                                    aRFModel.ACCN = ACCN;// aTRModel.ACCN;
                                    aRFModel.CN = CN;
                                    aRFModel.DOB = DOB;
                                    aRFModel.SEX = SEX;
                                    aRFModel.LOC = LOC;
                                    aRFModel.DRNO = DRNO;
                                    aRFModel.REQ_DTTM = aTRModel.REQ_DTTM;
                                    aRFModel.DRAWN_DTTM = aTRModel.DRAWN_DTTM;
                                    aRFModel.REQ_CODE = itemV_TD_GTD.TCODE;
                                    aRFModel.TCODE = itemV_TD_GTD.TCODE;
                                    aRFModel.GTNO = itemV_TD_GTD.GTNO;
                                    aRFModel.DTNO = itemV_TD_GTD.TEST_ID;
                                    //aRFModel.SREQ_CODE = itemV_TD_GTD.SREQ_CODE;
                                    aRFModel.S_TYPE = itemV_TD_GTD.S_TYPE;
                                    //aRFModel.SP_SITE = itemV_TD_GTD.SP_SITE;
                                    //aRFModel.PTN = itemV_TD_GTD.PTN;
                                    //aRFModel.B_NO = itemV_TD_GTD.B_NO;
                                    aRFModel.CT = "D";// aTRModel.SITE_NO;
                                    aRFModel.RESULT = itemV_TD_GTD.RESULT;
                                    //aRFModel.ORG_RES = itemV_TD_GTD.ORG_RES;
                                    aRFModel.UNITS = itemV_TD_GTD.UNITS;
                                    //aRFModel.F = itemV_TD_GTD.F;
                                    //aRFModel.LRESULT = itemV_TD_GTD.LRESULT;
                                    //aRFModel.LREQ_DATE = itemV_TD_GTD.LREQ_DATE;
                                    aRFModel.PNDG = itemV_TD_GTD.PNDG;
                                    aRFModel.TAT = itemV_TD_GTD.TAT;
                                    aRFModel.STS = itemV_TD_GTD.STS;
                                    aRFModel.PRTY = itemV_TD_GTD.PRTY;
                                    aRFModel.MDL = itemV_TD_GTD.MDL;
                                    aRFModel.RSTP = itemV_TD_GTD.RSTP;
                                    //aRFModel.RES_DTTM = itemV_TD_GTD.RES_DTTM;
                                    //aRFModel.VER_DTTM = itemV_TD_GTD.VER_DTTM;
                                    //aRFModel.RSLD_DTTM = aTRModel.RSLD_DTTM;
                                    aRFModel.DIV = itemV_TD_GTD.DIV;
                                    aRFModel.SECT = itemV_TD_GTD.SECT;
                                    aRFModel.WC = itemV_TD_GTD.WC;
                                    aRFModel.TS = itemV_TD_GTD.TS;
                                    //aRFModel.TST_ID = itemV_TD_GTD.TST_ID;
                                    aRFModel.SEQ = itemV_TD_GTD.SEQ;
                                    //aRFModel.RPT_NO = itemV_TD_GTD.RPT_NO;
                                    aRFModel.DEC = itemV_TD_GTD.DEC;
                                    //aRFModel.REF_LOW = aTRModel.SITE_NO;
                                    //aRFModel.REF_HIGH = aTRModel.SITE_NO;
                                    //aRFModel.CRTCL_LOW = aTRModel.SITE_NO;
                                    //aRFModel.CRTCL_HIGH = aTRModel.SITE_NO;
                                    //aRFModel.LHF = itemV_TD_GTD.LHF;
                                    //aRFModel.AF = itemV_TD_GTD.AF;
                                    //aRFModel.REF_RANGE = aTRModel.SITE_NO;
                                    //aRFModel.REF_LC = aTRModel.SITE_NO;
                                    //aRFModel.REF_HC = aTRModel.SITE_NO;
                                    aRFModel.TNO = itemV_TD_GTD.TNO;
                                    aRFModel.MHN = itemV_TD_GTD.MHN;
                                    aRFModel.SHN = itemV_TD_GTD.SHN;
                                    aRFModel.NO_SLD = 0;// itemV_TD_GTD.NO_SLD;

                                    //aRFModel.R_ID = itemV_TD_GTD.SITE_NO;
                                    //aRFModel.V_ID = itemV_TD_GTD.SITE_NO;
                                    //aRFModel.RSLD = itemV_TD_GTD.RSLD;
                                    //aRFModel.RSLD_ID = itemV_TD_GTD.SITE_NO;
                                    //aRFModel.VLDT = itemV_TD_GTD.SITE_NO;
                                    //aRFModel.VLDT_ID = itemV_TD_GTD.SITE_NO;
                                    aRFModel.R_STS = aTRModel.R_STS == "" ? "O" : aTRModel.R_STS;// itemV_TD_GTD.RS;
                                    aRFModel.BILL = itemV_TD_GTD.BILL;
                                    aRFModel.UPRICE = itemV_TD_GTD.UPRICE;
                                    //aRFModel.NOTES = itemV_TD_GTD.NOTES;
                                    //aRFModel.NOTESB = itemV_TD_GTD.SITE_NO;
                                    if (aRFModel.SEX == "M")
                                    {
                                        aRFModel.INTERP = itemV_TD_GTD.MINTERP;
                                        aRFModel.NOTES = itemV_TD_GTD.MNOTES;
                                    }
                                    else if (aRFModel.SEX == "F")
                                    {
                                        aRFModel.INTERP = itemV_TD_GTD.FINTERP;
                                        aRFModel.NOTES = itemV_TD_GTD.FNOTES;
                                    }

                                    //aRFModel.FN = itemV_TD_GTD.FN;
                                    //aRFModel.S = itemV_TD_GTD.S;
                                    //aRFModel.ARFID = itemV_TD_GTD.SITE_NO;
                                    aRFModel.ATRID = aTRModel.ATR_ID;
                                    aRFModel.PRID = 0;// itemV_TD_GTD.PRID;
                                                      //aRFModel.VER = itemV_TD_GTD.VER;
                                    aRFModel.P = "P";// itemV_TD_GTD.P;
                                    aRFModel.LN = 1;// itemV_TD_GTD.LN;
                                    aRFModel.CNT = 0;//  itemV_TD_GTD.CNT;
                                                     //aRFModel.PF = itemV_TD_GTD.PF;
                                    aRFModel.PR = itemV_TD_GTD.PR;
                                    //aRFModel.WS = itemV_TD_GTD.WS;
                                    aRFModel.LAST_UPDT = DateTime.Now;
                                    //aRFModel.UPDT_TIME = itemV_TD_GTD.SITE_NO;
                                    aRFModel.REF_SITE = ACCN != "" ? REF_SITE : "";
                                    aRFModel.O_ID = aTRModel.O_ID;
                                    await InsertActiveResultsFile(aRFModel);
                                    //await ADD_ARF(aRFModel);
                                }
                            }
                            else
                            {
                                var GET_V_TD_GTD_TCODE = _gTRepository.GetGroupTestsDetailedandGroupTests().Result // await _appDbContext.V_GT_GTD
                                    .Where(x => x.REQ_CODE == itemGTD.TCODE).ToList();

                                foreach (var itemV_TD_GTD in GET_V_TD_GTD_TCODE)
                                {
                                    if (PRFX != itemV_TD_GTD.PRFX)
                                    {
                                        PRFX = itemV_TD_GTD.PRFX;
                                        //ACCN = "";// await GenerateAccessionNumber(itemV_TD_GTD.PRFX);
                                    }
                                    //var ACCN = await GenerateAccessionNumber(itemV_TD_GTD.PRFX);



                                    //Add ORD_DETL and ORD_TRC
                                    ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
                                    oRD_DTLModel.ORD_SEQ = CNTOrderSeq.ToString("00");
                                    oRD_DTLModel.SITE_NO = aTRModel.SITE_NO;
                                    oRD_DTLModel.PAT_ID = aTRModel.PAT_ID;
                                    oRD_DTLModel.ORD_NO = aTRModel.ORD_NO;
                                    oRD_DTLModel.ACCN = ACCN;// aTRModel.ACCN;
                                                             //oRD_DTLModel.ORD_CODE = aTRModel.SITE_NO;
                                    oRD_DTLModel.REQ_CODE = itemV_TD_GTD.REQ_CODE;
                                    //oRD_DTLModel.DTNO = itemV_TD_GTD.DTNO;
                                    //oRD_DTLModel.GTNO = itemV_TD_GTD.GTNO;
                                    oRD_DTLModel.TEST_ID = itemV_TD_GTD.DTNO;
                                    oRD_DTLModel.CT = "G";// itemV_TD_GTD.CT;
                                    oRD_DTLModel.REQ_DTTM = aTRModel.REQ_DTTM;
                                    oRD_DTLModel.O_ID = aTRModel.O_ID;
                                    oRD_DTLModel.DRAWN_DTTM = aTRModel.DRAWN_DTTM;
                                    oRD_DTLModel.TRNS_DTTM = DateTime.Now;// aTRModel.TRNS_DTTM;
                                                                          //oRD_DTLModel.TRNS_ID = aTRModel.TRNS_ID;
                                    oRD_DTLModel.RCVD_DTTM = DateTime.Now; //itemV_TD_GTD.RCVD_DTTM;
                                                                           //oRD_DTLModel.RCVD_ID = aTRModel.RCVD_ID;
                                                                           //oRD_DTLModel.PRCVD_DTTM = itemV_TD_GTD.PRCVD_DTTM;
                                                                           //oRD_DTLModel.PRCVD_ID = itemV_TD_GTD.SITE_NO;
                                                                           //oRD_DTLModel.VER_DTTM = itemV_TD_GTD.SITE_NO;
                                                                           //oRD_DTLModel.RSLD = aTRModel.RSLD;
                                                                           //oRD_DTLModel.VLDT = itemV_TD_GTD.SITE_NO;
                                    oRD_DTLModel.PRTY = itemV_TD_GTD.PRTY;
                                    //oRD_DTLModel.STS = "OD";// itemV_TD_GTD.STS;
                                    //oRD_DTLModel.R_STS = "O";//itemV_TD_GTD.SITE_NO;
                                    oRD_DTLModel.STS = aTRModel.STS != "" ? "OD" : aTRModel.STS;// itemV_TD_GTD.STS;
                                    oRD_DTLModel.R_STS = aTRModel.R_STS != "" ? "O" : aTRModel.R_STS;//itemV_TD_GTD.SITE_NO;
                                    oRD_DTLModel.MDL = itemV_TD_GTD.MDL;
                                    oRD_DTLModel.DIV = itemV_TD_GTD.DIV;
                                    oRD_DTLModel.SECT = itemV_TD_GTD.SECT;
                                    //oRD_DTLModel.WC = itemV_TD_GTD.WC;
                                    oRD_DTLModel.TS = itemV_TD_GTD.TS;
                                    //oRD_DTLModel.X_ID = itemV_TD_GTD.X_ID;
                                    //oRD_DTLModel.CNLD = itemV_TD_GTD.SITE_NO;
                                    oRD_DTLModel.ATRID = aTRModel.ATR_ID;
                                    oRD_DTLModel.REF_SITE = ACCN != "" ? REF_SITE : "";
                                    await InsertOrdersDetails(oRD_DTLModel);
                                    //await ADD_Ord_Dtl(oRD_DTLModel);
                                }

                                var GET_V_TD_GTD = _tDRepository.GetGroupTestsDetailedandTestDirectory(itemGTD.TCODE).Result // await _appDbContext.V_TD_GTD
                                      .Where(x => x.REQ_CODE == itemGTD.TCODE).ToList();
                                foreach (var itemV_TD_GTD in GET_V_TD_GTD)
                                {

                                    if (PRFX != itemV_TD_GTD.PRFX)
                                    {
                                        PRFX = itemV_TD_GTD.PRFX;
                                        //ACCN = "";// await GenerateAccessionNumber(itemV_TD_GTD.PRFX);
                                    }
                                    //var ACCN = await GenerateAccessionNumber(itemV_TD_GTD.PRFX);

                                    //Add ARF
                                    ARFModel aRFModel = new ARFModel();
                                    aRFModel.SITE_NO = aTRModel.SITE_NO;
                                    aRFModel.PAT_ID = aTRModel.PAT_ID;
                                    aRFModel.ORD_NO = aTRModel.ORD_NO;
                                    aRFModel.ACCN = ACCN;// aTRModel.ACCN;
                                    aRFModel.CN = CN;
                                    aRFModel.DOB = DOB;
                                    aRFModel.SEX = SEX;
                                    aRFModel.LOC = LOC;
                                    aRFModel.DRNO = DRNO;
                                    aRFModel.REQ_DTTM = aTRModel.REQ_DTTM;
                                    aRFModel.DRAWN_DTTM = aTRModel.DRAWN_DTTM;
                                    aRFModel.REQ_CODE = itemGTD.TCODE;
                                    aRFModel.TCODE = itemV_TD_GTD.TCODE;
                                    aRFModel.GTNO = itemV_TD_GTD.GTNO;
                                    // aRFModel.DTNO = itemV_TD_GTD.DTNO;
                                    aRFModel.DTNO = itemV_TD_GTD.TEST_ID;
                                    //aRFModel.SREQ_CODE = itemV_TD_GTD.SREQ_CODE;
                                    aRFModel.S_TYPE = itemV_TD_GTD.S_TYPE;
                                    //aRFModel.SP_SITE = itemV_TD_GTD.SP_SITE;
                                    // aRFModel.PTN = itemV_TD_GTD.PTN;
                                    //aRFModel.B_NO = itemV_TD_GTD.B_NO;
                                    aRFModel.CT = "D";// aTRModel.SITE_NO;
                                    aRFModel.RESULT = itemV_TD_GTD.RESULT;
                                    //aRFModel.ORG_RES = itemV_TD_GTD.ORG_RES;
                                    aRFModel.UNITS = itemV_TD_GTD.UNITS;
                                    //aRFModel.F = itemV_TD_GTD.F;
                                    //aRFModel.LRESULT = itemV_TD_GTD.LRESULT;
                                    //aRFModel.LREQ_DATE = itemV_TD_GTD.LREQ_DATE;
                                    aRFModel.PNDG = itemV_TD_GTD.PNDG;
                                    aRFModel.TAT = itemV_TD_GTD.TAT;
                                    aRFModel.STS = itemV_TD_GTD.STS;
                                    aRFModel.PRTY = itemV_TD_GTD.PRTY;
                                    aRFModel.MDL = itemV_TD_GTD.MDL;
                                    aRFModel.RSTP = itemV_TD_GTD.RSTP;
                                    //aRFModel.RES_DTTM = itemV_TD_GTD.RES_DTTM;
                                    //aRFModel.VER_DTTM = itemV_TD_GTD.VER_DTTM;
                                    //aRFModel.RSLD_DTTM = aTRModel.RSLD_DTTM;
                                    aRFModel.DIV = itemV_TD_GTD.DIV;
                                    aRFModel.SECT = itemV_TD_GTD.SECT;
                                    aRFModel.WC = itemV_TD_GTD.WC;
                                    aRFModel.TS = itemV_TD_GTD.TS;
                                    //aRFModel.TST_ID = itemV_TD_GTD.TST_ID;
                                    aRFModel.SEQ = itemV_TD_GTD.SEQ;
                                    //aRFModel.RPT_NO = itemV_TD_GTD.RPT_NO;
                                    aRFModel.DEC = itemV_TD_GTD.DEC;
                                    //aRFModel.REF_LOW = aTRModel.SITE_NO;
                                    //aRFModel.REF_HIGH = aTRModel.SITE_NO;
                                    //aRFModel.CRTCL_LOW = aTRModel.SITE_NO;
                                    //aRFModel.CRTCL_HIGH = aTRModel.SITE_NO;
                                    //aRFModel.LHF = itemV_TD_GTD.LHF;
                                    //aRFModel.AF = itemV_TD_GTD.AF;
                                    //aRFModel.REF_RANGE = aTRModel.SITE_NO;
                                    //aRFModel.REF_LC = aTRModel.SITE_NO;
                                    //aRFModel.REF_HC = aTRModel.SITE_NO;
                                    aRFModel.TNO = itemV_TD_GTD.TNO;
                                    aRFModel.MHN = itemV_TD_GTD.MHN;
                                    aRFModel.SHN = itemV_TD_GTD.SHN;
                                    aRFModel.NO_SLD = 0;// itemV_TD_GTD.NO_SLD;
                                    aRFModel.O_ID = aTRModel.O_ID;
                                    //aRFModel.R_ID = itemV_TD_GTD.SITE_NO;
                                    //aRFModel.V_ID = itemV_TD_GTD.SITE_NO;
                                    //aRFModel.RSLD = itemV_TD_GTD.RSLD;
                                    //aRFModel.RSLD_ID = itemV_TD_GTD.SITE_NO;
                                    //aRFModel.VLDT = itemV_TD_GTD.SITE_NO;
                                    //aRFModel.VLDT_ID = itemV_TD_GTD.SITE_NO;
                                    aRFModel.R_STS = aTRModel.R_STS != "" ? "O" : aTRModel.R_STS;// itemV_TD_GTD.RS;
                                    aRFModel.BILL = itemV_TD_GTD.BILL;
                                    aRFModel.UPRICE = itemV_TD_GTD.UPRICE;
                                    //aRFModel.NOTES = itemV_TD_GTD.NOTES;
                                    //aRFModel.NOTESB = itemV_TD_GTD.SITE_NO;
                                    if (aRFModel.SEX == "M")
                                    {
                                        aRFModel.INTERP = itemV_TD_GTD.MINTERP;
                                        aRFModel.NOTES = itemV_TD_GTD.MNOTES;
                                    }
                                    else if (aRFModel.SEX == "F")
                                    {
                                        aRFModel.INTERP = itemV_TD_GTD.FINTERP;
                                        aRFModel.NOTES = itemV_TD_GTD.FNOTES;
                                    }

                                    //aRFModel.FN = itemV_TD_GTD.FN;
                                    //aRFModel.S = itemV_TD_GTD.S;
                                    //aRFModel.ARFID = itemV_TD_GTD.SITE_NO;
                                    aRFModel.ATRID = aTRModel.ATR_ID;
                                    aRFModel.PRID = 0;// itemV_TD_GTD.PRID;
                                                      //aRFModel.VER = itemV_TD_GTD.VER;
                                    aRFModel.P = "P";// itemV_TD_GTD.P;
                                    aRFModel.LN = 1;// itemV_TD_GTD.LN;
                                    aRFModel.CNT = 0;//  itemV_TD_GTD.CNT;
                                                     //aRFModel.PF = itemV_TD_GTD.PF;
                                    aRFModel.PR = itemV_TD_GTD.PR;
                                    //aRFModel.WS = itemV_TD_GTD.WS;
                                    aRFModel.LAST_UPDT = DateTime.Now;
                                    //aRFModel.UPDT_TIME = itemV_TD_GTD.SITE_NO;
                                    aRFModel.REF_SITE = ACCN != "" ? REF_SITE : "";
                                    await InsertActiveResultsFile(aRFModel);
                                    //await ADD_ARF(aRFModel);
                                }

                            }
                        }
                    }

                    #endregion

                    #endregion
                }
                else
                {
                    #region TD Found Add to ORD_DTL, ORD_TRC and ARF
                    //
                    var itemTD = GET_TD;
                    if (PRFX != itemTD.PRFX)
                    {
                        PRFX = itemTD.PRFX;
                        //ACCN = "";// await GenerateAccessionNumber(itemTD.PRFX);
                    }
                    //var ACCN = await GenerateAccessionNumber(itemTD.PRFX);

                    //Add ORD_DTL, ORD_TRC
                    ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
                    oRD_DTLModel.ORD_SEQ = CNTOrderSeq.ToString("00");
                    oRD_DTLModel.SITE_NO = aTRModel.SITE_NO;
                    oRD_DTLModel.PAT_ID = aTRModel.PAT_ID;
                    oRD_DTLModel.ORD_NO = aTRModel.ORD_NO;
                    oRD_DTLModel.ACCN = ACCN;// aTRModel.ACCN;
                                             //oRD_DTLModel.ORD_CODE = aTRModel.SITE_NO;
                    oRD_DTLModel.REQ_CODE = itemTD.TCODE;
                    //oRD_DTLModel.DTNO = itemTD.DTNO.ToString();
                    oRD_DTLModel.TEST_ID = itemTD.TEST_ID.ToString();
                    //oRD_DTLModel.GTNO = itemTD.GTNO;
                    oRD_DTLModel.CT = "D";// itemV_TD_GTD.CT;
                    oRD_DTLModel.REQ_DTTM = aTRModel.REQ_DTTM;
                    oRD_DTLModel.O_ID = aTRModel.O_ID;
                    oRD_DTLModel.DRAWN_DTTM = aTRModel.DRAWN_DTTM;
                    oRD_DTLModel.TRNS_DTTM = DateTime.Now;// aTRModel.TRNS_DTTM;
                                                          //oRD_DTLModel.TRNS_ID = aTRModel.TRNS_ID;
                    ///oRD_DTLModel.RCVD_DTTM = DateTime.Now; //itemV_TD_GTD.RCVD_DTTM;
                    //oRD_DTLModel.RCVD_ID = aTRModel.RCVD_ID;
                    //oRD_DTLModel.PRCVD_DTTM = itemV_TD_GTD.PRCVD_DTTM;
                    //oRD_DTLModel.PRCVD_ID = itemV_TD_GTD.SITE_NO;
                    //oRD_DTLModel.VER_DTTM = itemV_TD_GTD.SITE_NO;
                    //oRD_DTLModel.RSLD = aTRModel.RSLD;
                    //oRD_DTLModel.VLDT = itemV_TD_GTD.SITE_NO;
                    oRD_DTLModel.PRTY = itemTD.PRTY;
                    //oRD_DTLModel.STS = "OD";// itemV_TD_GTD.STS;
                    //oRD_DTLModel.R_STS = "O";//itemV_TD_GTD.SITE_NO;
                    oRD_DTLModel.STS = (aTRModel.STS == "" || aTRModel.STS == "RT") ? "OD" : aTRModel.STS;// itemV_TD_GTD.STS;
                    oRD_DTLModel.R_STS = aTRModel.R_STS == "" ? "O" : aTRModel.R_STS;//itemV_TD_GTD.SITE_NO;
                    oRD_DTLModel.MDL = itemTD.MDL;
                    oRD_DTLModel.DIV = itemTD.DIV;
                    oRD_DTLModel.SECT = itemTD.SECT;
                    oRD_DTLModel.WC = itemTD.WC;
                    oRD_DTLModel.TS = itemTD.TS;
                    //oRD_DTLModel.X_ID = itemV_TD_GTD.X_ID;
                    //oRD_DTLModel.CNLD = itemV_TD_GTD.SITE_NO;
                    oRD_DTLModel.ATRID = aTRModel.ATR_ID;
                    oRD_DTLModel.REF_SITE = ACCN != "" ? REF_SITE : "";
                    await InsertOrdersDetails(oRD_DTLModel);
                    //await ADD_Ord_Dtl(oRD_DTLModel);

                    //and ARF
                    ARFModel aRFModel = new ARFModel();
                    aRFModel.SITE_NO = aTRModel.SITE_NO;
                    aRFModel.PAT_ID = aTRModel.PAT_ID;
                    aRFModel.ORD_NO = aTRModel.ORD_NO;
                    aRFModel.ACCN = ACCN;// aTRModel.ACCN;
                    aRFModel.CN = CN;
                    aRFModel.DOB = DOB;
                    aRFModel.SEX = SEX;
                    aRFModel.LOC = LOC;
                    aRFModel.DRNO = DRNO;
                    aRFModel.REQ_DTTM = aTRModel.REQ_DTTM;
                    aRFModel.DRAWN_DTTM = aTRModel.DRAWN_DTTM;
                    aRFModel.REQ_CODE = itemTD.TCODE;
                    aRFModel.TCODE = itemTD.TCODE;
                    //aRFModel.GTNO = itemTD.GTNO;
                    // aRFModel.DTNO = itemTD.DTNO.ToString();
                    aRFModel.DTNO = itemTD.TEST_ID.ToString();
                    //aRFModel.SREQ_CODE = itemV_TD_GTD.SREQ_CODE;
                    aRFModel.S_TYPE = itemTD.S_TYPE;
                    //aRFModel.SP_SITE = itemV_TD_GTD.SP_SITE;
                    //aRFModel.PTN = itemTD.PTN;
                    //aRFModel.B_NO = itemTD.B_NO;
                    aRFModel.CT = "D";// aTRModel.SITE_NO;
                    aRFModel.RESULT = itemTD.RESULT;
                    //aRFModel.ORG_RES = itemV_TD_GTD.ORG_RES;
                    aRFModel.UNITS = itemTD.UNITS;
                    //aRFModel.F = itemV_TD_GTD.F;
                    //aRFModel.LRESULT = itemV_TD_GTD.LRESULT;
                    //aRFModel.LREQ_DATE = itemV_TD_GTD.LREQ_DATE;
                    //aRFModel.PNDG = itemTD.PNDG;
                    aRFModel.TAT = itemTD.TAT;
                    aRFModel.STS = itemTD.STS;
                    aRFModel.PRTY = itemTD.PRTY;
                    aRFModel.MDL = itemTD.MDL;
                    aRFModel.RSTP = itemTD.RSTP;
                    //aRFModel.RES_DTTM = itemV_TD_GTD.RES_DTTM;
                    //aRFModel.VER_DTTM = itemV_TD_GTD.VER_DTTM;
                    //aRFModel.RSLD_DTTM = aTRModel.RSLD_DTTM;
                    aRFModel.DIV = itemTD.DIV;
                    aRFModel.SECT = itemTD.SECT;
                    aRFModel.WC = itemTD.WC;
                    aRFModel.TS = itemTD.TS;
                    //aRFModel.TST_ID = itemV_TD_GTD.TST_ID;
                    aRFModel.SEQ = itemTD.SEQ;
                    //aRFModel.RPT_NO = itemV_TD_GTD.RPT_NO;
                    aRFModel.DEC = itemTD.DEC;
                    //aRFModel.REF_LOW = aTRModel.SITE_NO;
                    //aRFModel.REF_HIGH = aTRModel.SITE_NO;
                    //aRFModel.CRTCL_LOW = aTRModel.SITE_NO;
                    //aRFModel.CRTCL_HIGH = aTRModel.SITE_NO;
                    //aRFModel.LHF = itemV_TD_GTD.LHF;
                    //aRFModel.AF = itemV_TD_GTD.AF;
                    //aRFModel.REF_RANGE = aTRModel.SITE_NO;
                    //aRFModel.REF_LC = aTRModel.SITE_NO;
                    //aRFModel.REF_HC = aTRModel.SITE_NO;
                    aRFModel.TNO = itemTD.TNO;
                    aRFModel.MHN = itemTD.MHN;
                    aRFModel.SHN = itemTD.SHN;
                    aRFModel.NO_SLD = 0;// itemV_TD_GTD.NO_SLD;
                    aRFModel.O_ID = aTRModel.O_ID;
                    //aRFModel.R_ID = itemV_TD_GTD.SITE_NO;
                    //aRFModel.V_ID = itemV_TD_GTD.SITE_NO;
                    //aRFModel.RSLD = itemV_TD_GTD.RSLD;
                    //aRFModel.RSLD_ID = itemV_TD_GTD.SITE_NO;
                    //aRFModel.VLDT = itemV_TD_GTD.SITE_NO;
                    //aRFModel.VLDT_ID = itemV_TD_GTD.SITE_NO;
                    //aRFModel.R_STS = "O";// itemV_TD_GTD.RS;
                    aRFModel.R_STS = aTRModel.R_STS == "" ? "O" : aTRModel.R_STS;//itemV_TD_GTD.SITE_NO;
                    aRFModel.BILL = itemTD.BILL;
                    aRFModel.UPRICE = itemTD.UPRICE;
                    //aRFModel.NOTES = itemV_TD_GTD.NOTES;
                    //aRFModel.NOTESB = itemV_TD_GTD.SITE_NO;
                    if (aRFModel.SEX == "M")
                    {
                        aRFModel.INTERP = itemTD.MINTERP;
                        aRFModel.NOTES = itemTD.MNOTES;
                    }
                    else if (aRFModel.SEX == "F")
                    {
                        aRFModel.INTERP = itemTD.FINTERP;
                        aRFModel.NOTES = itemTD.FNOTES;
                    }


                    //aRFModel.FN = itemV_TD_GTD.FN;
                    //aRFModel.S = itemV_TD_GTD.S;
                    //aRFModel.ARFID = itemV_TD_GTD.SITE_NO;
                    aRFModel.ATRID = aTRModel.ATR_ID;
                    aRFModel.PRID = 0;// itemV_TD_GTD.PRID;
                                      //aRFModel.VER = itemV_TD_GTD.VER;
                    aRFModel.P = "P";// itemV_TD_GTD.P;
                    aRFModel.LN = 1;// itemV_TD_GTD.LN;
                    aRFModel.CNT = 0;//  itemV_TD_GTD.CNT;
                                     //aRFModel.PF = itemV_TD_GTD.PF;
                    aRFModel.PR = itemTD.PR;
                    //aRFModel.WS = itemV_TD_GTD.WS;
                    aRFModel.LAST_UPDT = DateTime.Now;
                    //aRFModel.UPDT_TIME = itemV_TD_GTD.SITE_NO;
                    aRFModel.REF_SITE = ACCN != "" ? REF_SITE : "";
                    await InsertActiveResultsFile(aRFModel);

                    //await ADD_ARF(aRFModel);


                    #endregion
                }

            }
            //return await Task.FromResult(getData.returnData);

            return await Task.FromResult(badresult.StatusCode);
        }

        public async Task<int> InsertOrdersDetails(ORD_DTLModel oRD_DTLModel)//ADD_Ord_Dtl
        {
            BadRequestResult badRequest = new BadRequestResult();

            if (oRD_DTLModel == null)
                return badRequest.StatusCode;
            if (string.IsNullOrEmpty(oRD_DTLModel.PAT_ID)) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Insert;
            oRD_DTLModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ORD_DTLModel>(oRD_DTLModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.SP_ManageOrdersDetails, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();


            ORD_TRCModel oRD_TRCModel = new ORD_TRCModel();
            oRD_TRCModel.SITE_NO = oRD_DTLModel.SITE_NO;
            oRD_TRCModel.ORD_NO = oRD_DTLModel.ORD_NO;
            //string STS = "";
            //if (oRD_DTLModel.STS == "OD") STS = "01";
            //if (oRD_DTLModel.STS == "RS") STS = "03";
            //if (oRD_DTLModel.STS == "CO") STS = "05";
            //if (oRD_DTLModel.STS == "IT") STS = "07";
            //if (oRD_DTLModel.STS == "PA") STS = "09";
            //if (oRD_DTLModel.STS == "CR") STS = "11";
            //if (oRD_DTLModel.STS == "QR") STS = "13";
            //if (oRD_DTLModel.STS == "DL") STS = "15";
            //if (oRD_DTLModel.STS == "ST") STS = "17";
            //if (oRD_DTLModel.STS == "DM") STS = "19";
            //if (oRD_DTLModel.STS == "RJ") STS = "21";
            //if (oRD_DTLModel.STS == "DS") STS = "23";
            //if (oRD_DTLModel.STS == "CK") STS = "25";

            oRD_TRCModel.STS = oRD_DTLModel.STS;// STS;
            oRD_TRCModel.SECT = oRD_DTLModel.SECT;
            oRD_TRCModel.REQ_CODE = oRD_DTLModel.REQ_CODE;
            oRD_TRCModel.ACT_DTTM = DateTime.Now;
            oRD_TRCModel.U_ID = oRD_DTLModel.O_ID;// "999";

            await InsertOrdersTracking(oRD_TRCModel);


            //Added on June 1, 2024 as per new business requirements requested by Mr. Rashid 
            if (oRD_DTLModel.MDL == "CG")
            {
                FreeTextResultsModel freeTextResultsModel = new FreeTextResultsModel();
                freeTextResultsModel.ORD_NO = oRD_DTLModel.ORD_NO;
                freeTextResultsModel.ACCN = oRD_DTLModel.ACCN;
                freeTextResultsModel.REQ_CODE = oRD_DTLModel.REQ_CODE;
                await InsertFreeTextResults(freeTextResultsModel);

                CytogeneticsQCModel cytogeneticsQCModel = new CytogeneticsQCModel();
                cytogeneticsQCModel.ORD_NO = oRD_DTLModel.ORD_NO;
                cytogeneticsQCModel.ACCN = oRD_DTLModel.ACCN;
                cytogeneticsQCModel.REQ_CODE = oRD_DTLModel.REQ_CODE;
                await InsertCytogeneticsQualityControl(cytogeneticsQCModel);
            }
            if (oRD_DTLModel.MDL == "AP")
            {
                //FreeTextResultsModel freeTextResultsModel = new FreeTextResultsModel();
                //freeTextResultsModel.ORD_NO = oRD_DTLModel.ORD_NO;
                //freeTextResultsModel.ACCN = oRD_DTLModel.ACCN;
                //await InsertFreeTextResults(freeTextResultsModel);

                AnatomicPathologyCasesModel anatomicPathologyCasesModel = new AnatomicPathologyCasesModel();
                anatomicPathologyCasesModel.ORD_NO = oRD_DTLModel.ORD_NO;
                anatomicPathologyCasesModel.ACCN = oRD_DTLModel.ACCN;
                anatomicPathologyCasesModel.REQ_CODE = oRD_DTLModel.REQ_CODE;
                anatomicPathologyCasesModel.MDL = oRD_DTLModel.MDL;
                await InsertAnatomicPathologyCases(anatomicPathologyCasesModel);

            }
            //end

            return await Task.FromResult(getData.returnData);

            //return Ok(new
            //{
            //    Message = "Order ATR Created!"
            //});
        }

        public async Task<int> InsertOrdersTracking(ORD_TRCModel oRD_TRCModel)//ADD_Ord_Trc
        {
            BadRequestResult badRequest = new BadRequestResult();
            if (oRD_TRCModel == null)
                return badRequest.StatusCode;
            if (string.IsNullOrEmpty(oRD_TRCModel.ORD_NO)) return badRequest.StatusCode;

            string STS = "";
            if (oRD_TRCModel.STS == "OD") STS = "01";
            if (oRD_TRCModel.STS == "RS") STS = "03";
            if (oRD_TRCModel.STS == "CO") STS = "05";
            if (oRD_TRCModel.STS == "IT") STS = "07";
            if (oRD_TRCModel.STS == "PR") STS = "11";
            if (oRD_TRCModel.STS == "CR") STS = "09";
            if (oRD_TRCModel.STS == "QR") STS = "13";
            if (oRD_TRCModel.STS == "DL") STS = "15";
            if (oRD_TRCModel.STS == "ST") STS = "17";
            if (oRD_TRCModel.STS == "DM") STS = "19";
            if (oRD_TRCModel.STS == "RJ") STS = "21";
            if (oRD_TRCModel.STS == "DS") STS = "23";
            if (oRD_TRCModel.STS == "CK") STS = "25";

            oRD_TRCModel.STS = STS;
            oRD_TRCModel.ACT_DTTM = DateTime.Now;
            oRD_TRCModel.U_ID = oRD_TRCModel.U_ID;// "999";

            int queryType = (int)QueryTypeEnum.Insert;
            oRD_TRCModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ORD_TRCModel>(oRD_TRCModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.SP_ManageOrdersTracking, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();


            return await Task.FromResult(getData.returnData);

            //await _appDbContext.Ord_Trc.AddAsync(oRD_TRCModel);
            // await _appDbContext.SaveChangesAsync();

            //return Ok(new
            //{
            //    Message = "Order ATR Created!"
            //});
        }

        public async Task<int> InsertActiveResultsFile(ARFModel aRFModel)//ADD_ARF
        {
            BadRequestResult badRequest = new BadRequestResult();
            if (aRFModel == null)
                return badRequest.StatusCode;
            if (string.IsNullOrEmpty(aRFModel.PAT_ID)) return badRequest.StatusCode;

            DateTime DRAWN_DTTM = new DateTime();
            DateTime DOB = new DateTime();
            DRAWN_DTTM = Convert.ToDateTime(string.IsNullOrEmpty(aRFModel.DRAWN_DTTM.ToString()) ? DRAWN_DTTM.ToString() : aRFModel.DRAWN_DTTM.ToString());
            DOB = Convert.ToDateTime(aRFModel.DOB.ToString());
            int age = DRAWN_DTTM.Subtract(DOB).Days;


            var GET_REF_RNG = _masterRepository.GetReferenceRanges().Result // await _appDbContext.REF_RNG
             .Where(x => x.TCODE.Trim() == aRFModel.TCODE.Trim()
                                     && x.RSTP.Trim() == aRFModel.RSTP.Trim()
                                     && x.SEX.Trim() == aRFModel.SEX.Trim()
                                     && (age >= x.AGE_FROM && age <= x.AGE_TO)
                                     //&& x.AGE_FROM == rEF_RNGModel.AGE_FROM
                                     ).FirstOrDefault();
            if (GET_REF_RNG != null)
            {
                aRFModel.REF_LOW = GET_REF_RNG.REF_LOW;
                aRFModel.REF_HIGH = GET_REF_RNG.REF_HIGH;

                aRFModel.CRTCL_LOW = GET_REF_RNG.CRTCL_HIGH;
                aRFModel.CRTCL_HIGH = GET_REF_RNG.CRTCL_LOW;

                aRFModel.REF_RANGE = GET_REF_RNG.REF_RANGE;
                aRFModel.REF_LC = GET_REF_RNG.REF_LC;
                aRFModel.REF_HC = GET_REF_RNG.REF_HC;
            }
            else
            {
                aRFModel.REF_LOW = null;
                aRFModel.REF_HIGH = null;

                aRFModel.CRTCL_LOW = null;
                aRFModel.CRTCL_HIGH = null;

                aRFModel.REF_RANGE = null;
                aRFModel.REF_LC = null;
                aRFModel.REF_HC = null;
            }

            int queryType = (int)QueryTypeEnum.Insert;
            aRFModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ARFModel>(aRFModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.SP_ManageActiveResultsFile, parameterCollection);//"usp_ManageActiveResultsFile"
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();

            return await Task.FromResult(getData.returnData);

            //await _appDbContext.ARF.AddAsync(aRFModel);
            //await _appDbContext.SaveChangesAsync();

            //return Ok(new
            //{
            //    Message = "Order ARF Created!"
            //});
        }


        public async Task<int> InsertFreeTextResults(FreeTextResultsModel freeTextResultsModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            freeTextResultsModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<FreeTextResultsModel>(freeTextResultsModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.SP_ManageCytogeneticsCases, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }
        public async Task<int> InsertCytogeneticsQualityControl(CytogeneticsQCModel cytogeneticsQCModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            cytogeneticsQCModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CytogeneticsQCModel>(cytogeneticsQCModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.SP_ManageCytogeneticsQualityControl, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }
        public async Task<int> InsertAnatomicPathologyCases(AnatomicPathologyCasesModel anatomicPathologyCasesModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            anatomicPathologyCasesModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<AnatomicPathologyCasesModel>(anatomicPathologyCasesModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.SP_ManageAnatomicPathologyCases, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }
        #endregion

        #region Update Order Transaction
        public async Task<int> UpdateOrdersTransactions(ORD_TRNSModel oRD_TRNSModel)
        {

            BadRequestResult badRequest = new BadRequestResult();

            if (oRD_TRNSModel == null)
                return badRequest.StatusCode;
            if (string.IsNullOrEmpty(oRD_TRNSModel.PAT_ID)) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Update;
            oRD_TRNSModel.QueryType = queryType;
            oRD_TRNSModel.PAYTP = null;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ORD_TRNSModel>(oRD_TRNSModel, "QueryType"); ;
            IEnumerable<ORD_TRNSModel> result = await _dataRepository.ExecuteQueryAsync<ORD_TRNSModel>(SPConstant.SP_ManageOrdersTransactions, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public async Task<int> UpdateOrdersTransactionsDetails(ORD_TRNSModel oRD_TRNSModel)
        {

            BadRequestResult badRequest = new BadRequestResult();

            if (oRD_TRNSModel == null)
                return badRequest.StatusCode;
            if (string.IsNullOrEmpty(oRD_TRNSModel.PAT_ID)) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Update;
            oRD_TRNSModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ORD_TRNSModel>(oRD_TRNSModel, "QueryType"); ;
            IEnumerable<ORD_TRNSModel> result = await _dataRepository.ExecuteQueryAsync<ORD_TRNSModel>(SPConstant.SP_ManageOrdersTransactions, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public async Task<int> UpdateActiveTestsRequest(ATRModel aTRModel)//Update_ATR inside
        {
            //ATRModel aTRModel = new ATRModel();
            //aTRModel.ATRID = ATRID;
            //aTRModel.CNLD = CNLD;
            //aTRModel.RS = RS;
            //aTRModel.NOTES = Notes;
            //aTRModel.FULL_NAME = "Cancelled Test";

            BadRequestResult badRequest = new BadRequestResult();

            if (aTRModel == null)
                return badRequest.StatusCode;
            if (aTRModel.ATRID == 0) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Update;
            aTRModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ATRModel>(aTRModel, "QueryType"); ;
            IEnumerable<ATRModel> result = await _dataRepository.ExecuteQueryAsync<ATRModel>(SPConstant.SP_ManageActiveTestsRequest, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public async Task<int> UpdateOrdersDetails(ORD_DTLModel oRD_DTLModel)//Update_ATR inside
        {
            //ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
            //oRD_DTLModel.ATRID = ATRID;
            //oRD_DTLModel.CNLD = CNLD;
            //oRD_DTLModel.RS = RS;
            //oRD_DTLModel.NOTES = Notes;
            //oRD_DTLModel.FULL_NAME = "Cancelled Test";

            BadRequestResult badRequest = new BadRequestResult();

            if (oRD_DTLModel == null)
                return badRequest.StatusCode;
            if (oRD_DTLModel.ATRID == 0) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Update;
            oRD_DTLModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ORD_DTLModel>(oRD_DTLModel, "QueryType");
            IEnumerable<ORD_DTLModel> result = await _dataRepository.ExecuteQueryAsync<ORD_DTLModel>(SPConstant.SP_ManageOrdersDetails, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> UpdateActiveResultsFile(ARFModel aRFModel)//Update_ATR inside
        {

            BadRequestResult badRequest = new BadRequestResult();

            if (aRFModel == null)
                return badRequest.StatusCode;
            if (aRFModel.ATRID == 0) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Update;
            aRFModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ARFModel>(aRFModel, "QueryType"); ;
            IEnumerable<ARFModel> result = await _dataRepository.ExecuteQueryAsync<ARFModel>(SPConstant.SP_ManageActiveResultsFile, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public async Task<int> UpdateCytogeneticsCases(FreeTextResultsModel freeTextResultsModel)
        {
            int queryType = (int)QueryTypeEnum.Update;
            freeTextResultsModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<FreeTextResultsModel>(freeTextResultsModel);
            IEnumerable<FreeTextResultsModel> result = await _dataRepository.ExecuteQueryAsync<FreeTextResultsModel>(SPConstant.SP_ManageCytogeneticsCases, parameterCollection);
            if (result != null && result.Any())
                return 1;
            else
                return 0;
        }
        public async Task<int> UpdateCytogeneticsQualityControl(CytogeneticsQCModel cytogeneticsQCModel)
        {
            int queryType = (int)QueryTypeEnum.Update;
            cytogeneticsQCModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CytogeneticsQCModel>(cytogeneticsQCModel);
            IEnumerable<CytogeneticsQCModel> result = await _dataRepository.ExecuteQueryAsync<CytogeneticsQCModel>(SPConstant.SP_ManageCytogeneticsQualityControl, parameterCollection);
            if (result != null && result.Any())
                return 1;
            else
                return 0;
        }
        public async Task<int> UpdateAnatomicPathologyCases(AnatomicPathologyCasesModel anatomicPathologyCasesModel)
        {
            int queryType = (int)QueryTypeEnum.Update;
            anatomicPathologyCasesModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<AnatomicPathologyCasesModel>(anatomicPathologyCasesModel);
            IEnumerable<AnatomicPathologyCasesModel> result = await _dataRepository.ExecuteQueryAsync<AnatomicPathologyCasesModel>(SPConstant.SP_ManageAnatomicPathologyCases, parameterCollection);
            if (result != null && result.Any())
                return 1;
            else
                return 0;
        }

        #endregion

        #region Orders Miscellaneuos
        public async Task<int> CancelActiveTestRequest(int ATRID, string R_STS, string CNLD, string Notes, string SITE_NO, string U_ID, string ORD_NO, string SECT, string REQ_CODE)//Update_ATR Main Methods
        {
            ATRModel aTRModel = new ATRModel();
            aTRModel.ATRID = ATRID;
            aTRModel.ATR_ID = ATRID;
            aTRModel.CNLD = CNLD;
            aTRModel.R_STS = R_STS;
            aTRModel.NOTES = Notes;
            aTRModel.FULL_NAME = "Cancelled Test";
            aTRModel.X_ID = U_ID;
            aTRModel.ORD_NO = ORD_NO;
            BadRequestResult badRequest = new BadRequestResult();

            if (aTRModel == null)
                return badRequest.StatusCode;
            if (aTRModel.ATRID == 0) return badRequest.StatusCode;

            int result = await UpdateActiveTestsRequest(aTRModel);
            if (result == 1)
            {
                ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
                oRD_DTLModel.ATRID = ATRID;
                oRD_DTLModel.CNLD = CNLD;
                oRD_DTLModel.R_STS = R_STS;
                //oRD_DTLModel.NOTES = Notes;
                //oRD_DTLModel.FULL_NAME = "Cancelled Test";

                await UpdateOrdersDetails(oRD_DTLModel);

                ORD_TRCModel oRD_TRCModel = new ORD_TRCModel();
                oRD_TRCModel.ORD_NO = ORD_NO;
                oRD_TRCModel.U_ID = U_ID;
                oRD_TRCModel.STS = "RJ";
                oRD_TRCModel.SECT = SECT;
                oRD_TRCModel.REQ_CODE = REQ_CODE;
                oRD_TRCModel.SITE_NO = SITE_NO;
                await InsertOrdersTracking(oRD_TRCModel);


                ARFModel aRFModel = new ARFModel();
                aRFModel.ATRID = ATRID;
                //aRFModel.CNLD = CNLD;
                aRFModel.R_STS = R_STS == "X" ? "XX" : R_STS;
                aRFModel.NOTES = Notes;
                aRFModel.FULL_NAME = "Cancelled Test";
                await UpdateActiveResultsFile(aRFModel);
                return 1;
            }
            else
            {
                return badRequest.StatusCode;
                //return 0;
            }

        }
        public async Task<int> CollectedActiveTestRequest(Object[] ATRs)//, int ATR_ID, string STS, DateTime DRAWN_DTTM, string ACCN, string REQ_CODE, string ORD_NO, string SECT, string REF_LAB)
        {
            string ACCN_GT = "";
            int rsponse = 0;
            var ATR = ATRs.ToList();
            string PRFX = "";
            string PREVREF_SITE = "";
            string REF_SITE = "";
            foreach (var objATRs in ATR)
            {
                string jsonString = JsonSerializer.Serialize(objATRs);
                var atrJson = JsonObject.Parse(jsonString);

                var ATRID = Convert.ToInt32(atrJson["ATR_ID"].ToString());
                var ATR_ID = Convert.ToInt32(atrJson["ATR_ID"].ToString());
                var STS = atrJson["STS"].ToString();
                var DRAWN_DTTM = Convert.ToDateTime(atrJson["DRAWN_DTTM"].ToString());
                var ACCN = atrJson["ACCN"] == null ? "" : atrJson["ACCN"].ToString();
                var REQ_CODE = atrJson["REQ_CODE"].ToString(); // REQ_CODE;
                var ORD_NO = atrJson["ORD_NO"].ToString();
                var SECT = atrJson["SECT"].ToString();
                var U_ID = atrJson["U_ID"].ToString();
                var MDL = atrJson["MDL"].ToString();
                var DOB1 = atrJson["DOB"].ToString();
                var SEX = atrJson["SEX"].ToString();
                var SITE_NO = atrJson["SITE_NO"].ToString();
                REF_SITE = atrJson["REF_SITE"].ToString();



                var getSITE_DTL = GetRefSitefromSiteDetails(SITE_NO).Result.FirstOrDefault();
                if (getSITE_DTL == null)
                    REF_SITE = "01";
                else
                    REF_SITE = getSITE_DTL.REF_SITE;



                if (ACCN == null || ACCN == "null" || ACCN == "")
                {
                    var GET_TD = _tDRepository.GetTestDirectoryByTCode(REQ_CODE).Result.FirstOrDefault();
                    if (GET_TD == null)
                    {
                        var GET_GT = _gTRepository.GetGroupTestsByReqCode(REQ_CODE).Result.FirstOrDefault();
                        if (PREVREF_SITE + PRFX != REF_SITE + GET_GT.PRFX)
                        {
                            PRFX = GET_GT.PRFX;
                            ACCN_GT = await GenerateAccessionNumber(REF_SITE, PRFX);
                            ACCN = ACCN_GT;
                            PREVREF_SITE = REF_SITE;
                        }
                        else
                            ACCN = ACCN_GT;
                    }
                    else
                    {
                        var itemTD = GET_TD;
                        if (PREVREF_SITE + PRFX != REF_SITE + itemTD.PRFX)
                        {
                            PRFX = itemTD.PRFX;
                            ACCN_GT = await GenerateAccessionNumber(REF_SITE, PRFX);
                            ACCN = ACCN_GT;
                            PREVREF_SITE = REF_SITE;
                        }
                        else
                            ACCN = ACCN_GT;
                    }
                }


                ATRModel aTRModel = new ATRModel();

                aTRModel.ATRID = ATR_ID;
                aTRModel.ATR_ID = ATR_ID;
                aTRModel.STS = STS;
                aTRModel.DRAWN_DTTM = DRAWN_DTTM;
                aTRModel.ACCN = ACCN;
                aTRModel.REQ_CODE = REQ_CODE;
                aTRModel.ORD_NO = ORD_NO;
                aTRModel.SECT = SECT;
                aTRModel.R_STS = "C";
                aTRModel.SITE_NO = SITE_NO;
                BadRequestResult badRequest = new BadRequestResult();


                if (aTRModel == null)
                    return badRequest.StatusCode;
                if (aTRModel.ATRID == 0) return badRequest.StatusCode;

                int updatedValue = await UpdateActiveTestsRequest(aTRModel);


                //Udpate Order Details
                ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
                oRD_DTLModel.ACCN = ACCN;
                oRD_DTLModel.ATRID = ATR_ID;
                oRD_DTLModel.REQ_CODE = REQ_CODE;
                oRD_DTLModel.DRAWN_DTTM = DRAWN_DTTM;
                oRD_DTLModel.STS = STS;
                oRD_DTLModel.R_STS = "C";
                oRD_DTLModel.SITE_NO = SITE_NO;
                oRD_DTLModel.REF_SITE = REF_SITE;
                oRD_DTLModel.ORD_NO = ORD_NO;
                int updatedValue_ORD_DETL = await UpdateOrdersDetails(oRD_DTLModel);


                //Added on June 1, 2024 as per new business requirements requested by Mr. Rashid 
                if (MDL == "CG")
                {
                    FreeTextResultsModel freeTextResultsModel = new FreeTextResultsModel();
                    freeTextResultsModel.ORD_NO = oRD_DTLModel.ORD_NO;
                    freeTextResultsModel.ACCN = oRD_DTLModel.ACCN;
                    freeTextResultsModel.REQ_CODE = oRD_DTLModel.REQ_CODE;
                    await UpdateCytogeneticsCases(freeTextResultsModel);

                    CytogeneticsQCModel cytogeneticsQCModel = new CytogeneticsQCModel();
                    cytogeneticsQCModel.ORD_NO = oRD_DTLModel.ORD_NO;
                    cytogeneticsQCModel.ACCN = oRD_DTLModel.ACCN;
                    cytogeneticsQCModel.REQ_CODE = oRD_DTLModel.REQ_CODE;
                    await UpdateCytogeneticsQualityControl(cytogeneticsQCModel);
                }
                if (MDL == "AP")
                {

                    AnatomicPathologyCasesModel anatomicPathologyCasesModel = new AnatomicPathologyCasesModel();
                    anatomicPathologyCasesModel.ORD_NO = oRD_DTLModel.ORD_NO;
                    anatomicPathologyCasesModel.ACCN = oRD_DTLModel.ACCN;
                    anatomicPathologyCasesModel.REQ_CODE = oRD_DTLModel.REQ_CODE;
                    anatomicPathologyCasesModel.MDL = oRD_DTLModel.MDL;
                    await UpdateAnatomicPathologyCases(anatomicPathologyCasesModel);

                }

                //
                ORD_TRCModel oRD_TRCModel = new ORD_TRCModel();
                oRD_TRCModel.SITE_NO = aTRModel.SITE_NO;
                oRD_TRCModel.ORD_NO = aTRModel.ORD_NO;

                //if (aTRModel.STS == "OD") STS = "01";
                //if (aTRModel.STS == "RS") STS = "03";
                //if (aTRModel.STS == "CO") STS = "05";
                //if (aTRModel.STS == "IT") STS = "07";
                //if (aTRModel.STS == "PA") STS = "09";
                //if (aTRModel.STS == "CR") STS = "11";
                //if (aTRModel.STS == "QR") STS = "13";
                //if (aTRModel.STS == "DL") STS = "15";
                //if (aTRModel.STS == "ST") STS = "17";
                //if (aTRModel.STS == "DM") STS = "19";
                //if (aTRModel.STS == "RJ") STS = "21";
                //if (aTRModel.STS == "DS") STS = "23";
                //if (aTRModel.STS == "CK") STS = "25";


                oRD_TRCModel.STS = aTRModel.STS;// STS;
                oRD_TRCModel.SECT = aTRModel.SECT;
                oRD_TRCModel.REQ_CODE = aTRModel.REQ_CODE;
                oRD_TRCModel.ACT_DTTM = DateTime.Now;
                oRD_TRCModel.U_ID = U_ID;// "999";
                oRD_TRCModel.SITE_NO = SITE_NO;
                await InsertOrdersTracking(oRD_TRCModel);


                //Update ARF Details
                ARFModel aRFModel = new ARFModel();

                var ARFList = await GetActiveResultFileDuringCollection(ORD_NO, REQ_CODE, ATR_ID);

                foreach (var itemARF in ARFList)
                {
                    aRFModel.ACCN = ACCN;
                    aRFModel.ATRID = ATR_ID;
                    aRFModel.REQ_CODE = REQ_CODE;
                    aRFModel.TCODE = itemARF.TCODE;
                    aRFModel.DRAWN_DTTM = DRAWN_DTTM;
                    aRFModel.STS = aTRModel.STS;//STS;
                    aRFModel.R_STS = "C";
                    aRFModel.LAST_UPDT = DateTime.Now;
                    aRFModel.SITE_NO = SITE_NO;
                    aRFModel.REF_SITE = REF_SITE;
                    aRFModel.SEX = SEX;
                    aRFModel.RSTP = itemARF.RSTP;

                    //Computer for the Reference Range

                    //DateTime DRAWN_DTTM = new DateTime();
                    DateTime DOB = new DateTime();
                    DRAWN_DTTM = Convert.ToDateTime(string.IsNullOrEmpty(aRFModel.DRAWN_DTTM.ToString()) ? DRAWN_DTTM.ToString() : aRFModel.DRAWN_DTTM.ToString());
                    DOB = Convert.ToDateTime(DOB1);// aRFModel.DOB.ToString());
                    int age = DRAWN_DTTM.Subtract(DOB).Days;


                    var GET_REF_RNG = _masterRepository.GetReferenceRanges().Result // await _appDbContext.REF_RNG
                     .Where(x => x.TCODE.Trim() == aRFModel.TCODE.Trim()
                                             && x.RSTP.Trim() == aRFModel.RSTP.Trim()
                                             && x.SEX.Trim() == aRFModel.SEX.Trim()
                                             && (age >= x.AGE_FROM && age <= x.AGE_TO)
                                             //&& x.AGE_FROM == rEF_RNGModel.AGE_FROM
                                             ).FirstOrDefault();
                    if (GET_REF_RNG != null)
                    {
                        aRFModel.REF_LOW = GET_REF_RNG.REF_LOW;
                        aRFModel.REF_HIGH = GET_REF_RNG.REF_HIGH;

                        aRFModel.CRTCL_LOW = GET_REF_RNG.CRTCL_HIGH;
                        aRFModel.CRTCL_HIGH = GET_REF_RNG.CRTCL_LOW;

                        aRFModel.REF_RANGE = GET_REF_RNG.REF_RANGE;
                        aRFModel.REF_LC = GET_REF_RNG.REF_LC;
                        aRFModel.REF_HC = GET_REF_RNG.REF_HC;
                    }
                    else
                    {
                        aRFModel.REF_LOW = null;
                        aRFModel.REF_HIGH = null;

                        aRFModel.CRTCL_LOW = null;
                        aRFModel.CRTCL_HIGH = null;

                        aRFModel.REF_RANGE = null;
                        aRFModel.REF_LC = null;
                        aRFModel.REF_HC = null;
                    }

                    ////////////////////


                    int updatedValue_ARF = await UpdateActiveResultsFile(aRFModel);
                    rsponse = updatedValue_ARF;
                }
            }
            return rsponse;


        }

        public async Task<int> AddNotesActiveTestsRequest(ATRModel aTRModel)//Update_ATR inside
        {
            BadRequestResult badRequest = new BadRequestResult();

            if (aTRModel == null)
                return badRequest.StatusCode;
            //if (aTRModel.ATRID == 0) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Update;
            aTRModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ATRModel>(aTRModel, "QueryType"); ;
            IEnumerable<ATRModel> result = await _dataRepository.ExecuteQueryAsync<ATRModel>(SPConstant.SP_ManageActiveTestsRequest, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public async Task<int> UpdateIssueInvoince(ORD_TRNSModel oRD_TRNSModel)//Update_ATR
        {

            BadRequestResult badRequest = new BadRequestResult();

            if (oRD_TRNSModel == null)
                return badRequest.StatusCode;
            if (string.IsNullOrEmpty(oRD_TRNSModel.PAT_ID)) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Update;
            oRD_TRNSModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ORD_TRNSModel>(oRD_TRNSModel, "QueryType"); ;
            IEnumerable<ORD_TRNSModel> result = await _dataRepository.ExecuteQueryAsync<ORD_TRNSModel>(SPConstant.SP_ManageOrdersTransactions, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }


        //Multiple Search
        public async Task<IEnumerable<MultipleSearchOrderModel>> GetMultipleSearch(string IDNT, string TEL, string PAT_ID, string PAT_NAME, DateTime? DOB, string SEX, string ORD_NO, string ACCN, string MDL, DateTime? FromDate, DateTime? ToDate)
        {
                MultipleSearchModel multipleSearchModel = new MultipleSearchModel();
                int queryType = (int)QueryTypeEnum.GetById;
                multipleSearchModel.QueryType = queryType;
                multipleSearchModel.IDNT = IDNT == "null" ? "" : IDNT;
                multipleSearchModel.TEL = TEL == "null" ? "" : TEL;
                multipleSearchModel.PAT_ID = PAT_ID == "null" ? "" : PAT_ID;
                multipleSearchModel.PAT_NAME = PAT_NAME == "null" ? "" : PAT_NAME;
                multipleSearchModel.DOB = DOB == DateTime.MinValue ? null : DOB.Value.ToString("MM/dd/yyyy");
                multipleSearchModel.FromDate = FromDate == DateTime.MinValue ? null :FromDate.Value.ToString("MM/dd/yyyy");
                multipleSearchModel.ToDate =  ToDate == DateTime.MinValue ? null : ToDate.Value.ToString("MM/dd/yyyy"); 
                multipleSearchModel.SEX = SEX == "null" ? "" : SEX;
                multipleSearchModel.ORD_NO = ORD_NO == "null" ? "" : ORD_NO;
                multipleSearchModel.ACCN = ACCN == "null" ? "" : ACCN;
                multipleSearchModel.MDL = MDL == "null" ? "" : MDL;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<MultipleSearchModel>(multipleSearchModel);
                return (await _dataRepository.ExecuteQueryAsync<MultipleSearchOrderModel>(SPConstant.SP_MultipleSearch, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<MultipleSearchOrderModel>> GetMultipleSearchOrders(string PAT_ID)
        {
            // MultipleSearchOrderModel multipleSearchOrderModel = new MultipleSearchOrderModel();
            int queryType = (int)QueryTypeEnum.GetById;
            //multipleSearchOrderModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(PAT_ID, queryType, "PAT_ID", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<MultipleSearchOrderModel>(SPConstant.SP_MultipleSearchOrders, parameterCollection)).ToList();
        }


        #endregion

        #region Environmental Orders

        public async Task<IEnumerable<EVOrderModel>> GetAllEnvironmentalOrder(string pSize)
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(pSize,queryType, "VC_NO", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<EVOrderModel>(SPConstant.SP_ManageEnvironmentalOrders, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<EVOrderATRModel>> GetForEnvironmentalOrderATRData(EVOrderATRModel evOrder)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(evOrder.ORD_NO, queryType, "@ORD_NO", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<EVOrderATRModel>(SPConstant.SP_ManageEnvironmentalOrdersATR, parameters)).ToList();
        }
        public async Task<IEnumerable<EVOrderListModel>> GetEnvironmentalOrderList(int pendingDays,bool isPending)
        {
            EVOrderDetailsModel eVOrderDetailsModel = new EVOrderDetailsModel();
            eVOrderDetailsModel.QueryType = (int)QueryTypeEnum.GetAll;
            eVOrderDetailsModel.PendingDays = pendingDays;
            eVOrderDetailsModel.IsPending = isPending;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(eVOrderDetailsModel);
            return (await _dataRepository.ExecuteQueryAsync<EVOrderListModel>(SPConstant.SP_ManageEVOrdersList, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<EVDetailModel>> GetEVOrderPatientDetails(EvResultDetailModel evResultModel)
        {
            evResultModel.ACCN = evResultModel.ACCN.Replace("-", "");
            evResultModel.QueryType = (int)QueryTypeEnum.Update;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EvResultDetailModel>(evResultModel, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<EVDetailModel>(SPConstant.SP_ManageEVOrdersDetails, parameterCollection)).ToList();
        }


        public async Task<IEnumerable<EVClientModel>> GetAllClients()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<EVClientModel>(SPConstant.Sp_Clients, parameters)).ToList();
        }

        public async Task<IEnumerable<EVSampleModel>> GetAllEVSample()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<EVSampleModel>(SPConstant.SP_ManageEnvironmentalSample, parameters)).ToList();
        }

        public async Task<IEnumerable<EVPatientModel>> GetPatientNextId()
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<EVPatientModel>(SPConstant.SP_ManageEnvironmentalOrders, parameters)).ToList();
        }

        public IEnumerable<EVPatientModel> GetPatientNextIdSync()
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (_dataRepository.ExecuteQuery<EVPatientModel>(SPConstant.SP_ManageEnvironmentalOrders, parameters)).ToList();
        }

        public async Task<string> InsertEVOrder(EVOrderModel evOrderModel)
        {

            if (evOrderModel.PAT_ID == string.Empty)
                evOrderModel.PAT_ID = GetPatientNextIdSync().FirstOrDefault().PAT_ID;


            if (evOrderModel.ORD_NO != string.Empty)
                DeleteEnvironmentalOrderByOrderNo(evOrderModel);

            int queryType = (int)QueryTypeEnum.Insert;
            evOrderModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVOrderModel>(evOrderModel, "QueryType");
            IEnumerable<EVOrderModel> result = await _dataRepository.ExecuteQueryAsync<EVOrderModel>(SPConstant.SP_ManageEnvironmentalOrders, parameterCollection);
            if (result != null && result.Any())
            {
                return Convert.ToString(Convert.ToInt64(evOrderModel.PAT_ID));
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<IEnumerable<TDModel>> GetAllEVTD(string TCode)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(TCode, queryType, "@SEARCH", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<TDModel>(SPConstant.SP_ManageEnvironmentalOrders, parameters)).ToList();
        }



        public async Task<int> InsertEVATR(List<EVOrderATRModel> ATRModel)
        {
            int rowsAffected = 0;
            try
            {
                List<EVOrderATRInsertModel> evListEVInsertATRModel = new List<EVOrderATRInsertModel>();

                foreach (var sen in ATRModel)
                {
                    var evInsertATRModel = new EVOrderATRInsertModel();
                    evInsertATRModel.SITE_NO = sen.SITE_NO;
                    evInsertATRModel.ORD_NO = sen.ORD_NO;
                    evInsertATRModel.PAT_ID = sen.PAT_ID;
                    evInsertATRModel.ACCN = sen.ACCN;

                    evInsertATRModel.REQ_CODE = sen.REQ_CODE;
                    evInsertATRModel.REQ_DTTM = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                    evInsertATRModel.DRAWN_DTTM = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                    evInsertATRModel.FULL_NAME = sen.FULL_NAME;
                    evInsertATRModel.TEST_ID = sen.TEST_ID;
                    evInsertATRModel.S_TYPE = sen.S_TYPE;
                    evInsertATRModel.R_STS = sen.R_STS;
                    evInsertATRModel.MDL = sen.MDL;
                    evInsertATRModel.DIV = sen.DIV;
                    evInsertATRModel.SECT = sen.SECT;
                    evInsertATRModel.PRTY = sen.PRTY;
                    evInsertATRModel.TS = sen.TS;
                    evInsertATRModel.UPRICE = Convert.ToDecimal(sen.UPRICE);
                    evInsertATRModel.DT = sen.DT;
                    evInsertATRModel.DSCNT = sen.DSCNT;
                    evInsertATRModel.DPRICE = sen.DPRICE;
                    evInsertATRModel.O_ID = sen.O_ID;
                    //evInsertATRModel.UPDT_TIME =sen.UPDT_TIME;
                    evListEVInsertATRModel.Add(evInsertATRModel);
                }

                DataTable dtEVInstertATR = CommonHelper.ToDataTable(evListEVInsertATRModel);
                if (dtEVInstertATR.Columns.Contains("QueryType"))
                    dtEVInstertATR.Columns.Remove("QueryType");
                rowsAffected = await _dataRepository.ExecuteDataTable(SPConstant.Sp_InsertEnvironmentalOrderATR, dtEVInstertATR, SPConstant.type_InsertEnvironmentalOrderATR);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }

        private int DeleteEnvironmentalOrderByOrderNo(EVOrderModel evOrderModel)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            evOrderModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVOrderModel>(evOrderModel, "QueryType");
            IEnumerable<EVOrderModel> result = _dataRepository.ExecuteQuery<EVOrderModel>(SPConstant.SP_ManageEnvironmentalOrders, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<EVSearch>> GetAllEVPatientSearch(string OrderNo)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(OrderNo, queryType, "@ORD_NO", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<EVSearch>(SPConstant.SP_ManageEnvironmentalOrdersATR, parameters)).ToList();
        }


        public async Task<IEnumerable<EVPatientModel>> UpdateInvoiceNo(string OrderNo)
        {
            int queryType = (int)QueryTypeEnum.Update;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(OrderNo, queryType, "@ORD_NO", "QueryType");
            var _invoice = (await _dataRepository.ExecuteQueryAsync<EVPatientModel>(SPConstant.SP_ManageEnvironmentalOrdersATR, parameters)).ToList();

            #region QR Code Generation


            queryType = (int)QueryTypeEnum.GetAll;
            parameters = ParameterGenerator.CreateParameterList(OrderNo, queryType, "@ORD_NO", "QueryType");
            var _td = (_dataRepository.ExecuteQuery<EVPrintRModel>(SPConstant.SP_ManageEnvironmentalOrdersATR, parameters)).ToList();
            double total = 0;
            if (_td != null)
                total = Convert.ToDouble(_td.Sum(item => item.DPRICE));

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(getBase64("Lambda Laboratory", "300010985700003", DateTime.Now.ToString(), (total + (total * 0.15)).ToString(), (total * 0.15).ToString()), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            byte[] data;
            using (MemoryStream m = new MemoryStream())
            {
                qrCodeImage.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                data = m.ToArray();
            }


            APImageModel _APImageModel = new APImageModel();
            _APImageModel.ACCN = _invoice[0].VC_NO.ToString();
            _APImageModel.TCODE = _invoice[0].PAT_ID.ToString();
            _APImageModel.IMAGE = Convert.ToBase64String(data);

            InsertQRImage(_APImageModel);

            #endregion
            
            return _invoice;
        }


        //For Print
        public string GetEVPrint(string OrderNo)
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(OrderNo, queryType, "@ORD_NO", "QueryType");
            var _td = (_dataRepository.ExecuteQuery<EVPrintRModel>(SPConstant.SP_ManageEnvironmentalOrdersATR, parameters)).ToList();
            var pdf = InvoicePdf(_td);
            return Convert.ToBase64String(pdf, 0, pdf.Length);

        }


        /// <summary>
        ///  Generate PDF
        /// </summary>
        /// <param name="CodePath"></param>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        private byte[] InvoicePdf(List<EVPrintRModel> taskModel)
        {
            try
            {
                string QRFile = @"\" + "QR.png";
                QuestPDF.Fluent.Document document = QuestPDF.Fluent.Document.Create(container =>
                {
                    var headerStyle = TextStyle.Default.FontFamily("Calibri");
                    _ = container
                    .Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(0.5f, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(headerStyle);

                        #region Page Header

                        page.Header()
                               .PaddingBottom(5)
                               .PaddingTop(5)
                               .Border(0.5f)
                               .Table(table =>
                               {
                                   table.ColumnsDefinition(columns =>
                                   {
                                       columns.ConstantColumn(190, Unit.Millimetre);
                                   });

                                   byte[] blankBytes = Convert.FromBase64String("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");
                                   table.Cell().Row(1).Column(1).Border(0).BorderBottom(0).PaddingRight(0).PaddingTop(7).PaddingBottom(7).PaddingLeft(60).AlignCenter().AlignTop().Image(blankBytes);

                               });

                        #endregion

                        page.Content()
                                .Height(700)
                                .Border(0.0f)
                                .Border(0.5f).PaddingLeft(3).PaddingLeft(15).PaddingTop(2.5f)
                                .Table(table =>
                                {

                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(20, Unit.Millimetre);
                                        columns.ConstantColumn(22, Unit.Millimetre);
                                        columns.ConstantColumn(20, Unit.Millimetre);
                                        columns.ConstantColumn(33, Unit.Millimetre);
                                        columns.ConstantColumn(33, Unit.Millimetre);
                                        columns.ConstantColumn(20, Unit.Millimetre);
                                        columns.ConstantColumn(22, Unit.Millimetre);
                                        columns.ConstantColumn(20, Unit.Millimetre);

                                    });

                                    uint i = 0;


                                    #region Invoice Headers

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).AlignLeft().PaddingTop(0).PaddingBottom(0).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Date");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).AlignMiddle().PaddingTop(0).PaddingBottom(0).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(DateTime.Now.ToString("dd/MM/yyyy"));
                                    });
                                    table.Cell().Row(i).Column(3).ColumnSpan(1).Border(0.0f).AlignRight().PaddingTop(0).PaddingBottom(0).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("التاريخ");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).Border(0.0f).AlignCenter().PaddingTop(0).PaddingBottom(0).PaddingLeft(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("Credit Invoice");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignLeft().PaddingTop(0).PaddingBottom(0).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(6).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Inv. No");
                                    });
                                    table.Cell().Row(i).Column(7).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(taskModel[0].VC_NO);
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).AlignRight().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("فاتورة رقم");
                                    });


                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).Border(0.0f).AlignRight().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).Border(0.0f).AlignLeft().PaddingLeft(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("Vat Number");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("300010985700003");
                                    });


                                    table.Cell().Row(i).Column(6).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(7).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).AlignRight().AlignCenter().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });


                                    i++;
                                    table.Cell().Row(i).Column(1).ColumnSpan(2).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Sample Description");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).Border(0.0f).AlignRight().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).Border(0.0f).AlignCenter().PaddingLeft(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(taskModel[0].PAT_NAME + " ( " + taskModel[0].PAT_ID + " )");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(7).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).AlignRight().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("نوع العينة");
                                    });

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Client");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).Border(0.0f).AlignRight().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).Border(0.0f).AlignLeft().PaddingLeft(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(taskModel[0].CLIENT);
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(6).ColumnSpan(2).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(7).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).AlignRight().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("السادة");
                                    });

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).AlignLeft().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Client VAT");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(2).Border(0.0f).AlignMiddle().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(taskModel[0].CLNTVAT);
                                    });
                                    table.Cell().Row(i).Column(3).Border(0.0f).AlignRight().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).Border(0.0f).AlignLeft().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("لرقم الضريبي للعميل");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignLeft().PaddingLeft(5).PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(6).Border(0.0f).AlignMiddle().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Ref No.");
                                    });
                                    table.Cell().Row(i).Column(7).Border(0.0f).AlignMiddle().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(taskModel[0].REF_NO);
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).AlignRight().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("رقم البيان");
                                    });

                                    #endregion

                                    #region Invoice Items

                                    #region Item Header
                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("الرمز");
                                        text.Span("\n");
                                        text.Span("Code");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(4).Border(0.1f).AlignLeft().AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("اسم التحليل");
                                        text.Span("\n");
                                        text.Span("Description");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.1f).BorderBottom(0.1f).AlignRight().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.1f).BorderBottom(0.1f).AlignLeft().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));

                                    });
                                    table.Cell().Row(i).Column(5).BorderTop(0.1f).BorderBottom(0.1f).AlignCenter().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                    });
                                    table.Cell().Row(i).Column(6).Border(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("السعر");
                                        text.Span("\n");
                                        text.Span("Unit Price");
                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.1f).BorderBottom(0.1f).AlignMiddle().PaddingTop(5).PaddingLeft(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("الخصم");
                                        text.Span("\n\t");
                                        text.Span("Discount");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("الاجمالي");
                                        text.Span("\n");
                                        text.Span("D/Price");
                                    });

                                    #endregion

                                    decimal _total = 0; decimal _sumPrice = 0;
                                    for (int item = 0; item < taskModel.Count; item++)
                                    {
                                        i++;
                                        table.Cell().Row(i).Column(1).Border(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span(taskModel[item].REQ_CODE);
                                        });
                                        table.Cell().Row(i).Column(2).ColumnSpan(4).Border(0.1f).AlignLeft().AlignMiddle().PaddingLeft(5).PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span(taskModel[item].FULL_NAME);
                                        });
                                        table.Cell().Row(i).Column(3).BorderTop(0.1f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });

                                        table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.1f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                        });
                                        table.Cell().Row(i).Column(5).BorderTop(0.1f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));

                                        });
                                        table.Cell().Row(i).Column(6).Border(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span(taskModel[item].UPRICE);
                                        });
                                        table.Cell().Row(i).Column(7).BorderTop(0.1f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span(taskModel[item].DSCNT.ToString());
                                        });
                                        table.Cell().Row(i).Column(8).Border(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span(taskModel[item].DPRICE.ToString());
                                        });

                                        _total = _total + (taskModel[item].DPRICE);
                                        _sumPrice = _sumPrice + Convert.ToDecimal(taskModel[item].UPRICE);
                                    }

                                    #endregion

                                    #region Total

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).BorderLeft(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(3).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(6).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("المجموع");

                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderBottom(0.0f).BorderRight(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Total");
                                    });
                                    table.Cell().Row(i).Column(8).BorderRight(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span(_total.ToString());
                                    });


                                    decimal _discount = (((_sumPrice - _total) / _sumPrice) * 100);
                                    decimal _netValue = (_sumPrice - _total);
                                    decimal _vatValue = ((_netValue * 15) / 100);
                                    decimal _grossValue = ((_netValue) + _vatValue);


                                    if (_discount != 0)
                                    {
                                        i++;
                                        table.Cell().Row(i).Column(1).Border(0.0f).BorderLeft(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(2).ColumnSpan(3).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });

                                        table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(5).Border(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span("الخصم");
                                        });


                                        table.Cell().Row(i).Column(6).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span(_discount.ToString("0.00") + " % ");

                                        });
                                        table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderBottom(0.0f).BorderRight(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span("Discount");
                                        });
                                        table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span((_sumPrice - _total).ToString());
                                        });


                                        i++;
                                        table.Cell().Row(i).Column(1).Border(0.0f).BorderLeft(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(2).ColumnSpan(3).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });

                                        table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(5).ColumnSpan(2).Border(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span("الاجمالي بعد الخصم");
                                        });

                                        table.Cell().Row(i).Column(6).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                        });
                                        table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.1f).BorderBottom(0.0f).AlignCenter().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span("Net Value");
                                        });
                                        table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span(_netValue.ToString());
                                        });
                                    }

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).BorderBottom(0.1f).BorderLeft(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(3).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).ColumnSpan(2).Border(0.0f).BorderBottom(0.1f).AlignRight().AlignMiddle().PaddingLeft(5).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("الضريبة المضافة");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("VAT");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span(_vatValue.ToString());
                                    });

                                    int grossInt = Convert.ToInt32(_grossValue);
                                    var _grossValueIntAr = ArabicWordConverter.ToArabicWord(Convert.ToDecimal(grossInt)) + " ريال ";
                                    var grossDecimal = Convert.ToDecimal((_grossValue.ToString().Substring((_grossValue.ToString().IndexOf(".")))).Replace(".", ""));
                                    var grossDecimalAr = ArabicWordConverter.ToArabicWord(grossDecimal);
                                    if (grossDecimal != 0)
                                        grossDecimalAr = " و " + ArabicWordConverter.ToArabicWord(grossDecimal) + " هللة";
                                    else
                                        grossDecimalAr = ArabicWordConverter.ToArabicWord(grossDecimal).ToString();

                                    grossDecimalAr = grossDecimalAr.ToString().Replace("صفر", "");

                                    i++;
                                    table.Cell().Row(i).Column(1).ColumnSpan(5).Border(0.0f).BorderBottom(0.1f).BorderLeft(0.1f).AlignRight().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span(NumberToWords.ConvertAmount(Convert.ToDouble(_grossValue)));
                                        text.Span("\n");
                                        text.Span(_grossValueIntAr + " " + grossDecimalAr.ToString());
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("الاجمالي");

                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().PaddingTop(10).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Net Total");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span(_grossValue.ToString());
                                    });


                                    #endregion

                                    i++;
                                    table.Cell().Row(i).Column(1).ColumnSpan(8).Border(0.0f).BorderBottom(0.0f).BorderLeft(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("عزيزي العميل: سيتم التخلص من العينة خلال 10 ايام عمل من تاريخ صدور النتيجة وفي حال رغبتكم في استلام العينة نأمل التكرم باستلامها");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");

                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });


                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).BorderBottom(0.0f).BorderLeft(0.0f).AlignCenter().AlignMiddle().PaddingTop(20).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("Signature");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(2).Border(0.0f).BorderBottom(0.0f).AlignLeft().AlignMiddle().PaddingTop(35).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("-------------------------------");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(25).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("التوقيع");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");

                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });


                                    #region QR Code Generation


                                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(getBase64("Lambda Laboratory", "300010985700003", DateTime.Now.ToString(), _grossValue.ToString(), _vatValue.ToString()), QRCodeGenerator.ECCLevel.Q);
                                    QRCode qrCode = new QRCode(qrCodeData);
                                    Bitmap qrCodeImage = qrCode.GetGraphic(20);
                                    byte[] data;
                                    using (MemoryStream m = new MemoryStream())
                                    {
                                        qrCodeImage.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                                        data = m.ToArray();
                                    }


                                    APImageModel _APImageModel = new APImageModel();
                                    _APImageModel.ACCN = taskModel[0].VC_NO.ToString();
                                    _APImageModel.TCODE = taskModel[0].PAT_ID.ToString();
                                    _APImageModel.IMAGE = Convert.ToBase64String(data);

                                    //InsertQRImage(_APImageModel);

                                    #endregion

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).BorderBottom(0.0f).BorderLeft(0.0f).AlignCenter().AlignMiddle().PaddingTop(40).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(2).Border(0.0f).BorderBottom(0.0f).AlignLeft().AlignMiddle().PaddingTop(35).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(25).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).ColumnSpan(3).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Width(100).Height(100).Image(data);
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.0f).BorderBottom(0.0f).PaddingTop(5);
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5);


                                });


                        page.Footer().DefaultTextStyle(x => x.FontSize(9))
                           .AlignLeft()
                           .BorderTop(0.5f)
                           .ContentFromLeftToRight()
                           .PaddingTop(5)
                           .Table(table =>
                           {
                               table.ColumnsDefinition(columns =>
                               {
                                   columns.ConstantColumn(30, Unit.Millimetre);
                                   columns.ConstantColumn(40, Unit.Millimetre);
                                   columns.ConstantColumn(5, Unit.Millimetre);
                                   columns.ConstantColumn(5, Unit.Millimetre);
                                   columns.ConstantColumn(20, Unit.Millimetre);
                                   columns.ConstantColumn(15, Unit.Millimetre);
                                   columns.ConstantColumn(15, Unit.Millimetre);
                                   columns.ConstantColumn(20, Unit.Millimetre);
                                   columns.ConstantColumn(40, Unit.Millimetre);
                               });

                               table.Cell().Row(1).Column(1).PaddingLeft(20).AlignRight().Text("Email:");
                               table.Cell().Row(1).Column(2).ColumnSpan(2).AlignLeft().Text("support@deltacare.com");

                               table.Cell().Row(1).Column(5).AlignRight().Text("Pin Code:");
                               table.Cell().Row(1).Column(6).AlignLeft().Text("51265");

                               table.Cell().Row(1).Column(7).AlignRight().Text("Phone:");
                               table.Cell().Row(1).Column(8).ColumnSpan(2).AlignLeft().Text("+966 11 3005415");

                               table.Cell().Row(1).Column(9).AlignRight().Text(x =>
                               {
                                   x.Span("Page ");
                                   x.CurrentPageNumber();
                                   x.Span(" Of ");
                                   x.TotalPages();
                               });

                           });
                    });

                });

                return document.GeneratePdf();
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public String getBase64(String sellername, String vatregistration, String timestamp, String invoiceamount, String vatamoun)
        {
            string ltr = ((char)0x200E).ToString();
            var seller = getTlvVAlue("1", sellername);
            var vatno = getTlvVAlue("2", vatregistration);
            var time = getTlvVAlue("3", timestamp);
            var invamt = getTlvVAlue("4", invoiceamount);
            var vatamt = getTlvVAlue("5", vatamoun);
            var result = seller.Concat(vatno).Concat(time).Concat(invamt).Concat(vatamt).ToArray();
            Console.WriteLine(result);
            ;
            Console.WriteLine(result.ToString());
            ;
            var output = Convert.ToBase64String(result);
            Console.WriteLine(output);
            return output;
        }



        public byte[] getTlvVAlue(String tagnums, String tagvalue)
        {
            string[] tagnums_array = { tagnums };
            var tagvalue1 = tagvalue;

            var tagnum = tagnums_array.Select(s => Byte.Parse(s)).ToArray();



            var tagvalueb = System.Text.Encoding.UTF8.GetBytes(tagvalue1);
            string[] taglengths = { tagvalueb.Length.ToString() };
            var tagvaluelengths = taglengths.Select(s => Byte.Parse(s)).ToArray();
            var tlvVAlue = tagnum.Concat(tagvaluelengths).Concat(tagvalueb).ToArray();


            return tlvVAlue;
        }




        //For Print
        public string GetEVMultiInvoicePrint(EvListMultiInvoicePrintModel evPrintModel)
        {
            try
            {
                EVOrderModel evOrderModel = new EVOrderModel();
                evOrderModel.QueryType = (int)QueryTypeEnum.Update;
                evOrderModel.CN = evPrintModel.ClientNo;
                evOrderModel.EXPR_DATE = (evPrintModel.SinceDate == String.Empty ? DateTime.Now.AddMonths(-2).ToString("MM/dd/yyyy") : evPrintModel.SinceDate);
                evOrderModel.SEARCH = (evPrintModel.Search.EndsWith(",") == true ? evPrintModel.Search.Remove(evPrintModel.Search.Length - 1) : evPrintModel.Search);
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVOrderModel>(evOrderModel, "QueryType");
                var _td = _dataRepository.ExecuteQuery<EVMultiInvoicePrintRModel>(SPConstant.SP_ManageEnvironmentalOrders, parameterCollection).ToList();
                if (_td != null && _td.Count > 0)
                {
                    var pdf = MultipleInvoicePdf(_td);
                    return Convert.ToBase64String(pdf, 0, pdf.Length);
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }



        /// <summary>
        ///  Generate PDF
        /// </summary>
        /// <param name="CodePath"></param>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        private byte[] MultipleInvoicePdf(List<EVMultiInvoicePrintRModel> taskModel)
        {
            try
            {
                QuestPDF.Fluent.Document document = QuestPDF.Fluent.Document.Create(container =>
                {
                    var headerStyle = TextStyle.Default.FontFamily("Calibri");
                    _ = container
                    .Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(0.5f, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(headerStyle);

                        #region Page Header

                        page.Header()
                               .PaddingBottom(5)
                               .PaddingTop(5)
                               .Border(0.5f)
                               .Table(table =>
                               {
                                   table.ColumnsDefinition(columns =>
                                   {
                                       columns.ConstantColumn(190, Unit.Millimetre);
                                   });

                                   byte[] blankBytes = Convert.FromBase64String("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");
                                   table.Cell().Row(1).Column(1).Border(0).BorderBottom(0).PaddingRight(0).PaddingTop(7).PaddingBottom(7).PaddingLeft(60).AlignCenter().AlignTop().Image(blankBytes);

                               });

                        #endregion

                        page.Content()
                                .Height(700)
                                .Border(0.0f)
                                .Border(0.5f).PaddingLeft(3).PaddingLeft(15).PaddingTop(2.5f)
                                .Table(table =>
                                {

                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(20, Unit.Millimetre);
                                        columns.ConstantColumn(22, Unit.Millimetre);
                                        columns.ConstantColumn(33, Unit.Millimetre);
                                        columns.ConstantColumn(20, Unit.Millimetre);
                                        columns.ConstantColumn(33, Unit.Millimetre);
                                        columns.ConstantColumn(20, Unit.Millimetre);
                                        columns.ConstantColumn(22, Unit.Millimetre);
                                        columns.ConstantColumn(20, Unit.Millimetre);

                                    });

                                    uint i = 0;


                                    #region Invoice Headers

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).AlignLeft().PaddingTop(0).PaddingBottom(0).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Date");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).AlignMiddle().PaddingTop(0).PaddingBottom(0).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(DateTime.Now.ToString("dd/MM/yyyy"));
                                    });
                                    table.Cell().Row(i).Column(3).ColumnSpan(1).Border(0.0f).AlignRight().PaddingTop(0).PaddingBottom(0).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("التاريخ");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).Border(0.0f).AlignCenter().PaddingTop(0).PaddingBottom(0).PaddingLeft(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("Credit Invoice");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignLeft().PaddingTop(0).PaddingBottom(0).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(6).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Inv. No");
                                    });
                                    table.Cell().Row(i).Column(7).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(taskModel[0].VC_NO);
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).AlignRight().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("فاتورة رقم");
                                    });


                                    i++;
                                    table.Cell().Row(i).Column(1).PaddingTop(3).PaddingBottom(3).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(2).PaddingTop(3).PaddingBottom(3).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));

                                    });
                                    table.Cell().Row(i).Column(3).ColumnSpan(2).PaddingTop(3).PaddingBottom(3).Border(0.0f).AlignCenter().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("Vat Number");
                                    });

                                    table.Cell().Row(i).Column(4).PaddingTop(3).PaddingBottom(3).Border(0.0f).AlignLeft().PaddingLeft(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));

                                    });
                                    table.Cell().Row(i).Column(5).PaddingTop(3).PaddingBottom(3).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("300010985700003");
                                    });


                                    table.Cell().Row(i).Column(6).PaddingTop(3).PaddingBottom(3).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(7).PaddingTop(3).PaddingBottom(3).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(8).PaddingTop(3).PaddingBottom(3).Border(0.0f).AlignRight().AlignCenter().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });


                                    i++;
                                    table.Cell().Row(i).Column(1).ColumnSpan(2).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Client");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).Border(0.0f).AlignRight().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).Border(0.0f).AlignCenter().PaddingLeft(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(taskModel[0].CLIENT);
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignLeft().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(7).Border(0.0f).AlignMiddle().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).AlignRight().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("السادة");
                                    });


                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).AlignLeft().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Client VAT");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(2).Border(0.0f).AlignMiddle().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(taskModel[0].CLNTVAT);
                                    });
                                    table.Cell().Row(i).Column(3).Border(0.0f).AlignRight().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).Border(0.0f).AlignLeft().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("لرقم الضريبي للعميل");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignLeft().PaddingLeft(5).PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(6).Border(0.0f).AlignMiddle().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Ref No.");
                                    });
                                    table.Cell().Row(i).Column(7).Border(0.0f).AlignMiddle().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span(taskModel[0].REF_NO);
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).AlignRight().PaddingBottom(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("رقم البيان");
                                    });

                                    #endregion

                                    #region Invoice Items

                                    #region Item Header
                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("رقم العينة");
                                        text.Span("\n");
                                        text.Span("Number");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.1f).BorderRight(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("رقم البيان");
                                        text.Span("\n");
                                        text.Span("Ref.No");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.1f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("نوع العينة\r\n");
                                        text.Span("\n");
                                        text.Span("Description");
                                    });

                                    table.Cell().Row(i).Column(4).BorderTop(0.1f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("الرمز");
                                        text.Span("\n");
                                        text.Span("Code");
                                    });
                                    table.Cell().Row(i).Column(5).BorderTop(0.1f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("اسم التحليل");
                                        text.Span("\n");
                                        text.Span("Description");
                                    });
                                    table.Cell().Row(i).Column(6).Border(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("السعر");
                                        text.Span("\n");
                                        text.Span("Unit Price");
                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.1f).BorderBottom(0.1f).AlignMiddle().PaddingTop(5).PaddingLeft(10).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("الخصم");
                                        text.Span("\n\t");
                                        text.Span("Discount");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.DefaultTextStyle(x => x.Bold());
                                        text.Span("الاجمالي");
                                        text.Span("\n");
                                        text.Span("D/Price");
                                    });

                                    #endregion

                                    decimal _total = 0; decimal _sumPrice = 0;
                                    for (int item = 0; item < taskModel.Count; item++)
                                    {
                                        i++;
                                        table.Cell().Row(i).Column(1).Border(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(9));
                                            text.Span(taskModel[item].PAT_ID);
                                        });
                                        table.Cell().Row(i).Column(2).Border(0.1f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(9));
                                            text.Span(taskModel[item].REF_NO);
                                        });
                                        table.Cell().Row(i).Column(3).BorderTop(0.1f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(9));
                                            text.Span(taskModel[item].FULL_NAME);
                                        });

                                        table.Cell().Row(i).Column(4).BorderTop(0.1f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(9));
                                            text.Span(taskModel[item].REQ_CODE);
                                        });
                                        table.Cell().Row(i).Column(5).BorderTop(0.1f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(9));
                                            text.Span(taskModel[item].DESCRIPTION);
                                        });
                                        table.Cell().Row(i).Column(6).Border(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(9));
                                            text.Span(taskModel[item].UPRICE);
                                        });
                                        table.Cell().Row(i).Column(7).BorderTop(0.1f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(9));
                                            text.Span(taskModel[item].DSCNT.ToString());
                                        });
                                        table.Cell().Row(i).Column(8).Border(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(9));
                                            text.Span(taskModel[item].DPRICE.ToString());
                                        });

                                        _total = _total + (taskModel[item].DPRICE);
                                        _sumPrice = _sumPrice + Convert.ToDecimal(taskModel[item].UPRICE);
                                    }

                                    #endregion

                                    #region Total

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).BorderLeft(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(3).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(6).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("المجموع");

                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderBottom(0.0f).BorderRight(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Total");
                                    });
                                    table.Cell().Row(i).Column(8).BorderRight(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span(_total.ToString());
                                    });


                                    decimal _discount = (((_sumPrice - _total) / _sumPrice) * 100);
                                    decimal _netValue = _total - (_sumPrice - _total);
                                    decimal _vatValue = ((_netValue * 15) / 100);
                                    decimal _grossValue = ((_netValue) + _vatValue);


                                    if (_discount != 0)
                                    {
                                        i++;
                                        table.Cell().Row(i).Column(1).Border(0.0f).BorderLeft(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(2).ColumnSpan(3).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });

                                        table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(5).Border(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span("الخصم");
                                        });


                                        table.Cell().Row(i).Column(6).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span(_discount.ToString("0.00") + " % ");

                                        });
                                        table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderBottom(0.0f).BorderRight(0.1f).AlignCenter().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span("Discount");
                                        });
                                        table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span((_sumPrice - _total).ToString());
                                        });


                                        i++;
                                        table.Cell().Row(i).Column(1).Border(0.0f).BorderLeft(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(2).ColumnSpan(3).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });

                                        table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                            text.Span("");
                                        });
                                        table.Cell().Row(i).Column(5).ColumnSpan(2).Border(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span("الاجمالي بعد الخصم");
                                        });

                                        table.Cell().Row(i).Column(6).Border(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(8));
                                        });
                                        table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.1f).BorderBottom(0.0f).AlignCenter().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span("Net Value");
                                        });
                                        table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                        {
                                            text.DefaultTextStyle(x => x.FontSize(12));
                                            text.Span(_netValue.ToString());
                                        });
                                    }

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).BorderBottom(0.1f).BorderLeft(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(3).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).ColumnSpan(2).Border(0.0f).BorderBottom(0.1f).AlignRight().AlignMiddle().PaddingLeft(5).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("الضريبة المضافة");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("VAT");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span(_vatValue.ToString());
                                    });

                                    int grossInt = Convert.ToInt32(_grossValue);
                                    var _grossValueIntAr = ArabicWordConverter.ToArabicWord(Convert.ToDecimal(grossInt)) + " ريال ";
                                    var grossDecimal = Convert.ToDecimal((_grossValue.ToString().Substring((_grossValue.ToString().IndexOf(".")))).Replace(".", ""));
                                    var grossDecimalAr = ArabicWordConverter.ToArabicWord(grossDecimal);
                                    if (grossDecimal != 0)
                                        grossDecimalAr = " و " + ArabicWordConverter.ToArabicWord(grossDecimal) + " هللة";
                                    else
                                        grossDecimalAr = ArabicWordConverter.ToArabicWord(grossDecimal).ToString();

                                    grossDecimalAr = grossDecimalAr.ToString().Replace("صفر", "");

                                    i++;
                                    table.Cell().Row(i).Column(1).ColumnSpan(5).Border(0.0f).BorderBottom(0.1f).BorderLeft(0.1f).AlignRight().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span(NumberToWords.ConvertAmount(Convert.ToDouble(_grossValue)));
                                        text.Span("\n");
                                        text.Span(_grossValueIntAr + " " + grossDecimalAr.ToString());
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.1f).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("الاجمالي");

                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().PaddingTop(10).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("Net Total");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.1f).BorderBottom(0.1f).AlignCenter().AlignMiddle().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span(_grossValue.ToString());
                                    });


                                    #endregion

                                    i++;
                                    table.Cell().Row(i).Column(1).ColumnSpan(8).Border(0.0f).BorderBottom(0.0f).BorderLeft(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12));
                                        text.Span("عزيزي العميل: سيتم التخلص من العينة خلال 10 ايام عمل من تاريخ صدور النتيجة وفي حال رغبتكم في استلام العينة نأمل التكرم باستلامها");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).ColumnSpan(2).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");

                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });


                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).BorderBottom(0.0f).BorderLeft(0.0f).AlignCenter().AlignMiddle().PaddingTop(20).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("Signature");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(2).Border(0.0f).BorderBottom(0.0f).AlignLeft().AlignMiddle().PaddingTop(35).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("-------------------------------");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(25).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("التوقيع");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");

                                    });
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });


                                    #region QR Code Generation


                                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(getBase64("Lambda Laboratory", "300010985700003", DateTime.Now.ToString(), _grossValue.ToString(), _vatValue.ToString()), QRCodeGenerator.ECCLevel.Q);
                                    QRCode qrCode = new QRCode(qrCodeData);
                                    Bitmap qrCodeImage = qrCode.GetGraphic(20);
                                    byte[] data;
                                    using (MemoryStream m = new MemoryStream())
                                    {
                                        qrCodeImage.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                                        data = m.ToArray();
                                    }

                                    #endregion

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).BorderBottom(0.0f).BorderLeft(0.0f).AlignCenter().AlignMiddle().PaddingTop(40).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(2).ColumnSpan(2).Border(0.0f).BorderBottom(0.0f).AlignLeft().AlignMiddle().PaddingTop(35).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(3).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(4).BorderTop(0.0f).BorderBottom(0.0f).PaddingTop(25).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });
                                    table.Cell().Row(i).Column(5).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingLeft(5).PaddingTop(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("");
                                    });

                                    table.Cell().Row(i).Column(6).ColumnSpan(3).Border(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5).Width(100).Height(100).Image(data);
                                    table.Cell().Row(i).Column(7).BorderTop(0.0f).BorderRight(0.0f).BorderBottom(0.0f).PaddingTop(5);
                                    table.Cell().Row(i).Column(8).Border(0.0f).BorderRight(0.0f).BorderBottom(0.0f).AlignCenter().AlignMiddle().PaddingTop(5);


                                });


                        page.Footer().DefaultTextStyle(x => x.FontSize(9))
                           .AlignLeft()
                           .BorderTop(0.5f)
                           .ContentFromLeftToRight()
                           .PaddingTop(5)
                           .Table(table =>
                           {
                               table.ColumnsDefinition(columns =>
                               {
                                   columns.ConstantColumn(30, Unit.Millimetre);
                                   columns.ConstantColumn(40, Unit.Millimetre);
                                   columns.ConstantColumn(5, Unit.Millimetre);
                                   columns.ConstantColumn(5, Unit.Millimetre);
                                   columns.ConstantColumn(20, Unit.Millimetre);
                                   columns.ConstantColumn(15, Unit.Millimetre);
                                   columns.ConstantColumn(15, Unit.Millimetre);
                                   columns.ConstantColumn(20, Unit.Millimetre);
                                   columns.ConstantColumn(40, Unit.Millimetre);
                               });

                               table.Cell().Row(1).Column(1).PaddingLeft(20).AlignRight().Text("Email:");
                               table.Cell().Row(1).Column(2).ColumnSpan(2).AlignLeft().Text("support@deltacare.com");

                               table.Cell().Row(1).Column(5).AlignRight().Text("Pin Code:");
                               table.Cell().Row(1).Column(6).AlignLeft().Text("51265");

                               table.Cell().Row(1).Column(7).AlignRight().Text("Phone:");
                               table.Cell().Row(1).Column(8).ColumnSpan(2).AlignLeft().Text("+966 11 3005415");

                               table.Cell().Row(1).Column(9).AlignRight().Text(x =>
                               {
                                   x.Span("Page ");
                                   x.CurrentPageNumber();
                                   x.Span(" Of ");
                                   x.TotalPages();
                               });

                           });
                    });

                });

                return document.GeneratePdf();
            }

            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<IEnumerable<EVMultiInvoicePrintRModel>> GetEVMultiInvoice(EvListMultiInvoicePrintModel evPrintModel)
        {
            EVOrderModel evOrderModel = new EVOrderModel();
            evOrderModel.QueryType = (int)QueryTypeEnum.Update;
            evOrderModel.CN = evPrintModel.ClientNo;
            evOrderModel.SEARCH = (evPrintModel.Search.EndsWith(",") == true ? evPrintModel.Search.Remove(evPrintModel.Search.Length - 1) : evPrintModel.Search);
            evOrderModel.EXPR_DATE = (evPrintModel.SinceDate == String.Empty ? DateTime.Now.AddMonths(-2).ToString("MM/dd/yyyy") : evPrintModel.SinceDate);
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVOrderModel>(evOrderModel, "QueryType");
            var _invoice = await _dataRepository.ExecuteQueryAsync<EVMultiInvoicePrintRModel>(SPConstant.SP_ManageEnvironmentalOrders, parameterCollection);
            
            ///Multi Invoice commented
            #region QR Code Generation

            //double total = 0;
            //if (_invoice != null)
            //    total = Convert.ToDouble(_invoice.Sum(item => Convert.ToDecimal(item.UPRICE)));

            //QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(getBase64("Lambda Laboratory", "300010985700003", DateTime.Now.ToString(), (total + (total * 0.15)).ToString(), (total * 0.15).ToString()), QRCodeGenerator.ECCLevel.Q);
            //QRCode qrCode = new QRCode(qrCodeData);
            //Bitmap qrCodeImage = qrCode.GetGraphic(20);
            //byte[] data;
            //using (MemoryStream m = new MemoryStream())
            //{
            //    qrCodeImage.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            //    data = m.ToArray();
            //}


            //APImageModel _APImageModel = new APImageModel();
            //_APImageModel.ACCN = evOrderModel.CN.ToString();
            //_APImageModel.TCODE = evOrderModel.EXPR_DATE.ToString();
            //_APImageModel.IMAGE = Convert.ToBase64String(data);

            //InsertQRImage(_APImageModel);

            #endregion

            return _invoice;

        }

        public int InsertQRImage(APImageModel _APImageModel)//Update_ATR
        {

            BadRequestResult badRequest = new BadRequestResult();

            if (_APImageModel == null)
                return badRequest.StatusCode;
            if (string.IsNullOrEmpty(_APImageModel.ACCN)) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Insert;
            _APImageModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<APImageModel>(_APImageModel, "QueryType"); ;
            IEnumerable<APImageModel> result = _dataRepository.ExecuteQuery<APImageModel>(SPConstant.SP_ManageAPImages, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }


        public async Task<IEnumerable<SignUserModel>> GetSignUser()
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<SignUserModel>(SPConstant.SP_ManageEnvironmentalOrdersATR, parameters)).ToList();
        }

        public async Task<IEnumerable<SignUserModel>> GetEVOTP()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<SignUserModel>(SPConstant.SP_ManageEVOrdersDetails, parameters)).ToList();
        }

        #endregion

        #region Cytogenetics Orders

        public async Task<IEnumerable<CGOrderModel>> GetAllCytogeneticOrder()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            var v = (await _dataRepository.ExecuteQueryAsync<CGOrderModel>(SPConstant.SP_ManageCytogeneticOrders, parameterCollection)).ToList();
            return v;
        }

        public async Task<IEnumerable<CGOrderATRModel>> GetForCytogeneticOrderATRData(CGOrderATRModel evOrder)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(evOrder.ORD_NO, queryType, "@ORD_NO", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<CGOrderATRModel>(SPConstant.SP_ManageCytogeneticOrdersATR, parameters)).ToList();
        }

        //public async Task<IEnumerable<AccnPrefixModel>> GetAllAccessionPrefixes()//ACCNPRFX
        //{
        //    int queryType = (int)QueryTypeEnum.GetAll;
        //    IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
        //    return (await _dataRepository.ExecuteQueryAsync<AccnPrefixModel>(SPConstant.SP_GenerateAccnPrefix, parameterCollection)).ToList();
        //}

        public async Task<string> InsertCGOrder(CGOrderModel cgOrderModel)
        {
            if (cgOrderModel.ORD_NO != string.Empty)
                DeleteCGOrderByOrderNo(cgOrderModel);

            if (cgOrderModel.RCVD_DATE != string.Empty)
                cgOrderModel.RCVD_DATE = cgOrderModel.RCVD_DATE.ToString().Replace("T", " ");

            int queryType = (int)QueryTypeEnum.Insert;
            cgOrderModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CGOrderModel>(cgOrderModel, "QueryType");
            var result = (await _dataRepository.ExecuteQueryAsync<CGOrderModel>(SPConstant.SP_ManageCytogeneticOrders, parameterCollection)).ToList();
            if (result != null && result.Any())
            {
                return Convert.ToString(Convert.ToInt32(result[0].PAT_ID));
            }
            else
            {
                return string.Empty;
            }
        }



        public async Task<int> InsertCGATR(List<CGOrderATRModel> ATRModel)
        {
            int rowsAffected = 0;
            try
            {
                List<CGOrderATRInsertModel> cgListInsertATRModel = new List<CGOrderATRInsertModel>();

                foreach (var sen in ATRModel)
                {
                    var cgInsertATRModel = new CGOrderATRInsertModel();
                    cgInsertATRModel.SITE_NO = sen.SITE_NO;
                    cgInsertATRModel.ORD_NO = sen.ORD_NO;
                    cgInsertATRModel.PAT_ID = sen.PAT_ID;
                    cgInsertATRModel.ACCN = sen.ACCN;

                    cgInsertATRModel.REQ_CODE = sen.REQ_CODE;
                    cgInsertATRModel.REQ_DTTM = DateTime.Now.ToString("MM/dd/yyyy");
                    cgInsertATRModel.DRAWN_DTTM = DateTime.Now.ToString("MM/dd/yyyy");
                    cgInsertATRModel.FULL_NAME = sen.FULL_NAME;
                    cgInsertATRModel.TEST_ID = sen.TEST_ID;
                    cgInsertATRModel.S_TYPE = sen.S_TYPE;
                    cgInsertATRModel.R_STS = sen.R_STS;
                    cgInsertATRModel.MDL = sen.MDL;
                    cgInsertATRModel.DIV = sen.DIV;
                    cgInsertATRModel.SECT = sen.SECT;
                    cgInsertATRModel.PRTY = sen.PRTY;
                    cgInsertATRModel.TS = sen.TS;
                    cgInsertATRModel.UPRICE = Convert.ToDecimal(sen.UPRICE);
                    cgInsertATRModel.DT = sen.DT;
                    cgInsertATRModel.DSCNT = sen.DSCNT;
                    cgInsertATRModel.DPRICE = sen.DPRICE;
                    cgInsertATRModel.O_ID = sen.O_ID;
                    //evInsertATRModel.UPDT_TIME =sen.UPDT_TIME;
                    cgListInsertATRModel.Add(cgInsertATRModel);
                }

                DataTable dtCGInstertATR = CommonHelper.ToDataTable(cgListInsertATRModel);
                if (dtCGInstertATR.Columns.Contains("QueryType"))
                    dtCGInstertATR.Columns.Remove("QueryType");
                rowsAffected = await _dataRepository.ExecuteDataTable(SPConstant.Sp_InsertCytogeneticOrderATR, dtCGInstertATR, SPConstant.type_InsertCytogeneticOrderATR);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }


        private int DeleteCGOrderByOrderNo(CGOrderModel cgOrderModel)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            cgOrderModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CGOrderModel>(cgOrderModel, "QueryType");
            IEnumerable<CGOrderModel> result = _dataRepository.ExecuteQuery<CGOrderModel>(SPConstant.SP_ManageCytogeneticOrders, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public async Task<IEnumerable<TDModel>> GetAllCGTD(string TCode)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(TCode, queryType, "@SEARCH", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<TDModel>(SPConstant.SP_ManageCytogeneticOrders, parameters)).ToList();
        }


        #endregion
    }
}
