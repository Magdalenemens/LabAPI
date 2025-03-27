using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class ORD_TRNSModel : RequestMode
    {
        [Key]
        public int ORD_TRNS_ID { get; set; }
        public string SITE_NO { get; set; }
        public string ORD_NO { get; set; }
        public string PAT_ID { get; set; }
        public string CN { get; set; }
        public string REF_NO { get; set; }
        public string LOC { get; set; }
        public string PT { get; set; }
        public string DRNO { get; set; }
        public string REQ_NO { get; set; }
        
        public string PHID { get; set; }
        public string NOTES { get; set; }
        public string OTP { get; set; }
        public byte RSVRD { get; set; }
        public DateTime? RSVRD_DTTM { get; set; }
        public string CASH { get; set; } = "";
        public string SMC { get; set; }
        public string VC_NO { get; set; }
        public string INS_NO { get; set; }
        public DateTime? INV_DATE { get; set; }
        public DateTime? INV_DTTM { get; set; }
        public decimal? TOTDSCNT { get; set; }
        public decimal? TOT_VALUE { get; set; }
        public decimal? DSCAMNT { get; set; }
        public decimal? NET_VALUE { get; set; }
        public decimal? PAID { get; set; }
        public decimal? RMNG { get; set; }
        public decimal? VAT { get; set; }
        public decimal? GRAND_VAL { get; set; }
        public decimal? EXTRDSCT { get; set; }
        public string PAYTP { get; set; }
        public string S_TYPE { get; set; }
        public string CLN_IND { get; set; }
        public string COMMENTS { get; set; }
    }
    public class v_ORD_TRANSModel : RequestMode
    {
        [Key]
        public string ORD_NO { get; set; }
        ////public int ORD_TRNS_ID { get; set; }
        public string SITE_NO { get; set; }
        public string PAT_ID { get; set; }
        public string CN { get; set; }
        public string CLIENT { get; set; }
        public string REF_NO { get; set; }
        public string LOC { get; set; }
        public string DESCRP { get; set; }
        public string PT { get; set; }
        public string DRNO { get; set; }
        public string DOCTOR { get; set; }
        public string REQ_NO { get; set; }
        public string PHID { get; set; }
        public string NOTES { get; set; }
        public string OTP { get; set; }
        public byte RSVRD { get; set; }
        public DateTime? RSVRD_DTTM { get; set; }
        public string CASH { get; set; }
        public string SMC { get; set; }
        public string VC_NO { get; set; }
        public string INS_NO { get; set; }
        public DateTime? INV_DATE { get; set; }
        public DateTime? INV_DTTM { get; set; }
        public decimal TOTDSCNT { get; set; }
        public decimal TOT_VALUE { get; set; }
        public decimal DSCAMNT { get; set; }
        public decimal NET_VALUE { get; set; }
        public decimal PAID { get; set; }
        public decimal RMNG { get; set; }
        public decimal VAT { get; set; }
        public decimal GRAND_VAL { get; set; }
        public decimal EXTRDSCT { get; set; }
        public string PAYTP { get; set; }
        public string S_TYPE { get; set; }
        public string CLN_IND { get; set; }
        public string COMMENTS { get; set; }
        //public int PR_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string TEL { get; set; }
        public string MOBILE { get; set; }
        public string FAXNO { get; set; }
        public DateTime? DOB { get; set; }
        public string SEX { get; set; }
        public string SAUDI { get; set; }
        public string NATIONALITY { get; set; }
        public string IDNT { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public int PRID { get; set; }

        public string OTP_DESC { get; set; }
        public string SCM_DESC { get; set; }

    }
    public class ATRModel : RequestMode
    {
        [Key]
        public int ATR_ID { get; set; }
        public string SITE_NO { get; set; }
        public string ORD_NO { get; set; }
        public string PAT_ID { get; set; }
        public string ACCN { get; set; }
        public string REQ_CODE { get; set; }
        public DateTime? REQ_DTTM { get; set; }
        public DateTime? DRAWN_DTTM { get; set; }
        public string FULL_NAME { get; set; }
        public string TEST_ID { get; set; }
        
        //public string DTNO { get; set; }
        //public string GTNO { get; set; }
        public string B_NO { get; set; }
        public string CT { get; set; }
        public string RSLD { get; set; }
        public string VLDT { get; set; }
        public string S_TYPE { get; set; }
        public string SP_SITE { get; set; }
        public Decimal? NO_SLD { get; set; }
        public string STS { get; set; }
        public string PRTY { get; set; }
        public string R_STS { get; set; }
        public string MDL { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string TS { get; set; }
        public string O_ID { get; set; }
        public string X_ID { get; set; }
        public string RUN_NO { get; set; }
        public string CNLD { get; set; }
        public string SF { get; set; }
        public bool? LF { get; set; }
        public string RNO { get; set; }
        public string RPT_NO { get; set; }
        public string NOTES { get; set; }
        public string LN { get; set; }
        public decimal? UPRICE { get; set; }
        public string DSCNTG { get; set; }
        public string DT { get; set; }
        public decimal? DSCNT { get; set; }
        public decimal? DPRICE { get; set; }
        public decimal? BPRICE { get; set; }
        public string KP { get; set; }
        public int? ATRID { get; set; }
        public int? PRID { get; set; }
        public string SEQ { get; set; }
        public string MDF { get; set; }
        public DateTime? LAST_UPDT { get; set; }
        public decimal? UPDT_TIME { get; set; }
        //public string PRFX { get; set; }
        //public DateTime? DOB { get; set; }
        //public string SEX { get; set; }
        //public string CN { get; set; }
        //public string LOC { get; set; }
        //public string DRNO { get; set; }
    }
    public class ORD_DTLModel : RequestMode
    {
        [Key]
        public int ORD_DTL_ID { get; set; }
        public string ORD_SEQ { get; set; }
        public string SITE_NO { get; set; }
        public string PAT_ID { get; set; }
        public string ORD_NO { get; set; }
        public string REF_SITE { get; set; }
        public string ACCN { get; set; }
        public string ORD_CODE { get; set; }
        public string REQ_CODE { get; set; }
        public string TEST_ID { get; set; }
        //public string DTNO { get; set; }
        //public string GTNO { get; set; }
        public string CT { get; set; }
        public DateTime? REQ_DTTM { get; set; }
        public string O_ID { get; set; }
        public DateTime? DRAWN_DTTM { get; set; }
        public DateTime? TRNS_DTTM { get; set; }
        public string TRNS_ID { get; set; }
        public DateTime? RCVD_DTTM { get; set; }
        public string RCVD_ID { get; set; }
        public DateTime? PRCVD_DTTM { get; set; }
        public string PRCVD_ID { get; set; }
        public DateTime? VER_DTTM { get; set; }
        public string RSLD { get; set; }
        public string VLDT { get; set; }
        public string PRTY { get; set; }
        public string STS { get; set; }
        public string STSDSP { get; set; }
        public string R_STS { get; set; }
        public int? TAT { get; set; }
        public string MDL { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string X_ID { get; set; }
        public string CNLD { get; set; }
        public int ATRID { get; set; }

        public string FULL_NAME { get; set; }
        public string DESCRIP { get; set; }

    }
    public class ARFModel : RequestMode
    {
        [Key]
        public int RNO { get; set; }
        public int ARF_ID { get; set; }
        public string SITE_NO { get; set; }
        public string PAT_ID { get; set; }
        public string REF_SITE { get; set; }
        public string ORD_NO { get; set; }
        public string ACCN { get; set; }
        public string CN { get; set; }
        public DateTime? DOB { get; set; }
        public string SEX { get; set; }
        public string LOC { get; set; }
        public string DRNO { get; set; }
        public DateTime? REQ_DTTM { get; set; }
        public DateTime? DRAWN_DTTM { get; set; }
        public string REQ_CODE { get; set; }
        public string TCODE { get; set; }
        public string GTNO { get; set; }
        public string DTNO { get; set; }
        public string SREQ_CODE { get; set; }
        public string S_TYPE { get; set; }
        public string SP_SITE { get; set; }
        public string PTN { get; set; }
        public string B_NO { get; set; }
        public string CT { get; set; }
        public string RESULT { get; set; }
        public string ORG_RES { get; set; }
        public string UNITS { get; set; }
        public string F { get; set; }
        public string LRESULT { get; set; }
        public DateTime? LREQ_DTTM { get; set; }
        public string PNDG { get; set; }
        public decimal? TAT { get; set; }
        public string STS { get; set; }
        public string PRTY { get; set; }
        public string MDL { get; set; }
        public string RSTP { get; set; }
        public DateTime? RES_DTTM { get; set; }
        public DateTime? VER_DTTM { get; set; }
        public DateTime? RSLD_DTTM { get; set; }
        public DateTime? VLDT_DTTM { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string TST_ID { get; set; }
        public string SEQ { get; set; }
        public string RPT_NO { get; set; }
        public decimal? DEC { get; set; }
        public decimal? REF_LOW { get; set; }
        public decimal? REF_HIGH { get; set; }
        public decimal? CRTCL_LOW { get; set; }
        public decimal? CRTCL_HIGH { get; set; }
        public string LHF { get; set; }
        public string AF { get; set; }
        public string REF_RANGE { get; set; }
        public string REF_LC { get; set; }
        public string REF_HC { get; set; }
        public string TNO { get; set; }
        public string MHN { get; set; }
        public string SHN { get; set; }
        public decimal NO_SLD { get; set; }
        public string O_ID { get; set; }
        public string R_ID { get; set; }
        public string V_ID { get; set; }
        public string RSLD { get; set; }
        public string RSLD_ID { get; set; }
        public string VLDT { get; set; }
        public string VLDT_ID { get; set; }
        public string R_STS { get; set; }
        public string BILL { get; set; }
        public decimal? UPRICE { get; set; }
        public string NOTES { get; set; }
        public string NOTESB { get; set; }
        public string INTERP { get; set; }
        public string FN { get; set; }
        public string S { get; set; }
        public int ARFID { get; set; }
        public int ATRID { get; set; }
        public int PRID { get; set; }
        public string VER { get; set; }
        public string P { get; set; }
        public decimal? LN { get; set; }
        public decimal? CNT { get; set; }
        public string PF { get; set; }
        public string PR { get; set; }
        public string WS { get; set; }
        public DateTime? LAST_UPDT { get; set; }
        public decimal? UPDT_TIME { get; set; }
        public string FULL_NAME { get; set; }
    }
    public class ORD_STSModel : RequestMode
    {
        [Key]
        public int STS_ID { get; set; }
        public string STS { get; set; }
        public string STSDSP { get; set; }
        public string DESCRIP { get; set; }
    }
    public class ORD_TPModel : RequestMode
    {
        [Key]
        public int ORD_TP_ID { get; set; }
        public string OTP { get; set; }
        public string DESCRIP { get; set; }
    }
    public class ORD_TRCModel : RequestMode
    {
        [Key]
        public int ORD_TRC_ID { get; set; }
        public string SITE_NO { get; set; }
        public string SITE_NAME { get; set; }
        public string ORD_NO { get; set; }
        public string STS { get; set; }
        public string SECT { get; set; }
        public string REQ_CODE { get; set; }
        public DateTime? ACT_DTTM { get; set; }
        public string U_ID { get; set; }
        public string USER_NAME { get; set; }
    }
    public class p_ORD_DTL_TD_GTModel : RequestMode
    {
        [Key]
        public int ORD_DTL_ID { get; set; }
        public string ORD_SEQ { get; set; }
        public string SITE_NO { get; set; }
        public string PAT_ID { get; set; }
        public string ORD_NO { get; set; }
        public string REF_SITE { get; set; }
        public string ACCN { get; set; }
        public string ORD_CODE { get; set; }
        public string REQ_CODE { get; set; }
        public string TEST_ID { get; set; }
        //public string DTNO { get; set; }
        //public string GTNO { get; set; }
        public string CT { get; set; }
        public DateTime? REQ_DTTM { get; set; }
        public string O_ID { get; set; }
        public DateTime? DRAWN_DTTM { get; set; }
        public DateTime? TRNS_DTTM { get; set; }
        public string TRNS_ID { get; set; }
        public DateTime? RCVD_DTTM { get; set; }
        public string RCVD_ID { get; set; }
        public DateTime? PRCVD_DTTM { get; set; }
        public string PRCVD_ID { get; set; }
        public DateTime? VER_DTTM { get; set; }
        public string RSLD { get; set; }
        public string VLDT { get; set; }
        public string PRTY { get; set; }
        public string STS { get; set; }
        public string STSDSP { get; set; }
        public string R_STS { get; set; }
        public string MDL { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string X_ID { get; set; }
        public string CNLD { get; set; }
        public int ATRID { get; set; }
        public string BILL_NAME { get; set; }
        public string FULL_NAME { get; set; }
    }


    //Multiple Search
    public class MultipleSearchModel : RequestMode
    {
        [Key]
        public string ORD_NO { get; set; }
        ////public int ORD_TRNS_ID { get; set; }
       
        public string PAT_ID { get; set; }

        public string PAT_NAME { get; set; }
        public string TEL { get; set; }
        //public string MOBILE { get; set; }
       
        public string DOB { get; set; }
        public string SEX { get; set; }
        //public string SAUDI { get; set; }
        public string IDNT { get; set; }
        //public DateTime? REG_DATE { get; set; }
        //public string EMAIL { get; set; }
        //public string ADDRESS { get; set; }
        //public int PRID { get; set; }
        public string ACCN { get; set; }
        public string MDL { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }


    }
    public class MultipleSearchOrderModel : RequestMode
    {
        [Key]
        public string ORD_NO { get; set; }
        ////public int ORD_TRNS_ID { get; set; }
        public string SITE_NO { get; set; }
        public string PAT_ID { get; set; }
        public string CN { get; set; }
        public string CLIENT { get; set; }
        public string REF_NO { get; set; }
        public string LOC { get; set; }
        public string DESCRP { get; set; }
        public string PT { get; set; }
        public string DRNO { get; set; }
        public string DOCTOR { get; set; }
        public string REQ_NO { get; set; }
        public string PHID { get; set; }
        public string NOTES { get; set; }
        public string OTP { get; set; }
        public byte RSVRD { get; set; }
        public DateTime? RSVRD_DTTM { get; set; }
        public string CASH { get; set; }
        public string SMC { get; set; }
        public string VC_NO { get; set; }
        public string INS_NO { get; set; }
        public DateTime? INV_DATE { get; set; }
        public DateTime? INV_DTTM { get; set; }
        public decimal TOTDSCNT { get; set; }
        public decimal TOT_VALUE { get; set; }
        public decimal DSCAMNT { get; set; }
        public decimal NET_VALUE { get; set; }
        public decimal PAID { get; set; }
        public decimal RMNG { get; set; }
        public decimal VAT { get; set; }
        public decimal GRAND_VAL { get; set; }
        public decimal EXTRDSCT { get; set; }
        public string PAYTP { get; set; }
        public string S_TYPE { get; set; }
        public string CLN_IND { get; set; }
        public string COMMENTS { get; set; }
        //public int PR_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string TEL { get; set; }
        public string MOBILE { get; set; }
        public string FAXNO { get; set; }
        public DateTime? DOB { get; set; }
        public string SEX { get; set; }
        public string SAUDI { get; set; }
        public string IDNT { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public int PRID { get; set; }
        public string ACCN { get; set; }


    }

}
