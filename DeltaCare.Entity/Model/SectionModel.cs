using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class SectionModel : RequestMode
    {
        public int LAB_SECT_ID { get; set; }

        [Required]
        [MaxLength(3)]
        [Range(100, 999, ErrorMessage = "Please enter 3 digit integer Number")]
        public string SECT { get; set; }

        [MaxLength(3)]
        public string ABRV { get; set; }

        [MaxLength(2)]
        [Range(01, 99, ErrorMessage = "Please enter 2 digit integer Number")]
        public string DIV { get; set; }

        [MaxLength(35)]
        public string DESCRIP { get; set; }

        [IgnoreParameter]
        public string? SECTDESCRIP { get; set; }
    }
}
