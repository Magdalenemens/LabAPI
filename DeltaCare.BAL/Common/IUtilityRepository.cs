using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Common
{
    public interface IUtilityRepository
    {
        Task<int> GetMaxValueAsync(string tableName, string columnName);
    }
}
