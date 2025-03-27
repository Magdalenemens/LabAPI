using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class CytogeneticLoginModel : RequestMode
    {
        public string ACCN { get; set; }
        public string ORD_NO { get; set; }
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string REQ_CODE { get; set; }
        public string CLIN_IND { get; set; }
        public string VOLUME {get; set;}
        public string INTGRTY { get; set; }
        public string CNTR { get; set; }
        public string COLOR { get; set; }
        public string PAC { get; set; }
        public string SEQ { get; set; }
        public string CNST { get; set; }
        public string RCVD_DTTM { get; set; }
        public string SP_TYPE { get; set; }
        public string COMMENTS { get; set; }
        public string SITE_NO { get; set; }
        public string UID { get; set; }

    }

    public class CytogeneticLoginARModel : RequestMode {
        public int AR_ID { get; set; }
        public string CD { get; set; }
        public string RESPONSE { get; set; }
        public int TYPES { get; set; }
    }
}
