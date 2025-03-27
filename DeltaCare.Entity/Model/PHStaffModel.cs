using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class PHStaffModel : RequestMode
    {
        public int PH_ID { get; set; }
        [MaxLength(3)]
        public string PHID { get; set; }
        [MaxLength(25)]
        public string PH_NAME { get; set; }
    }
}
