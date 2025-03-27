using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class EVSampleTestModel : RequestMode
    {
        public int SNO { get; set; }
        public int EV_SMPLS_ID { get; set; }
        [Required]
        [MaxLength(15)]
        public string S_TYPE { get; set; }
        [MaxLength(50)]
        public string SP_DESCRP { get; set; }

    }
}
