using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class PatientRegistrationModel: RequestMode
    {
        [Key]
        public int PR_ID { get; set; }
        public string PAT_ID { get; set; }
        public string CN { get; set; }
        public string REF_NO { get; set; }
        public string PAT_NAME { get; set; }
        public string TEL { get; set; }
        public string MOBILE { get; set; }
        public string FAXNO { get; set; }
        public DateTime? DOB { get; set; }
        public string SEX { get; set; }
        public string SAUDI { get; set; }
        public string NATIONALITY { get; set; }
        public string IDNT { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string LOC { get; set; }
        public string PT { get; set; }
        public string DRNO { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public int? PRID { get; set; }
        public string NOTES { get; set; }
        public DateTime? LAST_UPDT { get; set; }
        public Decimal? UPDT_TIME { get; set; }
        public string? sDOB { get; set; }
        public string? AGE { get; set; }
        public string? GENDER { get; set; }
    }
}
