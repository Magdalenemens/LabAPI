using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class ResultTypeModel : RequestMode
    {
        public int RESTYPE_ID { get; set; }
        [MaxLength(1)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please enter only alphabets.")]
        public string RSTP { get; set; }
        [MaxLength(30)]
        public string DESCRIP { get; set; }
    }
}
