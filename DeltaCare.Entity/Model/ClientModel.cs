using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DeltaCare.Entity.Model
{
    public class ClientModel : RequestMode
    {
        [IgnoreParameter]
        public int? SNO { get; set; }
        public int? CLNT_FL_ID { get; set; }
        [MaxLength(4)]
        public string CN { get; set; }
        [MaxLength(2)]
        public string? CMPNY_NO { get; set; }
        public string? SITE_NO { get; set; }
        public string CLIENT { get; set; }
        public string? ACLIENT { get; set; }
        public string? CTGRY_CD { get; set; }
        public string? ACMNGR { get; set; }
        public string? DRVRC { get; set; }
        public string? GRP { get; set; }
        public string? CLNTVAT { get; set; }
        public string? TEL { get; set; }
        //public string? Fax { get; set; }
        public string? MOBILE { get; set; }
        public string? CONTACT { get; set; }
        public string? EMAIL { get; set; }
        public string? EMAIL2 { get; set; }
        public string? EMAIL3 { get; set; }
        public string? CLNT_ADDRESS { get; set; }
        public bool? TELREQ { get; set; }
        public bool? REQNOREQ { get; set; }
        public Decimal? DSCNT { get; set; }
        public Decimal? DSCNT_2 { get; set; }
        public Decimal? DSCNT_3 { get; set; }
        public Decimal? DSCNT_4 { get; set; }
        public Decimal? DSCNT_5 { get; set; }
        public bool? ADSCNT { get; set; }
        public bool? CASH { get; set; }
        public bool? CASHONLY { get; set; }
        public bool? CRDTONLY { get; set; }
        public bool? HABN { get; set; }
        public bool? CU { get; set; }
        public bool? INACTV { get; set; }
        public bool? SPCL { get; set; }
        public string? NOFAX { get; set; }
        public bool? ZEROVAL { get; set; }
        public Decimal? YTD_DEBIT { get; set; }
        public Decimal? YTD_CREDIT { get; set; }
        public Decimal? BALANCE { get; set; }
        public Decimal? MAXCRDT { get; set; }
        public DateOnly? LST_ST_DT { get; set; }
        public string? LST_ST_NO { get; set; }
        public Decimal? LST_BAL { get; set; }
        public string? NOTES { get; set; }
        public string? AC_NO { get; set; }
        public Decimal? SP1 { get; set; }
        public Decimal? SP3 { get; set; }
        public string? CLNT_TP { get; set; }
        [IgnoreParameter]
        public string? CNCLIENT { get; set; }

    }

    public class CLNT_GRPModel : RequestMode
    {
        [Key]
        public int CLNT_GRP_ID { get; set; }
        public string GRP { get; set; }
        public string GRP_NAME { get; set; }
    }
    public class CLNT_SPModel : RequestMode
    {
        [Key]
        public int CLNT_SP_ID { get; set; }
        public string CN { get; set; }
        public string TCODE { get; set; }
        public string REQ_CODE { get; set; }
        public string B_NO { get; set; }
        public string BILL_NAME { get; set; }
        public string BT { get; set; }
        public Decimal? UPRICE { get; set; }
        public string DT { get; set; }
        public Decimal? DSCNT { get; set; }
        public Decimal? DPRICE { get; set; }
    }
    public class CLNTACNTModel : RequestMode
    {
        [Key]
        public int CLNTACNT_ID { get; set; }
        public string CN { get; set; }
        public string VC_NO { get; set; }
        public string RCT_NO { get; set; }
        public DateTime? INV_DATE { get; set; }
        public string TT { get; set; }
        public DateTime? DATE { get; set; }
        public Decimal? DEBIT { get; set; }
        public Decimal? BALANCE { get; set; }
        public string REMARKS { get; set; }
        public string SI { get; set; }
        public string S { get; set; }
        public string U_ID { get; set; }
        public Decimal? AGE_DAYS { get; set; }
    }
}
