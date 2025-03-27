namespace DeltaCare.Entity.Model
{
    public class ClinicalFindingModel : RequestMode
    {
        [IgnoreParameter]
        public int SNO { get; set; }
        [IgnoreParameter]
        public int CLNCFNDG_ID { get; set; }
        [IgnoreParameter]
        public int ORD_NO { get; set; }
        public string ACCN { get; set; }   
        public string AX { get; set; }
        [IgnoreParameter]
        public string T_AX { get; set; }
        [IgnoreParameter]
        public string M_AX { get; set; }        
        [IgnoreParameter]
        public string T_Description { get; set; }
        [IgnoreParameter]
        public string M_Description { get; set; }

    }
}
