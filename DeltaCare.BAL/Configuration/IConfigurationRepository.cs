using DeltaCare.Entity.Model;
using System.Collections.Generic;

namespace DeltaCare.BAL
{
    public interface IConfigurationRepository
    {
        Task<int> InsertSystemConfig(SysConfigModel sysConfigModel);
        Task<int> UpdateSystemConfig(int Id, SysConfigModel sysConfigModel);
        Task<int> DeleteSystemConfig(int Id);
        Task<IEnumerable<SysConfigModel>> GetAllSystemConfig();
        Task<SysConfigModel> GetSystemConfigById(int Id);

        Task<int> InsertSiteTestsAssignment(List<SiteTestsAssignmentModel> siteTestsAssignments);
        Task<int> DeleteSiteTestsAssignment(int Id);
        Task<IEnumerable<SiteTestsAssignmentModel>> GetAllSiteTestsAssignment();
        Task<SiteTestsAssignmentModel> GetSiteTestsAssignmentById(int Id);

    }
}
