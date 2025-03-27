using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class ReportSubHeaderModel : RequestMode
    {
        public int RPT_SHDR_ID { get; set; }
        [MaxLength(2)]
        [Range(01, 99, ErrorMessage = "Please enter 2 digit integer Number")]
        public string MHN { get; set; }
        [MaxLength(3)]
        [Range(100, 999, ErrorMessage = "Please enter 3 digit integer Number")]
        public string SHN { get; set; }
        [MaxLength(40)]
        public string SHDR_NAME { get; set; }

    }
}
