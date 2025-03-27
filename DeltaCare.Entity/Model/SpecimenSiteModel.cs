using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class SpecimenSiteModel : RequestMode
    {
        public int SNO { get; set; }
        public int SP_SITE_ID { get; set; }
        [MaxLength(25)]
        public string SP_SITE { get; set; }
    }
}
