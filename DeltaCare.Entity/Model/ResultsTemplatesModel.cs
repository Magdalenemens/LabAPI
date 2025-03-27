using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class ResultsTemplatesModel : RequestMode
    {
        public int SNO { get; set; }
        public int RS_TMPLT_ID { get; set; }
        public string TNO { get; set; }
        [MaxLength(25)]
        public string TNAME { get; set; }
        [MaxLength(2)]
        [Range(01, 99, ErrorMessage = "Please enter 2 digit integer Number")]
        public string DIV { get; set; }
        public string TEMPLATE { get; set; }
    }
}
