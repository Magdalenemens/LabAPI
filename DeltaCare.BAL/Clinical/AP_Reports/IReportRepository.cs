using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Clinical.AP_Reports
{
    public interface IReportRepository
    {
        Task<int> UpdateAPReport(int Id,APReportModel aPReportModel);
        Task<int> UpdateMBReport(int Id, MBReportModel mBReportModel);
        Task<int> UpdateCGReport(int Id, CGReportModel cgReportModel);

    }
}
