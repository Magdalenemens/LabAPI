using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class APReportModel : RequestMode
    {
        public int ARF_ID { get; set; }
        public string R_STS { get; set; } 
        public string NOTES { get; set; }

    }

    public class APImageModel : RequestMode {
        public string? ACCN { get; set; }
        public string? TCODE { get; set; }
        public string? IMAGE { get; set; }

    }
}
