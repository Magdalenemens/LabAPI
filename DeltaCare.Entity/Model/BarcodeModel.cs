using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class BarcodeModel
    {
        //[Key]
        public string SECT { get; set; }
         //public string ORD_NO { get; set; }
        public string ACCN { get; set; }

        public string REQ_CODE { get; set; }
        public string LABEL_NAME { get; set; }
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string SEX { get; set; }
        public string CN { get; set; }
        public string CLIENT { get; set; }
        public string MDL { get; set; }
        public string REQ_NO { get; set; }
        public string PAT_NO { get; set; }
    }

    public class QRListQRModel : RequestMode
    {
        public string accn { get; set; }
    }
   
}
