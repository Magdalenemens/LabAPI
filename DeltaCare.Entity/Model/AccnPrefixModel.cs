using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class AccnPrefixModel : RequestMode
    {
        public int ACCNPRFX_ID { get; set; }
        [MaxLength(2)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please enter only alphabets.")]
        public string PRFX { get; set; }
        [MaxLength(20)]
        public string DESCRIP { get; set; }

        public string REF_SITE { get; set; }
        public string NEXTACCN { get; set; }
        public string UPDT { get; set; }
        public DateTime? CUR_DATE { get; set; }
        public string LASTYRACCN { get; set; }
        public decimal? CUR_YEAR { get; set; }
        public string DFLT_SET { get; set; }
    }
   
    
    

}