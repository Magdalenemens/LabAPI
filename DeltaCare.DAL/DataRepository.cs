using Dapper;
using DeltaCare.Entity.Model;
using DeltaCare.Logger;
using System.Data;
using System.Data.SqlClient;

namespace DeltaCare.DAL
{
    public class DataRepository : IDataRepository
    {
        private DbConnection _dbconnection;
        private const int commandTimeout = 100; //ConfigurationSettings.SqlCommandTimeout
        public DataRepository()
        {
            _dbconnection = DbConnection.Instance;
        }
        public DataSet ExecuteQuery(string spName, IList<QueryParameterForSqlMapper> QPCollection = null, IList<DataTableParameter> DTPCollection = null)
        {
            DataSet resultSet = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            //cmd.CommandTimeout = ConfigurationSettings.SqlCommandTimeout;
                            cmd.CommandTimeout = commandTimeout;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (QPCollection != null && QPCollection.Count() > 0)
                            {
                                foreach (QueryParameterForSqlMapper param in QPCollection)
                                {
                                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                                    cmd.Parameters[param.Name].Direction = param.ParameterDirection;
                                }
                            }
                            if (DTPCollection != null && DTPCollection.Count() > 0)
                            {
                                foreach (DataTableParameter dataTableParameter in DTPCollection)
                                {
                                    cmd.Parameters.AddWithValue(dataTableParameter.ParameterName, dataTableParameter.DataTable);
                                }
                            }
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(resultSet);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DeltaCareLogger.Error(ex.Message);
                throw ex;
            }
            return resultSet;
        }

        public async Task<DataSet> ExecuteQueryAsync(string spName, IList<QueryParameterForSqlMapper> QPCollection = null, IList<DataTableParameter> DTPCollection = null)
        {
            DataSet resultSet = new DataSet();

            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            cmd.CommandTimeout = commandTimeout;
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (QPCollection != null && QPCollection.Count() > 0)
                            {
                                foreach (QueryParameterForSqlMapper param in QPCollection)
                                {
                                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                                    cmd.Parameters[param.Name].Direction = param.ParameterDirection;
                                }
                            }

                            if (DTPCollection != null && DTPCollection.Count() > 0)
                            {
                                foreach (DataTableParameter dataTableParameter in DTPCollection)
                                {
                                    cmd.Parameters.AddWithValue(dataTableParameter.ParameterName, dataTableParameter.DataTable);
                                }
                            }

                            //await con.OpenAsync();

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                await Task.Run(() => adapter.Fill(resultSet));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DeltaCareLogger.Error(ex.Message);
                throw ex;
            }

            return resultSet;
        }

        //public int ExecuteNonQuery(string spName, IList<QueryParameterForSqlMapper> QPCollection=null, IList<DataTableParameter> DTPCollection=null)
        //{
        //    int result = 0;
        //    string outputParamName = string.Empty;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(spName))
        //        {
        //            using (SqlConnection con = _dbconnection.GetSqlConnection())
        //            {
        //                using (SqlCommand cmd = new SqlCommand(spName, con))
        //                {
        //                    cmd.CommandTimeout = commandTimeout
        //                    if (QPCollection != null && QPCollection.Count() > 0)
        //                    {
        //                        foreach (QueryParameterForSqlMapper param in QPCollection)
        //                        {
        //                            if (param.ParameterDirection == ParameterDirection.Output)
        //                            {
        //                                cmd.Parameters.Add(param.Name, SqlDbType.VarChar, 4);
        //                                cmd.Parameters[param.Name].Direction = param.ParameterDirection;
        //                                outputParamName = param.Name;
        //                            }
        //                            else
        //                            {
        //                                cmd.Parameters.AddWithValue(param.Name, param.Value);
        //                                cmd.Parameters[param.Name].Direction = param.ParameterDirection;
        //                            }
        //                        }
        //                    }
        //                    if (DTPCollection != null && DTPCollection.Count() > 0)
        //                    {
        //                        foreach (DataTableParameter dataTableParameter in DTPCollection)
        //                        {
        //                           // SqlParameter parameter = new SqlParameter();
        //                           // //The parameter for the SP must be of SqlDbType.Structured 
        //                           //parameter.TypeName = "[dbo].[kpiList]";
        //                           // parameter.ParameterName = dataTableParameter.ParameterName;
        //                           // parameter.SqlDbType = SqlDbType.Structured;
        //                           // parameter.Value = dataTableParameter.DataTable;
        //                           // cmd.Parameters.Add(parameter);
        //                              cmd.Parameters.AddWithValue(dataTableParameter.ParameterName, dataTableParameter.DataTable);
        //                        }
        //                    }
        //                    result = cmd.ExecuteNonQuery();
        //                    if (!string.IsNullOrEmpty(outputParamName))
        //                    {
        //                        result = Convert.ToInt32(cmd.Parameters[outputParamName].Value);
        //                    }

        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DeltaCareLogger.Error(ex.Message);
        //    }
        //    return result;
        //}

        public int ExecuteNonQuery(string spName, IList<QueryParameterForSqlMapper> QPCollection = null, IList<DataTableParameter> DTPCollection = null)
        {
            int result = 0;
            string outputParamName = "";

            try
            {
                using (SqlConnection conn = _dbconnection.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand(spName, conn)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = commandTimeout
                    };

                    if (QPCollection != null && QPCollection.Count > 0)
                    {
                        foreach (QueryParameterForSqlMapper param in QPCollection)
                        {
                            if (param.ParameterDirection == ParameterDirection.Output)
                            {
                                cmd.Parameters.Add(param.Name, SqlDbType.VarChar, 4);
                                cmd.Parameters[param.Name].Direction = param.ParameterDirection;
                                outputParamName = param.Name;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(param.Name, param.Value);
                                cmd.Parameters[param.Name].Direction = param.ParameterDirection;
                            }
                        }
                    }

                    if (DTPCollection != null && DTPCollection.Count > 0)
                    {
                        foreach (DataTableParameter dataTableParameter in DTPCollection)
                        {
                            cmd.Parameters.AddWithValue(dataTableParameter.ParameterName, dataTableParameter.DataTable);
                        }
                    }

                    result = cmd.ExecuteNonQuery();
                    if (outputParamName != "")
                    {
                        result = Convert.ToInt32(cmd.Parameters[outputParamName].Value);
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                DeltaCareLogger.Error(ex.Message);
                throw ex;
            }

            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(string spName, IList<QueryParameterForSqlMapper> QPCollection = null, IList<DataTableParameter> DTPCollection = null)
        {
            int result = 0;
            string outputParamName = "";

            try
            {
                using (SqlConnection conn = _dbconnection.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(spName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = commandTimeout;

                        if (QPCollection != null && QPCollection.Count > 0)
                        {
                            foreach (QueryParameterForSqlMapper param in QPCollection)
                            {
                                if (param.ParameterDirection == ParameterDirection.Output)
                                {
                                    cmd.Parameters.Add(param.Name, SqlDbType.VarChar, 4);
                                    cmd.Parameters[param.Name].Direction = param.ParameterDirection;
                                    outputParamName = param.Name;
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                                    cmd.Parameters[param.Name].Direction = param.ParameterDirection;
                                }
                            }
                        }

                        if (DTPCollection != null && DTPCollection.Count > 0)
                        {
                            foreach (DataTableParameter dataTableParameter in DTPCollection)
                            {
                                cmd.Parameters.AddWithValue(dataTableParameter.ParameterName, dataTableParameter.DataTable);
                            }
                        }

                        await conn.OpenAsync();
                        result = await cmd.ExecuteNonQueryAsync();

                        if (outputParamName != "")
                        {
                            result = Convert.ToInt32(cmd.Parameters[outputParamName].Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DeltaCareLogger.Error(ex.Message);
                throw ex;
            }

            return result;
        }

        public IEnumerable<T> ExecuteQuery<T>(string spName, IList<QueryParameterForSqlMapper> QPCollection)
        {
            IEnumerable<T> resultSet = null;
            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            DynamicParameters dynamicParameter = ConvertToDynamicParameters(QPCollection);
                            resultSet = con.Query<T>(spName, dynamicParameter, null, true, commandTimeout, CommandType.StoredProcedure).AsEnumerable();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DeltaCareLogger.Error(ex.Message);
                throw ex;
            }

            return resultSet;
        }
        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string spName, IList<QueryParameterForSqlMapper> QPCollection)
        {
            IEnumerable<T> resultSet = null;
            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            DynamicParameters dynamicParameter = ConvertToDynamicParameters(QPCollection);
                            resultSet = await con.QueryAsync<T>(spName, dynamicParameter, null, commandTimeout, CommandType.StoredProcedure);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DeltaCareLogger.Error(ex.Message);
                throw ex;
            }
            return resultSet;
        }

        public dynamic FetchRecordSet(string spName, IList<QueryParameterForSqlMapper> QPCollection)
        {
            dynamic resultSet = null;

            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            DynamicParameters dynamicParameter = ConvertToDynamicParameters(QPCollection);
                            resultSet = con.Query(spName, dynamicParameter, null, true, commandTimeout, CommandType.StoredProcedure).SingleOrDefault();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DeltaCareLogger.Error(ex.Message);
                throw ex;
            }
            return resultSet;
        }
        public IList<dynamic> FetchMultipleRecordSet(string spName, IList<QueryParameterForSqlMapper> QPCollection)
        {
            IList<dynamic> dataCollection = null;
            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            DynamicParameters dynamicParameter = ConvertToDynamicParameters(QPCollection);

                            SqlMapper.GridReader resultSet = con.QueryMultiple(spName, dynamicParameter, null, commandTimeout, commandType: CommandType.StoredProcedure);
                            dataCollection = new List<dynamic>();
                            while (!resultSet.IsConsumed)
                            {
                                dataCollection.Add(resultSet.Read());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DeltaCareLogger.Error(ex.Message);
                throw ex;

            }
            return dataCollection;
        }
        private DynamicParameters ConvertToDynamicParameters(IList<QueryParameterForSqlMapper> QPCollection)
        {
            DynamicParameters dynamicParameter = null;
            try
            {
                if (QPCollection != null && QPCollection.Count > 0)
                {
                    dynamicParameter = new DynamicParameters();
                    foreach (QueryParameterForSqlMapper parameter in QPCollection)
                    {
                        dynamicParameter.Add(parameter.Name, parameter.Value, parameter.DbType, parameter.ParameterDirection);
                    }
                }
            }
            catch (Exception ex)
            {
                DeltaCareLogger.Error(ex.Message);
                throw ex;
            }
            return dynamicParameter;
        }

        public async Task<int> ExecuteDataTable(string spName, DataTable dt, string tableType)
        {
            try
            {
                using (SqlConnection con = _dbconnection.GetSqlConnection())
                {
                    return await con.ExecuteAsync(spName, new { tableValue = dt.AsTableValuedParameter(tableType) },
                       commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<long> ExecuteDataTablePlus(string spName, DataTable dt, string tableType)
        {
            try
            {
                using (SqlConnection con = _dbconnection.GetSqlConnection())
                {
                    return await con.ExecuteAsync(spName, new { tableValue = dt.AsTableValuedParameter(tableType) },
                       commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
