using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class SiteTestsAssignmentModel : RequestMode
    {
        public int? SITE_TESTS_ID { get; set; }
        public int? SNO { get; set; }
        public string REF_SITE { get; set; }
        public string REF_SITE_S { get; set; }
        public string TCODE { get; set; }
        public string? FULL_NAME { get; set; }
        public string? CT { get; set; }
        public string TEST_ID { get; set; }
        public string? SELECTED_REF_SITE { get; set; }
        public string? ABRV { get; set; }
        

    }
}
