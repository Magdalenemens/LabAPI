namespace DeltaCare.Entity.Model
{
    public class IVTemplateModel : RequestMode
    {
        public int IV_ID { get; set; }
        public string DTNO { get; set; }
        public string TCODE { get; set; }
        public string SEX { get; set; }
        public decimal VAL_LOW { get; set; }
        public decimal VAL_HIGH { get; set; }
        public string RESPONSE { get; set; }
        public decimal DEC { get; set; }
        public string RSTP { get; set; }
        public string ABN { get; set; }
        public string IVINTERP { get; set; }
        public decimal? rsultvalue { get; set; }
        
    }
}
