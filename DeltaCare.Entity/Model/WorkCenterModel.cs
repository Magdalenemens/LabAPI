using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class WorkCenterModel : RequestMode
    {
        public int LAB_WC_ID { get; set; }
        [Required]
        [MaxLength(3)]
        [Range(100, 999, ErrorMessage = "Please enter 3 digit integer Number")]
        public string WC { get; set; }
        [MaxLength(35)]
        public string DESCRIP { get; set; }
    }
}
