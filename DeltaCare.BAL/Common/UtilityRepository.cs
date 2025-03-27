using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Common
{
    public class UtilityRepository : IUtilityRepository
    {
        private readonly IDataRepository _datarepository;
        public UtilityRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }

        public async Task<int> GetMaxValueAsync(string tableName, string columnName)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentException("Table name and column name must not be null or empty.");
            }

            // Create a dictionary for parameters
            var parameters = new List<QueryParameterForSqlMapper>
    {
        new QueryParameterForSqlMapper
        {
            Name = "TableName",
            Value = tableName,

        },
        new QueryParameterForSqlMapper
        {
            Name = "ColumnName",
            Value = columnName,

        }
    };

            // Execute the stored procedure and retrieve results
            var result = await _datarepository.ExecuteQueryAsync<int>(
                SPConstant.Sp_GetMaxValue,
                parameters
            );

            // Return the maximum value or default to 0 if no results
            return result.FirstOrDefault();
        }


    }
}

