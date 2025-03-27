using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class LoginFLModel : RequestMode
    {
        //Login
        public int LOGIN_FL_ID { get; set; }
        public string STATION_ID { get; set; }
        public string U_ID { get; set; }
        public string USER_CODE { get; set; }
        public string FULL_NAME { get; set; }
        public DateTime IN_DTTM { get; set; }
        public DateTime? OUT_DTTM { get; set; }         
    }

    public class ChangePasswordRequestModel: RequestMode
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
