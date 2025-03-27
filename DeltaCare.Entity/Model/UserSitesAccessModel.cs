using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class UserSitesAccessModel
    {
        [IgnoreParameter]
        public int? SNO { get; set; }       
        public int? USER_SITES_ID { get; set; }
        public string USER_ID { get; set; }
        public string SITE_NO { get; set; }   
        public string? ABRV { get; set; }    
        public string? SITE_NAME { get; set; }
    }

    public class FindAllSitesModel
    {       
        public int? USER_SITES_ID { get; set; }
        public string USER_ID { get; set; }
        public string SITE_NO { get; set; }
        public string? ABRV { get; set; }
        public string? SITE_NAME { get; set; }
    }
}
