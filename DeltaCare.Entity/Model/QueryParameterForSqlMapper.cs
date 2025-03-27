using System.Data;

namespace DeltaCare.Entity.Model
{
    public class QueryParameterForSqlMapper
    {
        public string Name { get; set; }
        public ParameterDirection ParameterDirection { get; set; }
        public object Value { get; set; }
        public DbType DbType { get; set; }
    }
}
