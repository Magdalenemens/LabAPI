using DeltaCare.Common;
using System.Data.SqlClient;

namespace DeltaCare.DAL
{
    public sealed class DbConnection
    {
        //private static DbConnection instance = null;
        //private static readonly object padlock = new object();
        private static readonly Lazy<DbConnection> _instance = new Lazy<DbConnection>();
        public DbConnection()
        {

        }
        // read how to handle asynchorise connection on singleton class
        public static DbConnection Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        //public static DbConnection Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            lock (padlock)
        //            {
        //                if (instance == null)
        //                {
        //                    instance = new DbConnection();
        //                }
        //            }
        //        }
        //        return instance;
        //    }
        //}
        // this method is use to handle multiple connection string in app setting
        //public SqlConnection GetSqlConnection(ConnectionStringsCollection conName)
        //{

        //    SqlConnection sql = null;
        //    try
        //    {
        //        string constring = Convert.ToString(ConfigurationSettings.connectionStrings.ToList().Where(x => x.key.Equals(conName)).Select(x => x.value).FirstOrDefault());
        //        if (!string.IsNullOrEmpty(constring))
        //        {
        //            sql = new SqlConnection(constring);
        //            sql.Open();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return sql;
        //}
        public SqlConnection GetSqlConnection()
        {

            SqlConnection sql = null;
            try
            {
                string constring = Convert.ToString(DbConnectionString.ConnectionString);
                if (!string.IsNullOrEmpty(constring))
                {
                    sql = new SqlConnection(constring);
                    sql.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sql;
        }
    }
}
