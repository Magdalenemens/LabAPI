using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class CGReportModel : RequestMode
    {
        public int ARF_ID { get; set; }
        public string ISCN { get; set; }
        public string FigLine1 { get; set; }
        public string FigLine2 { get; set; }
        public string Results { get; set; }
        public string NOTES { get; set; }
        public string R_STS { get; set; }
    }

}
