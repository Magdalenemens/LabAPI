using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class DoctorFileModel: RequestMode
    {
        public int SNO { get; set; }
        [Key]
        public int DOC_FL_ID { get; set; }
        public string DRNO { get; set; }
        public string DOCTOR { get; set; }
        public string CN { get; set; }
        public string LOC { get; set; }
        public string TEL { get; set; }
        public string FAX { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string NOTES { get; set; }
        public string? NOTESG { get; set; }
    }
}
