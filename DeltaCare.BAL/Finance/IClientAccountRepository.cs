using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Account
{
    public interface IClientAccountRepository
    {
        Task<int> InsertClientAccountEntry(ClientAccountEntryModel clientAccountEntry);
        Task<int> UpdateClientAccountEntry(int Id, ClientAccountEntryModel clientAccountEntry);
        Task<IEnumerable<ClientAccountDataEntryModel>> GetDataEntryList(int id, string companyNo);
        Task<ClientAccountDataEntryModel> GetDataEntryById(int id);
        Task<ClientAccountDataEntryModel> GetClientAccountById(int id, string companyNo);
        Task<IEnumerable<ClientAccountDataEntryModel>> GetClientAccountList(string companyNo);
        Task<IEnumerable<ClientAccountDataEntryModel>> GetClientAccountStatement(int id,string companyNo,DateTime? fromDate= null,DateTime? toDate=null);
        Task<IEnumerable<ClientAccountCrossCheckModel>> GetClientAccountCrossCheckList(ClientAccountCrossCheckDataModel clientAccountCrossCheckDataModel);
        Task<IEnumerable<ClientAccountCurrentStatusModel>> GetClientAccountCurrentStatusList(string companyNo);
    }
}
