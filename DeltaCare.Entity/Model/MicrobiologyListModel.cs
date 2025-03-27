using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class MicrobiologyListModel : RequestMode
    {
        public int ARF_ID { get; set; }
        public string SITE_NO { get; set; }
        public string ACCN { get; set; }
        public string REQ_CODE { get; set; }
        public string PAT_ID { get; set; }
        public string CN { get; set; }
        public string DRNO { get; set; }
        public string ORDER_DTTMSTR { get; set; }
        //public DateTime ORDER_DTTM { get; set; }
        public string COL_DTTM { get; set; }
        public string DURATION { get; set; }
        public string Sts { get; set; }
        public string Descrip { get; set; }

    }

    public class MicrobiologySearchModel {
        public string ordeR_FDTTM { get; set; }
        public string ordeR_TDTTM { get; set; }
        public string cn { get; set; }
        public string  sitE_NO { get; set; }

    }
}
