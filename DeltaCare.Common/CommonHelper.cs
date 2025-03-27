using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace DeltaCare.Common
{
    public static class CommonHelper
    {


        public static List<T> ConvertDataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert List<T> to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            foreach (PropertyDescriptor property in properties)
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);

            foreach (T item in list)
            {
                DataRow row = dataTable.NewRow();
                foreach (PropertyDescriptor property in properties)
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }


    }
}

