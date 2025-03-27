using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class EVTestDefinitionModel : RequestMode
    {
        [IgnoreParameter]
        public int? SNO { get; set; }
        public int? TD_ID { get; set; }
        public string TCODE { get; set; }
        public int? TEST_ID { get; set; }
        public string? FULL_NAME { get; set; }
        public string? UNITS { get; set; }
        public string? LBL_CMNT { get; set; }
        public string? PRTY { get; set; }
        public string? STATUS { get; set; }
        public int? ANL_MTHD_ID { get; set; }
        public string? MTHD { get; set; }
        public string? ORDABLE { get; set; }
        public string? PR { get; set; }
        public string? CT { get; set; }
        public string MDL { get; set; }
        public string PRFX { get; set; }
        public int? TAT { get; set; }
        public string? TATU { get; set; }
        public string? TATC { get; set; }
        public int? TAT_MIN { get; set; }
        public string? RSTP { get; set; }
        public decimal? DEC { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string? MHN { get; set; }
        public string? SHN { get; set; }       
        public decimal? UPRICE { get; set; }

    }

    public class EVReferenceRangeModel : RequestMode
    {
        [IgnoreParameter]
        public int SNO { get; set; }
        public int? EV_REFRNG_ID { get; set; }
        public int TEST_ID { get; set; }
        public string TCODE { get; set; }
        public string S_TYPE { get; set; }
        public string? SP_DESCRP { get; set; }
        public string LHF { get; set; }
        public decimal DEC { get; set; }
        public decimal REF_LOW { get; set; }
        public decimal REF_HIGH { get; set; }

    }

    public class EVTestDefinitionProfileModel : RequestMode
    {
        public int? SNO { get; set; }
        public int? TEST_ID { get; set; }
        public string TCODE { get; set; }
        public string? FULL_NAME { get; set; }
        public string? SYNM { get; set; }
        public string? STATUS { get; set; }

    }

    public class EVProfileGTDModel : RequestMode
    {
        [IgnoreParameter]
        public int? SNO { get; set; }
        public int? GTD_ID { get; set; }
        public string TCODE { get; set; }
        public string? GTDTCODE { get; set; }
        public string? PROFILE_FULLNAME { get; set; }
        public string? REQ_CODE { get; set; }
        public string? PNDG { get; set; }
    }
}
