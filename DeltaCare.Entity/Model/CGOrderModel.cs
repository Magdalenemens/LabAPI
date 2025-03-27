using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class CGOrderModel : RequestMode
    {
        public int SNO { get; set; }
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string GENDER { get; set; }
        public string DOB { get; set; }
        public string AGE { get; set; }
        public string NATIONALITY { get; set; }
        public string IQAMA { get; set; }
        public string ORD_SEQ { get; set; }
        public string CONSENT { get; set; }
        public string VOLUME { get; set; }
        public string INTGRTY { get; set; }
        public string COLOR { get; set; }
        public string PAC { get; set; }
        public string CNTR { get; set; }
        public string COMMENTS { get; set; }
        public string CNST { get; set; }
        public string CLN_IND { get; set; }
        public string DRNO { get; set; }
        public string DOCTOR { get; set; }
        public bool CASH { get; set; }
        public string REF_NO { get; set; }
        public string TEL { get; set; }
        public string EMAIL { get; set; }
        public string CN { get; set; }
        public string RCVD_DATE { get; set; }
        public string ORD_NO { get; set; }
        public string S_TYPE { get; set; }
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
 
    }

    public class CGOrderATRModel : RequestMode
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


    public class CGOrderATRInsertModel : RequestMode
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

}
