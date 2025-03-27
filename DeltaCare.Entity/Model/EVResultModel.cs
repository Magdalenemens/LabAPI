using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class EVResultModel : RequestMode
    {
        public int SNO { get; set; }
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string REF_NO { get; set; }
        public string CN { get; set; }
        public string CLIENT { get; set; }
        public string ORD_NO { get; set; }
        public string ACCN { get; set; }
        public string RCVD_DATE { get; set; }
        [IgnoreParameter]
        public string? CUSTOM_SR { get; set; }

    }

    public class EVResultARFModel : RequestMode
    {
        public string ACCN { get; set; }
        public string UPDATETYPE { get; set; }
        public string TCODE { get; set; }
        public string REQ_CODE { get; set; }
        public string R_STS { get; set; }
        public string RESULT { get; set; }
        public string F { get; set; }
        public string REF_RANGE { get; set; }
        public string UNITS { get; set; }
        public string FULL_NAME { get; set; }
        public string FULL_NAMEPLUS { get; set; }
        public string RSID { get; set; }
        public string VLDT { get; set; }
        public string FINDING { get; set; }
        public string NOTES { get; set; }
        public string FNLRES { get; set; }
        public string SHN { get; set; }

    }

    public class EVResultStatusModel : RequestMode
    {
        public string ACCN { get; set; }
        public string R_STS { get; set; }
        public int QueryType { get; set; }
    }

    public class evSearch : RequestMode
    {
        public int id { get; set; }

        public string name { get; set; }
    }

    public class EVDetailModel : RequestMode
    {
        public string SITE_NO { get; set; }
        public string ACCN { get; set; }
        public string ORDER_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string FULL_NAME { get; set; }
        public string PAT_ID { get; set; }
        public string PAT_NAME { get; set; }
        public string MOBILE { get; set; }
        public string CN { get; set; }
        public string CLIENT { get; set; }
        public string REF_NO { get; set; }
        public string TCODE { get; set; }
        public string ORDER_DTTM { get; set; }
        public string COL_DTTM { get; set; }
        public string RES_DTTM { get; set; }
        public string VER_DTTM { get; set; }
        public string VLDT_DTTM { get; set; }
        public string O_ID { get; set; }
        public string R_ID { get; set; }
        public string V_ID { get; set; }
        public string VLDT_ID { get; set; }
        public string ORDERED_BY { get; set; }
        public string RESULTD_BY { get; set; }
        public string VERIFIED_BY { get; set; }
        public string VALIDATED_BY { get; set; }
        public string R_STS { get; set; }
        public string SHDR_NAME { get; set; }
        public string DESCRIP { get; set; }
        public string CLN_IND { get; set; }

    }

    public class EvResultDetailModel : RequestMode
    {
        public string ACCN { get; set; }
        public string ORD_NO { get; set; }
        public string TCODE { get; set; }
        public int QueryType { get; set; }
    }


}
