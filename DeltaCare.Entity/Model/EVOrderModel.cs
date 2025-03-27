using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class EVOrderModel : RequestMode
    {
        public int SNO { get; set; }
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string S_TYPE { get; set; }
        public string CN { get; set; }
        public bool CASH { get; set; }
        public string CUS_NAME { get; set; }
        public string CUS_TP { get; set; }
        public string ISSU_DATE { get; set; }
        public string REF_NO { get; set; }
        public string BATCHA_NO { get; set; }
        public string TEL { get; set; }
        public string VOL_WT { get; set; }
        public string VOL_WT2 { get; set; }
        public string EMAIL { get; set; }
        public string CPERSON { get; set; }
        public string CLIENT { get; set; }
        public string RCVD_DATE { get; set; }
        public string PHYS_COND { get; set; }
        public string ORIGIN { get; set; }
        public string CITY_CNTRY { get; set; }
        public string ADDRESS { get; set; }
        public string CUSTOM_SR { get; set; }
        public string PRD_DATE { get; set; }
        public string EXPR_DATE { get; set; }
        public string AST_DATE { get; set; }
        public string ORD_NO { get; set; }
        public string CNCLIENT { get; set; }
        public string SP_DESCRP { get; set; }
        public string STYPESP_DESCRP { get; set; }
        public string SEARCH { get; set; }
        public string PAYTP { get; set; }
        public string PAID { get; set; }
        public string RMNG { get; set; }
        public string DSCAMNT { get; set; }
        public string TOTDSCNT { get; set; }
        public string TOT_VALUE { get; set; }
        public string NET_VALUE { get; set; }
        public string VAT { get; set; }
        public string GRAND_VAL { get; set; }
        public string VC_NO { get; set; }
        public string INV_DATE { get; set; }
        public string ATR_ID { get; set; }
        public string BRAND { get; set; }
        public string EXPIRY_PERIOD { get; set; }
        public string EXTRADISCOUNT { get; set; }
        public string OTP { get; set; }
    }

    public class EVOrderATRModel : RequestMode
    {
        public int ATR_ID { get; set; }
        public int SNO { get; set; }
        public string SITE_NO { get; set; }
        public string ORD_NO { get; set; }
        public string PAT_ID { get; set; }
        public string ACCN { get; set; }
        public string REQ_CODE { get; set; }
        public string REQ_DTTM { get; set; }
        public string DRAWN_DTTM { get; set; }
        public string FULL_NAME { get; set; }
        public string TEST_ID { get; set; }
        public string B_NO { get; set; }
        public string CT { get; set; }
        public string RSLD { get; set; }
        public string VLDT { get; set; }
        public string S_TYPE { get; set; }
        public string SP_SITE { get; set; }
        public string NO_SLD { get; set; }
        public string STS { get; set; }
        public string PRTY { get; set; }
        public string R_STS { get; set; }
        public string MDL { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string O_ID { get; set; }
        public string X_ID { get; set; }
        public string RUN_NO { get; set; }
        public string CNLD { get; set; }
        public string SF { get; set; }
        public string LF { get; set; }
        public string RNO { get; set; }
        public string RPT_NO { get; set; }
        public string NOTES { get; set; }
        public string LN { get; set; }
        public string UPRICE { get; set; }
        public string DSCNTG { get; set; }
        public string DT { get; set; }
        public Decimal DSCNT { get; set; }
        public Decimal DPRICE { get; set; }
        public string BPRICE { get; set; }
        public string KP { get; set; }
        public string ATRID { get; set; }
        public string PRID { get; set; }
        public string SEQ { get; set; }
        public string MDF { get; set; }
        public string LAST_UPDT { get; set; }
        public string UPDT_TIME { get; set; }
        public string VC_NO { get; set; }
        public string INV_DATE { get; set; }
    }


    public class EVOrderATRInsertModel : RequestMode
    {
        //public int ATR_ID { get; set; }
        public string SITE_NO { get; set; }
        public string ORD_NO { get; set; }
        public string PAT_ID { get; set; }
        public string ACCN { get; set; }
        public string REQ_CODE { get; set; }
        public string TEST_ID { get; set; }
        public string FULL_NAME { get; set; }
        public string REQ_DTTM { get; set; }
        public string DRAWN_DTTM { get; set; }
        public string DT { get; set; }
        public string S_TYPE { get; set; }
        public string MDL { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string PRTY { get; set; }
        public string TS { get; set; }
        public decimal DSCNT { get; set; }
        public decimal DPRICE { get; set; }
        public string R_STS { get; set; }
        public string O_ID { get; set; }
        public decimal UPRICE { get; set; }
        //public string UPDT_TIME { get; set; }
    }

    public class EVSampleModel : RequestMode
    {
        public int ID { get; set; }
        public string S_TYPE { get; set; }
        public string SP_DESCRP { get; set; }
        public string SP_DESCRIP { get; set; }
    }

    public class EVPatientModel : RequestMode
    {
        public string VC_NO { get; set; }
        public string PAT_ID { get; set; }
    }

    public class EVClientModel : RequestMode
    {
        public string CN { get; set; }
        public string CLIENT { get; set; }
        public string CNCLIENT { get; set; }
        public string CLNT_ADDRESS { get; set; }
    }

    public class EVSearch : RequestMode
    {
        public string id { get; set; }

        public string name { get; set; }
    }

    public class EVPrintRModel : RequestMode
    {

        public int SNO { get; set; }
        public int ATR_ID { get; set; }
        public string SITE_NO { get; set; }
        public string ORD_NO { get; set; }
        public string PAT_ID { get; set; }
        public string ACCN { get; set; }
        public string REQ_CODE { get; set; }
        public string REQ_DTTM { get; set; }
        public string DRAWN_DTTM { get; set; }
        public string FULL_NAME { get; set; }
        public string TEST_ID { get; set; }
        public string B_NO { get; set; }
        public string CT { get; set; }
        public string RSLD { get; set; }
        public string VLDT { get; set; }
        public string S_TYPE { get; set; }
        public string SP_SITE { get; set; }
        public string NO_SLD { get; set; }
        public string STS { get; set; }
        public string PRTY { get; set; }
        public string R_STS { get; set; }
        public string MDL { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string O_ID { get; set; }
        public string X_ID { get; set; }
        public string RUN_NO { get; set; }
        public string CNLD { get; set; }
        public string SF { get; set; }
        public string LF { get; set; }
        public string RNO { get; set; }
        public string RPT_NO { get; set; }
        public string NOTES { get; set; }
        public string LN { get; set; }
        public string UPRICE { get; set; }
        public string DSCNTG { get; set; }
        public string DT { get; set; }
        public Decimal DSCNT { get; set; }
        public Decimal DPRICE { get; set; }
        public string BPRICE { get; set; }
        public string KP { get; set; }
        public string ATRID { get; set; }
        public string PRID { get; set; }
        public string SEQ { get; set; }
        public string MDF { get; set; }
        public string LAST_UPDT { get; set; }
        public string UPDT_TIME { get; set; }
        public string PAT_NAME { get; set; }
        public string CLIENT { get; set; }
        public string CLNTVAT { get; set; }
        public string INS_NO { get; set; }
        public string REF_NO { get; set; }

        public string VC_NO { get; set; }
        public string INV_DATE { get; set; }

    }


    public class EvListPrintModel : RequestMode
    {
        public string OrderNo { get; set; }

    }

    public class EvListMultiInvoicePrintModel : RequestMode
    {
        public string ClientNo { get; set; }
        public string SinceDate { get; set; }
        public string Search { get; set; }
    }

    public class EVMultiInvoicePrintRModel : RequestMode
    {

        public int SNO { get; set; }
        public bool CHECKED { get; set; }
        public string PAT_ID { get; set; }
        public string REF_NO { get; set; }
        public string FULL_NAME { get; set; }
        public string REQ_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UPRICE { get; set; }
        public string DSCNT { get; set; }
        public Decimal DPRICE { get; set; }
        public string VC_NO { get; set; }
        public string INV_DATE { get; set; }
        public string CLIENT { get; set; }
        public string CLNTVAT { get; set; }
    }

    public class SignUserModel : RequestMode
    {
        public string ID { get; set; }

        public string NAME { get; set; }
    }

    public class EVOrderListModel : RequestMode
    {
        public string SAMPLE_ID { get; set; }

        public string SAMPLE_NAME { get; set; }
        public string CN { set; get; }
        public string CLIENT { set; get; }
        public string REQ_CODE { set; get; }
        public string TCODE { set; get; }
        public string ACCN { get; set; }
        public string ORD_NO { get; set; }
        public string FULL_NAME { get; set; }
        public string ORDER_DTM { get; set; }
        public string REF_DATE { get; set; }
        public string O_ID { get; set; }
        public string ORDERED_BY { get; set; }
        public int PENDING { set; get; }
    }
    public class EVOrderPatientModel : RequestMode
    {
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string ACCN { get; set; }
        public string SEX { get; set; }

        public string DOB { get; set; }

        public string TEST_ID { get; set; }

        public string CN { get; set; }

        public string CLIENT { get; set; }

        public string REF_NO { get; set; }

        public string LOC { get; set; }

    }

    public class EVOTPModel : RequestMode
    {
        public int OTP { get; set; }
        public string DESCRIP { get; set; }
    }

    public class EVOrderDetailsModel : RequestMode
    {
        public int PendingDays { get; set; }
        public bool IsPending { get; set; }
    }
}
