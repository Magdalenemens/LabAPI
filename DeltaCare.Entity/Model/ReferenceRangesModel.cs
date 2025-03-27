using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class ReferenceRangesModel:RequestMode
    {
        [Key]
        public int REF_RNG_ID { get; set; }//REFID
        public string SITE_NO { get; set; }
        public int DTNO { get; set; }
        public string TCODE { get; set; }
        public string RSTP { get; set; }
        public string S_TYPE { get; set; }
        public string SEX { get; set; }
        public decimal? AGE_F { get; set; }
        public string AFF { get; set; }
        public decimal? AGE_T { get; set; }
        public string ATF { get; set; }
        public decimal? AGE_FROM { get; set; }
        public decimal? AGE_TO { get; set; }
        public decimal? REF_LOW { get; set; }
        public decimal? REF_HIGH { get; set; }
        public decimal? CRTCL_LOW { get; set; }
        public decimal? CRTCL_HIGH { get; set; }
        public string LHF { get; set; }
        public string RESPONSE { get; set; }
        public decimal? DEC { get; set; }
        public string REF_RANGE { get; set; }
        public string REF_LC { get; set; }
        public string REF_HC { get; set; }
        public string REMARKS { get; set; }
    }
}
