using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class MBIsolModel : RequestMode
    {
        public int RNO { get; set; }
        public int MB_ISOL_ID { get; set; }
        public int ARF_ID { get; set; }
        public string ISOL_CD { get; set; }
        public string ORD_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string DESCRIP { get; set; }
        public string NO { get; set; }
        public string R_STS { get; set; }
        public string F { get; set; }
        public string SEARCH { get; set; }
    }

    public class MBIsolInsertModel : RequestMode
    {
        //public int MB_ISOL_ID { get; set; }
        public string ORD_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string NO { get; set; }
        public string ISOL_CD { get; set; }
        public string R_STS { get; set; }
        public string F { get; set; }
    }
}
