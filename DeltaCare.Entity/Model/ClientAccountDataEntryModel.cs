using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class AccountStatemntFilter
    {
        public int Id { get; set; }
        public string? CompanyNo { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }

    public class ClientAccountDataEntryModel : RequestMode
    {
        public int FIRST_ID { get; set; }
        public int CLNT_FL_ID { get; set; }
        public int PREVIOUS_ID { get; set; }
        public int NEXT_ID { get; set; }
        public int LAST_ID { get; set; }
        public int CLNTACNT_ID { get; set; }
        public string? CN { get; set; }
        public DateTime? DATE { get; set; }
        public string? VC_NO { get; set; }
        public string? RCT_NO { get; set; }
        public decimal DEBIT { get; set; }
        public decimal CREDIT { get; set; }
        public string? CLIENT { get; set; }
        public string? TT { get; set; }
        public string? REMARKS { get; set; }
        public decimal YTD_DEBIT { get; set; }
        public decimal YTD_CREDIT { get; set; }
        public decimal BALANCE { get; set; }
        public int TOTAL_DAYS { get; set; }
        
    }

    public class ClientAccountEntryModel : RequestMode
    {
        public int CLNTACNT_ID { get; set; }
        public string? CN { get; set; }
        public DateTime? DATE { get; set; }
        public string? VC_NO { get; set; }
        public string? RCT_NO { get; set; }
        public decimal DEBIT { get; set; }
        public decimal CREDIT { get; set; }
        public string? TT { get; set; }
        public string? REMARKS { get; set; }
    }
    public class ClientAccountCrossCheckModel : RequestMode
    {
        public string? CN { get; set; }
        public string? CLIENT { get; set; }
        public string? VC_NO { get; set; }
        public string? INV_DATE { get; set; }
        public decimal GRAND_VAL { get; set; }
        public decimal DEBIT { get; set; }
        public decimal DIFF { get; set; }


    }

    public class ClientAccountCrossCheckDataModel : RequestMode
    {
        public int DisplayMonth { get; set; }
        public bool IsPositive { get; set; }
        public string? CompanyNo { get; set; }


    }
    public class ClientAccountCurrentStatusModel : RequestMode
    {
        public string? CN { get; set; }
        public string? CLIENT { get; set; }
        public decimal TOTDEBIT { get; set; }
        public decimal TOTCREDIT { get; set; }
        public decimal TOTBALANCE { get; set; }


    }

    public class ClientAccountDataEntry : RequestMode
    {
        public int Id { get; set; }
        public string CompanyNo { get; set; }
    }
}
