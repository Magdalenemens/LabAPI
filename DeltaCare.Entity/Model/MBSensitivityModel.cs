using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class MBSensitivityModel : RequestMode
    {
        public bool ISCHECKED { get; set; }
        public string CODE { get; set; }
        public int ARF_ID { get; set; }
        public int ORD_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string SREQ_CODE { get; set; }
        public string TCODE { get; set; }
        public string FULL_NAME { get; set; }
        public string TYPE { get; set; }
        public int RNO { get; set; }
        public string STS { get; set; }
        public string R_STS { get; set; }
        public string RESULT { get; set; }
        public bool SPRS { get; set; }
        public string AF { get; set; }
        public string MIC { get; set; }
        public string ISOLATE { get; set; }
    }

    public class MBSensitivityInsertModel : RequestMode
    {
        //public int MBR_SNS_ID { get; set; }
        public int ORD_NO { get; set; }
        public string NO { get; set; }
        public string REQ_CODE { get; set; }
        public string SREQ_CODE { get; set; }
        public string ISOL_CD { get; set; }
        public string TCODE { get; set; }
        public string RESULT { get; set; }
        public bool SPRS { get; set; }
        public string R_STS { get; set; }
        public string AF { get; set; }
        public string MIC { get; set; }
    }
}
