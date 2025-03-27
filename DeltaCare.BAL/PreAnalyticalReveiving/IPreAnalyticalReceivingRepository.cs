using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL
{
    public interface IPreAnalyticalReceivingRepository
    {
        Task<IEnumerable<ORD_DTLModel>> GetOrdersDetailsByAccn(string ACCN, string STS);
        //Task<int> CentralReceivingOrders(Object[] ORDs, string ACCN );
        Task<int> UpdatePreAnalyticalReceiving(Object[] ORDs, string ACCN, string REQ_CODE, string SECT, int ATRID, string ORD_NO, string SITE_NO, string U_ID);
    }
}
