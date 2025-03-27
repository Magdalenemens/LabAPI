using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class ReportMainHeaderModel : RequestMode
    {
        public int RPT_MHDR_ID { get; set; }
        [Required]
        [MaxLength(2)]
        [Range(01, 99, ErrorMessage = "Please enter 2 digit integer Number")]
        public string MHN { get; set; }
        [MaxLength(60)]
        public string MHDR_NAME { get; set; }
    }
}
