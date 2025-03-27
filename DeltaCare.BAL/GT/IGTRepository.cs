using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DeltaCare.BAL
{
    public interface IGTRepository
    {
        Task<IEnumerable<GTModel>> GetGroupTestsByReqCode(string REQ_CODE); //GET_GT
        Task<IEnumerable<GTDModel>> GetGroupTestsDetailedByParams(string GTNO, string REQ_CODE); //GET_GTD
        Task<IEnumerable<V_GT_GTDModel>> GetGroupTestsDetailedandGroupTests();

    }
}
