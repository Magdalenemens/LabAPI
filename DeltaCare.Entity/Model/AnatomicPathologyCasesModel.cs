using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class AnatomicPathologyCasesModel:RequestMode
    {
        public int AP_CASES_ID { get; set; }
        public string ORD_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string ACCN { get; set; }
        public string CLN_IND { get; set; }
        public string PTHGST_CD { get; set; }
        public string PTHGST_NAM { get; set; }
        public string COMMENTS { get; set; }
        public string MDL { get; set; }
    }
}
