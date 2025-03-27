using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class SpecimentypeModel : RequestMode
    {
        public int SNO { get; set; }
        public int SP_TYPE_ID { get; set; }
        [Required]
        [MaxLength(20)]
        public string SP_CODE { get; set; }
        [MaxLength(35)]
        public string SP_DESCRP { get; set; }

    }
}
