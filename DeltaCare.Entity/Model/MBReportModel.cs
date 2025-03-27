using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class MBReportModel : RequestMode
    {
        public int ARF_ID { get; set; }
        public string R_STS { get; set; }
        public string NOTES { get; set; }
    }
}
