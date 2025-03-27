using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class UserFLModel : RequestMode
    {
        [IgnoreParameter]
        public int? SNO { get; set; }
        public int? USER_FL_ID { get; set; }
        public string USER_ID { get; set; }
        public int? ROLE_ID { get; set; }
        public string ROLE_NAME { get; set; }
        [IgnoreParameter]
        public string USER_ROLE { get; set; }
        public string? USER_CODE { get; set; }
        [MaxLength(12)]
        public string USER_NAME { get; set; }
        [MaxLength(30)]
        public string FULL_NAME { get; set; }
        [MaxLength(10)]
        public string NAT_ID { get; set; }
        [MaxLength(10)]
        public string? TEL { get; set; }
        [MaxLength(35)]
        public string EMAIL { get; set; }
        [MaxLength(2)]
        public string? JOB_CD { get; set; }
        public string? U_LVL { get; set; }
        [MaxLength(200)]
        public string? PASS_WORD { get; set; }
        public string DEF_SITE { get; set; }
        public bool? BILLING { get; set; }
        public bool? NDC { get; set; }
        public bool NPC { get; set; }
        public decimal? MD { get; set; }
        public bool? RAO { get; set; }
        public bool AMIN { get; set; }
        public decimal? LGNLMT { get; set; }
        public decimal? LGNCNT { get; set; }
        public string? PRNTMOD { get; set; }
        public bool? PAGEHDR { get; set; }
        public bool? ELECSNGR { get; set; }
        public bool? ESNGRACS { get; set; }
        public bool? DFLTSNGR { get; set; }
        public bool? SNGRRSTRCT { get; set; }
        public bool? SP_UPDATE { get; set; }
        public bool? VALIDATOR { get; set; }
        public bool? VLDTPRMT { get; set; }
        public bool? RSLDPRINT { get; set; }
        public bool? SITESLCT { get; set; }
        public string? SIGN_LINE1 { get; set; }
        public string? SIGN_LINE2 { get; set; }
        public string? NOTES { get; set; }        
        public string? SNGR { get; set; }
        public bool? IS_ACTIVE { get; set; }
    }

    public class UserJobTypeModel : RequestMode
    {
        public string? USER_ID { get; set; }
        public string? USER_CODE { get; set; }
        public string? USER_NAME { get; set; }
        public string? FULL_NAME { get; set; }
        public int JOB_TLT_ID { get; set; }
        public string JOB_CD { get; set; }
        public string JOB_TITLE { get; set; }

    }

    public class UserAccesseModel : RequestMode
    {
        public int? CM_ACCESS_ID { get; set; }
        public string USER_ID { get; set; }
        public string? SITE_NO { get; set; }
        public string? SITE_NAME { get; set; }
        public string? MDL { get; set; }
        public bool? A_VIEW { get; set; }
        public bool? A_EDIT { get; set; }
        public bool? A_VERIFY { get; set; }
        public bool? A_VALIDATE { get; set; }
        public bool? A_AMEND { get; set; }
        public bool? A_PRINT { get; set; }

    }
}
