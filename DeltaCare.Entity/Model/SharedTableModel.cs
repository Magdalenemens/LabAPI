using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class SharedTableModel : RequestMode
    {
        public int SHRD_TBL_ID { get; set; }
        public string OTP { get; set; }
        public string DESCRIP { get; set; }
        public string UNITS { get; set; }
        public string S_TYPE { get; set; }
        public string PAYTP { get; set; }
        public string AR_CD { get; set; }
        public string AR_RSPNS { get; set; }
        public string CONTAINER { get; set; }
        public string LBL_CMNT { get; set; }
        public string NATIONALITY { get; set; }

    }
}
