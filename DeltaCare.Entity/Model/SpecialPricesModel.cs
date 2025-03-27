namespace DeltaCare.Entity.Model
{
    public class SpecialPricesModel : RequestMode
    {
        public int CLNT_SP_ID { get; set; }
        public string CN { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public decimal UPRICE { get; set; }
        public string DT { get; set; }
        public decimal DSCNT { get; set; }
        public decimal DPRICE { get; set; }
        public decimal FPRICE { get; set; }
        public string B_NO { get; set; }
        public string BILL { get; set; }
        public string TT { get; set; }
        public string DIV { get; set; }
        public string TOCN { get; set; }

        //public string TABLE_NAME { get; set; }

        // Additional properties or methods can be added as needed
    }

    public class SpecialPricesTypeModel
    {
        public int CLNT_SP_ID { get; set; }
        public decimal DSCNT { get; set; }
        public decimal DPRICE { get; set; }
        public string DT { get; set; }


    }
}
