using DeltaCare.BAL;
using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL
{
    public class GTRepository:IGTRepository
    {
        private readonly IDataRepository _datarepository;

        public GTRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }

        public async Task<IEnumerable<GTModel>> GetGroupTestsByReqCode(string REQ_CODE)//GET_GT
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(REQ_CODE, queryType, "REQ_CODE", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<GTModel>("usp_ManageGroupTests", parameters)).ToList();
        }

        public async Task<IEnumerable<GTDModel>> GetGroupTestsDetailedByParams(string GTNO, string REQ_CODE)//GET_GTD
        {
            int queryType = (int)QueryTypeEnum.GetById;
            GTDModel gTDModel = new GTDModel();
            gTDModel.QueryType = queryType;
            gTDModel.GTNO = GTNO;
            gTDModel.REQ_CODE = REQ_CODE;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList<GTDModel>(gTDModel);
            return (await _datarepository.ExecuteQueryAsync<GTDModel>(SPConstant.SP_ManageGroupTestsDetailed, parameters)).ToList();

            //<ORD_TRNSModel>(oRD_TRNSModel)

        }

        public async Task<IEnumerable<V_GT_GTDModel>> GetGroupTestsDetailedandGroupTests()//V_GT_GTD
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<V_GT_GTDModel>("usp_GetGroupTestsDetailedandGroupTests", parameters)).ToList();
        }
    }
}
