using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class SysConfigModel : RequestMode
    {
        public int SYSCNFG_ID { get; set; }
        public string SEQ { get; set; }
        public string DEF_DESCRP { get; set; }
        public string DEF_NAME { get; set; }
        public string DEF_VAL { get; set; }
        public DateTime DATE { get; set; }
    }
}
