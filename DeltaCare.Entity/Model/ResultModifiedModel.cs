using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class ResultModifiedModel : RequestMode
    {
        public int RES_MDF_ID { get; set; }
        public string PAT_ID { get; set; }
        public string ACCN { get; set; }
        public string TCODE { get; set; }
        public string CRESULT { get; set; }
        public DateTime CVER_DTTM { get; set; }
        public string CV_ID { get; set; }
        public string RESULT { get; set; }
        public DateTime VER_DTTM { get; set; }
        public string V_ID { get; set; }
    }
}
