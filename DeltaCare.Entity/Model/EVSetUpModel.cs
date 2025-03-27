using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class EVSetUpModel
    {
        public class ANLMethodModel : RequestMode
        {
            //public int? SNO { get; set; }
            public int? ANL_MTHD_ID { get; set; }
            public string R_MTHD { get; set; }
            public string MTHD_NAME { get; set; }
            public string SOP_CODE { get; set; }

        }

        public class EVSubHeaderModel : RequestMode
        {
           // [IgnoreParameter]
          //  public int SNO { get; set; }
            public int EV_SUBHDR_ID { get; set; }
            public string SHN { get; set; }
            public string SHDR_NAME { get; set; }
        }
    }
}
