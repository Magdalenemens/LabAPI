using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Account
{
    public interface IAccountRepository
    {
        int InvokeBilling();

        int DeleteBilling();
        Task<IEnumerable<BillingModel>> GetBillingData(string cn);
        Task<IEnumerable<ClientNumberModel>> GetClientNumber();
    }
}
