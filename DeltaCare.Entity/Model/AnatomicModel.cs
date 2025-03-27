using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class AnatomicModel : RequestMode
    {
        public int SNO { get; set; }       
        public int ORD_NO { get; set; }
        public int ARF_ID { get; set; }
        public string ACCN { get; set; }
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string SEX { get; set; }
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
    }
   
}


