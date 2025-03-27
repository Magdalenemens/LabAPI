using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeltaCare.Entity.Model
{
    public class BillingModel : RequestMode
    {        
        public string BDETAIL_DATE { get; set; }
        public int BL_FILE_ID { get; set; }
        public string VC_NO { get; set; }
        public string CN { get; set; }
        public DateTime ST_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public Date DATE { get; set; }
        public DateTime INV_DTTM { get; set; }
        public decimal TOT_VALUE { get; set; }
        public decimal DSCNTP { get; set; }
        public decimal DSCNTAMNT { get; set; }
        public decimal NET_VALUE { get; set; }
        public decimal VAT { get; set; }
        public decimal SVAT { get; set; }
        public decimal GRAND_VAL { get; set; }
        public int BL_DTLS_ID { get; set; }
        public string SEQ { get; set; }
        public string DIV { get; set; }
        public string PAT_ID { get; set; }
        public string REF_NO { get; set; }
        public string CUSTOM_SR { get; set; }
        public string SAUDI { get; set; }
        public string REQ_NO { get; set; }
        public string ACCN { get; set; }
        public string INS_NO { get; set; }
        public int ATRID { get; set; }
        public string DRAWN_DATE { get; set; }
        public string REQ_DATE { get; set; }
        public string REQ_CODE { get; set; }
        public string TCODE { get; set; }
        public string B_NO { get; set; }
        public string FULL_NAME { get; set; }
        public string BILL_NAME { get; set; }
        public string BILL { get; set; }
        public decimal UPRICE { get; set; }
        public string DT { get; set; }
        public decimal DSCNT { get; set; }
        public decimal DPRICE { get; set; }
        public string BT { get; set; }
        public string KP { get; set; }
        public decimal CNT { get; set; }
        public decimal TPRICE { get; set; }
        public string DOCTOR { get; set; }
        public string PAT_NAME { get; set; }

    }

    public class ClientNumberModel : RequestMode
    {
        public int? SNO { get; set; }
        public int BL_FILE_ID { get; set; }
        public string VC_NO { get; set; }
        public string CN { get; set; }
        public string ST_DATE { get; set; }
        public string END_DATE { get; set; }
        public string HEADER_DATE { get; set; }
        public string INV_DTTM { get; set; }
        public decimal TOT_VALUE { get; set; }
        public decimal DSCNTP { get; set; }
        public decimal DSCNTAMNT { get; set; }
        public decimal NET_VALUE { get; set; }
        public decimal VAT { get; set; }
        public decimal SVAT { get; set; }
        public decimal GRAND_VAL { get; set; }        
    }
}
    