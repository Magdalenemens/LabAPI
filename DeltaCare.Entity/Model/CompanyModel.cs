using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class CompanyModel : RequestMode
    {
        public int SNO { get; set; }
        public string CMP_DTLS_ID { get; set; }
        public string NAME { get; set; }
        public string COMPANY { get; set; }
        public string TLT_01 { get; set; }
        public string TLT_02 { get; set; }
        public string TLT_03 { get; set; }

    }
}