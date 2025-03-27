using DeltaCare.Entity.Model;
using System.Data;

namespace DeltaCare.DAL
{
    public interface IDataRepository
    {
        DataSet ExecuteQuery(string spName, IList<QueryParameterForSqlMapper> QPCollection = null, IList<DataTableParameter> DTPCollection = null);
        Task<DataSet> ExecuteQueryAsync(string spName, IList<QueryParameterForSqlMapper> QPCollection = null, IList<DataTableParameter> DTPCollection = null);
        int ExecuteNonQuery(string spName, IList<QueryParameterForSqlMapper> QPCollection = null, IList<DataTableParameter> DTPCollection = null);
        IEnumerable<T> ExecuteQuery<T>(string storedProcedure, IList<QueryParameterForSqlMapper> parameterCollection);
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string storedProcedure, IList<QueryParameterForSqlMapper> parameterCollection);
        dynamic FetchRecordSet(string storedProcedure, IList<QueryParameterForSqlMapper> parameterCollection);
        IList<dynamic> FetchMultipleRecordSet(string storedProcedure, IList<QueryParameterForSqlMapper> parameterCollection);

        Task<int> ExecuteDataTable(string spName, DataTable dt, string tableType);
        Task<long> ExecuteDataTablePlus(string spName, DataTable dt, string tableType);
    }
}
