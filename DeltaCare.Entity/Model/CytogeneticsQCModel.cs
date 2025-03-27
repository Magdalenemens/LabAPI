
namespace DeltaCare.Entity.Model
{
    public class CytogeneticsQCModel: RequestMode
    {
        public int CG_QC_ID { get; set; }
        public string ORD_NO { get; set; }
        public string ACCN { get; set; }
        public string REQ_CODE { get; set; }
        public string CLTR_TP { get; set; }
        public string CLTR_NO { get; set; }
        public string HRVST_TP { get; set; }
        public DateTime? HRVST_DATE { get; set; }
        public string MIT_NDX { get; set; }
        public string MTPH_QLTY { get; set; }
        public string BNDG_TP { get; set; }
        public string BNDG_RES { get; set; }
        public string FISH_SNGL { get; set; }
        public string SMPL_RET { get; set; }
    }
}
