using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class AccountManagerModel : RequestMode
    {
        public int SNO { get; set; }
        public int SALESMEN_ID { get; set; }
        [MaxLength(3)]
        [Range(100, 999, ErrorMessage = "Please enter 3 digit integer Number")]
        public string SMC { get; set; }
        [MaxLength(25)]
        public string SALESMAN { get; set; }
    }
}
