using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.UserAccess
{
    public interface IUserAccessRepository
    {
        Task<IEnumerable<UserAccesseModel>> GetModuleAccessDetailsByUserId(string userId);
    }
}
