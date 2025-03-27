using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class MicroBiologyModel : RequestMode
    {
        public int SNO { get; set; }
        public int ARF_ID { get; set; }
        public string ACCN { get; set; }
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string SEX { get; set; }
        public string AGE { get; set; }
        public string DOB { get; set; }
        public string REQ_CODE { get; set; }
        public string TCODE { get; set; }
        public string LOC { get; set; }
        public string PR { get; set; }
        public string CN { get; set; }
        public string CLIENT { get; set; }
        public string DRNO { get; set; }
        public string DOCTOR { get; set; }
        public DateTime DRAWN_DTTM { get; set; }
        public string R_STS { get; set; }
        public string R_ID { get; set; }
        public string V_ID { get; set; }
        public DateTime VER_DTTM { get; set; }
        public string VLDT_ID { get; set; }
        public DateTime RES_DTTM { get; set; }
        public string RSLD_ID { get; set; }
        public DateTime RSLD_DTTM { get; set; }
        public string NOTES { get; set; }
        public string ORD_NO { get; set; }
        public string TNAME { get; set; }
    }

    public class mbSearch : RequestMode { 
        public int id { get; set; } 

        public string name { get; set; }
    }

    public class mbListQRModel : RequestMode
    {
        public string accn { get; set; }
    }


}
