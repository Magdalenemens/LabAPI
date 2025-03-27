using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class TestSiteModel : RequestMode
    {
        public int LAB_TS_ID { get; set; }
        [Required]
        [MaxLength(3)]
        [Range(100, 999, ErrorMessage = "Please enter 3 digit integer Number")]
        public string TS { get; set; }
        [MaxLength(3)]
        [Range(100, 999, ErrorMessage = "Please enter 3 digit integer Number")]
        public string WC { get; set; }
        [MaxLength(30)]
        public string DESCRIP { get; set; }
        [IgnoreParameter]
        public string? TSDESCRIP { get; set; }
    }
}
