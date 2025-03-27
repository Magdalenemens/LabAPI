using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class CytogeneticsModel
    {
        public int SNO { get; set; }
        public int ARF_ID { get; set; }
        public string ACCN { get; set; }
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string SEX { get; set; }
        public string AGE { get; set; }
        public string DOB { get; set; }
        public string REQ_CODE { get; set; }
        public string TCODE { get; set; }
        public string LOC { get; set; }
        public string PR { get; set; }
        public string CN { get; set; }
        public string CLIENT { get; set; }
        public string DRNO { get; set; }
        public string DOCTOR { get; set; }
        public DateTime DRAWN_DTTM { get; set; }
        public string R_STS { get; set; }
        public string R_ID { get; set; }
        public string V_ID { get; set; }
        public DateTime VER_DTTM { get; set; }
        public string VLDT_ID { get; set; }
        public DateTime RES_DTTM { get; set; }
        public string RSLD_ID { get; set; }
        public DateTime RSLD_DTTM { get; set; }
        public string NOTES { get; set; }
        public string ORD_NO { get; set; }
        public string ISCN { get; set; }
        public string FigLine1 { get; set; }
        public string FigLine2 { get; set; }
        public string RESULTS { get; set; }
    }

    public class rTestModel
    {
        public string rTest { get; set; }
    }

    public class TxtNameModel
    {
        public string R_STS { get; set; }
        public string R_SEQ { get; set; }
        public string R_NAME { get; set; }
        public string R_Result { get; set; }
        public string R_ArfId { get; set; }
    }

    public class CGInsertTxtResModel : RequestMode
    {
        public string R_NAME { get; set; }
        public string TXT_RES { get; set; }
        public string R_SEQ { get; set; }
        public string ARFID { get; set; }
    }

    public class CGInsertParamModel : RequestMode
    {
        public int ARF_ID { get; set; }
        public string R_STS { get; set; }
        public int QueryType { get; set; }
    }

    public class CytogeneticListModel : RequestMode
    {
        public int ARF_ID { get; set; }
        public string SITE_NO { get; set; }
        public string ACCN { get; set; }
        public string REQ_CODE { get; set; }
        public string PAT_ID { get; set; }
        public string CN { get; set; }
        public string DRNO { get; set; }
        public string ORDER_DTTMSTR { get; set; }
        //public DateTime ORDER_DTTM { get; set; }
        public string COL_DTTM { get; set; }
        public string DURATION { get; set; }
        public string Sts { get; set; }
        public string Descrip { get; set; }

    }

    public class CytogeneticSearchModel
    {
        public string ordeR_FDTTM { get; set; }
        public string ordeR_TDTTM { get; set; }
        public string cn { get; set; }
        public string sitE_NO { get; set; }

    }
    public class ClinicalImageModel : RequestMode
    {
        public int APP_IMAGES_ID { get; set; }
        public string? IMAGE_ID { get; set; }
        public string? ACCN { get; set; }
        public string? TCODE { get; set; }
        public string? IMAGE { get; set; }

    }
}
