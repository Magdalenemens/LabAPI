namespace DeltaCare.Entity.Model
{
    public class PathFindingModel : RequestMode
    {
        [IgnoreParameter]
        public int SNO { get; set; }
        
        [IgnoreParameter]
        public int PATHNMCR_ID { get; set; }
        
        [IgnoreParameter]
        public int CLNCFNDG_ID { get; set; }

        [IgnoreParameter]
        public string AX { get; set; }

        [IgnoreParameter]
        public string SEQ { get; set; }

        [IgnoreParameter]
        public string DESCRIP { get; set; }
        public string AX_NMBR { get; set; }
        public string ACCN { get; set; }
        public int ORD_NO { get; set; }
        [IgnoreParameter]
        public string T_Description { get; set; }
        [IgnoreParameter]
        public string M_Description { get; set; }
        


    }
}
