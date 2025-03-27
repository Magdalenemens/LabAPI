using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class APReceivingModel : RequestMode
    {
        public int SNO { get; set; }
        public int AP_CASES_ID { get; set; }
        public int ORD_DTL_ID { get; set; }
        public string ORD_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string ACCN { get; set; }
        public string? CLN_IND { get; set; }      
        public string PTHGST_NAM { get; set; }
        public string? MDL { get; set; }
        public string? PAT_ID { get; set; }
        public string? PAT_NAME { get; set; }
        public string PRCVD_ID { get; set; }
        public DateTime PRCVD_DTTM { get; set; }
        public DateTime? DRAWN_DTTM { get; set; }
        public string SITE_NO { get; set; }
        public string? SECT { get; set; }
        public string U_ID { get; set; }

    }
}
