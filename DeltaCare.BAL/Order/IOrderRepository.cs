using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace DeltaCare.BAL
{
    public interface IOrderRepository
    {
        #region Get Order Transaction
        Task<IEnumerable<SiteModel>> GetRefSitefromSiteDetails(string SITE_NO);//ACCNPRFX
        Task<IEnumerable<AccnPrefixModel>> GetAllAccessionPrefixes();//ACCNPRFX
        Task<int> UpdateAccessionPrefixesByOne(string prfx, AccnPrefixModel AccnPrefixModel);//UpdateACCNPRFX
        Task<string> GenerateAccessionNumber(string siteNo, string prfx);
        Task<IEnumerable<ORD_TRNSModel>> GetLastOrdersTransactions();//GET_NEW_ORD_NO
        Task<IEnumerable<ORD_TRNSModel>> GetAllOrdersTransactions();///Get All Orders Transactions
        Task<IEnumerable<ORD_TRNSModel>> GetOrdersTransactionsByParams(string PAT_ID, ORD_TRNSModel oRD_TRNSModel);// string ORD_NO);//GET_ORD_TRNS
        Task<IEnumerable<v_ORD_TRANSModel>> GetOrdersTransactionsDetailsByParams(string PAT_ID, string ORD_NO);//GET_v_ORD_TRANS
        Task<IEnumerable<ATRModel>> GetActiveTestsRequestByParams(string PAT_ID, string ORD_NO);//GET_ATR
        Task<IEnumerable<ORD_DTLModel>> GetOrdersDetailsByParams(string PAT_ID, string ORD_NO);//GET_Ord_Dtl
        Task<IEnumerable<p_ORD_DTL_TD_GTModel>> GetOrdersDetailsByUnion(string PAT_ID, string ORD_NO);// GET_p_ORD_DTL_TD_GT
        Task<IEnumerable<ORD_TPModel>> GetAllOrderType();//GET_Ord_TP

        Task<IEnumerable<ORD_TRCModel>> GetOrderTrackingByOrdNo(string ORD_NO);
        Task<IEnumerable<ORD_TRCModel>> GetOrderTrackingByReqCode(string REQ_CODE);
        #endregion

        #region Order Entry Transactions
        Task<int> InsertOrdersTransactions(ORD_TRNSModel oRD_TRNSModel);//ADD_ORD_TRNS
        Task<int> InsertActiveTestsRequest(Object[] ATRs, string SEX, string CN, DateTime DOB, string DRNO, string LOC, string PRFX, string CLN_IND, string ORD_NO);//ADD_ATR
        Task<int> InsertOrdersDetails(ORD_DTLModel oRD_DTLModel);//ADD_Ord_Dtl
        Task<int> InsertOrdersTracking(ORD_TRCModel oRD_TRCModel);//ADD_Ord_Trc
        Task<int> InsertActiveResultsFile(ARFModel aRFModel);//ADD_ARF

        Task<int> InsertFreeTextResults(FreeTextResultsModel freeTextResultsModel);
        Task<int> InsertCytogeneticsQualityControl(CytogeneticsQCModel cytogeneticsQCModel);
        Task<int> InsertAnatomicPathologyCases(AnatomicPathologyCasesModel  anatomicPathologyCasesModel);
        #endregion

        #region Update Order Transaction
        Task<int> UpdateOrdersTransactions(ORD_TRNSModel oRD_TRNSModel);
        Task<int> UpdateOrdersTransactionsDetails(ORD_TRNSModel oRD_TRNSModel);
        Task<int> UpdateActiveTestsRequest(ATRModel aTRModel);
        Task<int> UpdateOrdersDetails(ORD_DTLModel oRD_DTLModel);
        Task<int> UpdateActiveResultsFile(ARFModel aRFModel);

        Task<int> UpdateCytogeneticsCases(FreeTextResultsModel freeTextResultsModel);
        Task<int> UpdateCytogeneticsQualityControl(CytogeneticsQCModel cytogeneticsQCModel);
        Task<int> UpdateAnatomicPathologyCases(AnatomicPathologyCasesModel anatomicPathologyCasesModel);

        Task<int> CancelActiveTestRequest(int ATRID, string R_STS, string CNLD, string Notes, string SITE_NO, string U_ID, string ORD_NO, string SECT, string REQ_CODE);
        Task<int> CollectedActiveTestRequest(Object[] ATRs);//, int ATR_ID, string STS, DateTime DRAWN_DTTM, string ACCN, string REQ_CODE, string ORD_NO, string SECT, string siteNo);

        Task<int> AddNotesActiveTestsRequest(ATRModel aTRModel);

        Task<int> UpdateIssueInvoince(ORD_TRNSModel oRD_TRNSModel);

        #endregion

        #region Multiple Search
        Task<IEnumerable<MultipleSearchOrderModel>> GetMultipleSearch(string IDNT, string TEL, string PAT_ID, string PAT_NAME, DateTime? DOB, string SEX, string ORD_NO, string ACCN, string MDL, DateTime? FromDate, DateTime? ToDate);
        Task<IEnumerable<MultipleSearchOrderModel>> GetMultipleSearchOrders(string PAT_ID);
        #endregion

        #region Environmental Orders

        Task<IEnumerable<EVOrderModel>> GetAllEnvironmentalOrder(string pSize);
        Task<IEnumerable<EVOrderListModel>> GetEnvironmentalOrderList(int pendingDays, bool isPending);
        Task<IEnumerable<EVDetailModel>> GetEVOrderPatientDetails(EvResultDetailModel evResultModel);
        Task<IEnumerable<EVOrderATRModel>> GetForEnvironmentalOrderATRData(EVOrderATRModel evOrder);
        Task<IEnumerable<EVClientModel>> GetAllClients();
        Task<IEnumerable<EVSampleModel>> GetAllEVSample();
        Task<IEnumerable<EVPatientModel>> GetPatientNextId();
        Task<string> InsertEVOrder(EVOrderModel evOrderModel);
        Task<IEnumerable<TDModel>> GetAllEVTD(string TCode);
        Task<int> InsertEVATR(List<EVOrderATRModel> ATRModel);
        Task<IEnumerable<EVSearch>> GetAllEVPatientSearch(string OrderNo);
        string GetEVPrint(string OrderNo);
        Task<IEnumerable<EVPatientModel>> UpdateInvoiceNo(string OrderNo);
        string GetEVMultiInvoicePrint(EvListMultiInvoicePrintModel evPrintModel);
        Task<IEnumerable<EVMultiInvoicePrintRModel>> GetEVMultiInvoice(EvListMultiInvoicePrintModel evPrintModel);
        Task<IEnumerable<SignUserModel>> GetSignUser();

        Task<IEnumerable<SignUserModel>> GetEVOTP();

        #endregion

        #region Cytogenetic Order
        Task<IEnumerable<CGOrderModel>> GetAllCytogeneticOrder();
        Task<IEnumerable<CGOrderATRModel>> GetForCytogeneticOrderATRData(CGOrderATRModel evOrder);
        Task<string> InsertCGOrder(CGOrderModel cgOrderModel);
        Task<int> InsertCGATR(List<CGOrderATRModel> ATRModel);
        Task<IEnumerable<TDModel>> GetAllCGTD(string TCode);

        #endregion
    }
}
