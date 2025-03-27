using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Clinical.AP_Reports
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDataRepository _datarepository;

        public ReportRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }
        public async Task<int> UpdateAPReport(int Id, APReportModel apReportModel)
        {
            apReportModel.ARF_ID = Id;           
            int queryType = (int)QueryTypeEnum.Update;
            apReportModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(apReportModel);
            IEnumerable<APReportModel> result = await _datarepository.ExecuteQueryAsync<APReportModel>(SPConstant.Sp_ManageAPReportStatus, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> UpdateMBReport(int Id, MBReportModel mbReportModel)
        {
            mbReportModel.ARF_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            mbReportModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(mbReportModel);
            IEnumerable<MBReportModel> result = await _datarepository.ExecuteQueryAsync<MBReportModel>(SPConstant.Sp_ManageMBReportStatus, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> UpdateCGReport(int Id, CGReportModel cgReportModel)
        {
            cgReportModel.ARF_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            cgReportModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CGReportModel>(cgReportModel, "QueryType");
            IEnumerable<CGReportModel> result = await _datarepository.ExecuteQueryAsync<CGReportModel>(SPConstant.Sp_ManageCytogenetics, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
