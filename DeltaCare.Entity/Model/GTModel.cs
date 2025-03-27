using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class GTModel: RequestMode
    {
        [Key]
        public int GT_ID { get; set; }
        public string GTNO { get; set; }
        public string GRP_NO { get; set; }
        public string B_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string GRP_NAME { get; set; }
        public string AGRP_NAME { get; set; }
        public string SYNM { get; set; }
        public string MTHD { get; set; }
        public string STATUS { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string TS { get; set; }
        public string PRFX { get; set; }
        public string MDL { get; set; }
        public string STS { get; set; }
        public string PRTY { get; set; }
        public string RPT { get; set; }
        public string SUBHDR { get; set; }
        public string RPT_NO { get; set; }
        public string SRRR { get; set; }
        public string S { get; set; }
        public string GP { get; set; }
        public Decimal? TAT { get; set; }
        public string BILL_NAME { get; set; }
        public string BILL { get; set; }
        public string DSCNTG { get; set; }
        public string DT { get; set; }
        public Decimal? DSCNT { get; set; }
        public Decimal? UPRICE { get; set; }
        public string STK_CODE { get; set; }
        public bool UPDT { get; set; }
        public bool DWL { get; set; }
        public string COL_CNDN { get; set; }
    }

    public class GTDModel: RequestMode
    {
        [Key]
        public int GTD_ID { get; set; }
        public string GTNO { get; set; }
        public string GRP_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string DTNO { get; set; }
        public string TCODE { get; set; }
        public string FULL_NAME { get; set; }
        public string PNDG { get; set; }
        public string S_TYPE { get; set; }
        public string MDL { get; set; }
        public string RSTP { get; set; }
        public string S { get; set; }
        public string GP { get; set; }
        public string SEQ { get; set; }
    }

    public class InsertGTDModel : RequestMode
    {
        public string GTNO { get; set; }
        public string GRP_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string DTNO { get; set; }
        public string TCODE { get; set; }
        public string FULL_NAME { get; set; }
        public string PNDG { get; set; }
        public string S_TYPE { get; set; }
        public string MDL { get; set; }
        public string RSTP { get; set; }
        public string S { get; set; }
        public string GP { get; set; }
        public string SEQ { get; set; }
    }

    public class V_GT_GTDModel
    {
        [Key]
        public int GTD_ID { get; set; }
        public string DTNO { get; set; }
        public string FULL_NAME { get; set; }
        public string PNDG { get; set; }
        public string S_TYPE { get; set; }
        public string SEQ { get; set; }
        public int GT_ID { get; set; }
        public string GTNO { get; set; }
        public string GRP_NO { get; set; }
        public string B_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string GRP_NAME { get; set; }
        public string AGRP_NAME { get; set; }
        public string SYNM { get; set; }
        public string MTHD { get; set; }
        public string STATUS { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string TS { get; set; }
        public string PRFX { get; set; }
        public string MDL { get; set; }
        public string STS { get; set; }
        public string PRTY { get; set; }
        public string RPT { get; set; }
        public string SUBHDR { get; set; }
        public string RPT_NO { get; set; }
        public string SRRR { get; set; }
        public string S { get; set; }
        public string GP { get; set; }
        public decimal TAT { get; set; }
        public string BILL_NAME { get; set; }
        public string BILL { get; set; }
        public string DSCNTG { get; set; }
        public string DT { get; set; }
        public decimal DSCNT { get; set; }
        public decimal UPRICE { get; set; }
        public string STK_CODE { get; set; }
        public bool UPDT { get; set; }
        public bool DWL { get; set; }
        public string COL_CNDN { get; set; }
        public string RSTP { get; set; }
    }

}
