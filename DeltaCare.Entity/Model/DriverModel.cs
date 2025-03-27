using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class DriverModel : RequestMode
    {
        public int SNO { get; set; }
        public int DRVRS_ID { get; set; }
        [MaxLength(3)]
        [Range(100, 999, ErrorMessage = "Please enter 3 digit integer Number")]
        public string DRVRC { get; set; }
        [MaxLength(25)]
        public string DRVRNAME { get; set; }
    }
}
