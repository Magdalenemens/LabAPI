using DeltaCare.Entity.Model;
using System.Data;

namespace DeltaCare.DAL
{
    public static class ParameterGenerator
    {
        //public static IList<QueryParameterForSqlMapper> CreateParameterList<T>(T obj)
        //{
        //    IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>();

        //    foreach (var property in typeof(T).GetProperties())
        //    {
        //        var parameter = new QueryParameterForSqlMapper
        //        {
        //            Name = property.Name,
        //            ParameterDirection = ParameterDirection.Input,
        //            Value = property.GetValue(obj),
        //            DbType = GetDbType(property.PropertyType)
        //        };

        //        parameterCollection.Add(parameter);
        //    }

        //    return parameterCollection;
        //}

        public static IList<QueryParameterForSqlMapper> CreateParameterList<T>(T obj)
        {
            IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>();

            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {
                // If T is a simple value type (e.g., int, string), create a single parameter
                parameterCollection.Add(new QueryParameterForSqlMapper
                {
                    Name = "Value",
                    ParameterDirection = ParameterDirection.Input,
                    Value = obj,
                    DbType = GetDbType(typeof(T))
                });
            }
            else
            {
                // Ignoring the parameters before insert.
                var parameters = new List<QueryParameterForSqlMapper>();
                var properties = typeof(T).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(IgnoreParameterAttribute)));

                // If T is a complex type, iterate through properties
                foreach (var property in properties)
                {
                    var parameter = new QueryParameterForSqlMapper
                    {
                        Name = property.Name,
                        ParameterDirection = ParameterDirection.Input,
                        Value = property.GetValue(obj),
                        DbType = GetDbType(property.PropertyType)
                    };

                    parameterCollection.Add(parameter);
                }
            }

            return parameterCollection;
        }

        public static IList<QueryParameterForSqlMapper> CreateParameterList<T>(T obj, string propertyName)
        {
            IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>();

            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {
                // If T is a simple value type (e.g., int, string), create a single parameter
                parameterCollection.Add(new QueryParameterForSqlMapper
                {
                    Name = propertyName,
                    ParameterDirection = ParameterDirection.Input,
                    Value = obj,
                    DbType = GetDbType(typeof(T))
                });
            }
            else
            {
                // If T is a complex type, iterate through properties
                foreach (var property in typeof(T).GetProperties())
                {
                    var parameter = new QueryParameterForSqlMapper
                    {
                        Name = property.Name,
                        ParameterDirection = ParameterDirection.Input,
                        Value = property.GetValue(obj),
                        DbType = GetDbType(property.PropertyType)
                    };

                    parameterCollection.Add(parameter);
                }
            }

            return parameterCollection;
        }

        public static IList<QueryParameterForSqlMapper> CreateParameterList<T1, T2>(T1 obj1, T2 obj2, string propertyName1, string propertyName2)
        {
            IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>();

            if (typeof(T1).IsValueType || typeof(T1) == typeof(string))
            {
                // If T is a simple value type (e.g., int, string), create a single parameter
                parameterCollection.Add(new QueryParameterForSqlMapper
                {
                    Name = propertyName1,
                    ParameterDirection = ParameterDirection.Input,
                    Value = obj1,
                    DbType = GetDbType(typeof(T1))
                });
            }
            else
            {
                // If T is a complex type, iterate through properties
                foreach (var property in typeof(T1).GetProperties())
                {
                    var parameter = new QueryParameterForSqlMapper
                    {
                        Name = property.Name,
                        ParameterDirection = ParameterDirection.Input,
                        Value = property.GetValue(obj1),
                        DbType = GetDbType(property.PropertyType)
                    };

                    parameterCollection.Add(parameter);
                }

                // If T is a complex type, iterate through properties
                foreach (var property in typeof(T2).GetProperties())
                {
                    var parameter = new QueryParameterForSqlMapper
                    {
                        Name = property.Name,
                        ParameterDirection = ParameterDirection.Input,
                        Value = property.GetValue(obj2),
                        DbType = GetDbType(property.PropertyType)
                    };

                    parameterCollection.Add(parameter);
                }
            }
            if (typeof(T2).IsValueType || typeof(T2) == typeof(string))
            {
                // If T is a simple value type (e.g., int, string), create a single parameter
                parameterCollection.Add(new QueryParameterForSqlMapper
                {
                    Name = propertyName2,
                    ParameterDirection = ParameterDirection.Input,
                    Value = obj2,
                    DbType = GetDbType(typeof(T2))
                });
            }

            return parameterCollection;
        }

        private static DbType GetDbType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return DbType.String;
                case TypeCode.Int32:
                    return DbType.Int32;
                case TypeCode.Boolean:
                    return DbType.Boolean;
                case TypeCode.Decimal:
                    return DbType.Decimal;
                // Add more cases for other types as needed

                // Default to DbType.String if no specific mapping is found
                default:
                    return DbType.String;
            }
        }

        //public static IList<QueryParameterForSqlMapper> CreateParameterList<T1, T2>(T1 obj1, T2 obj2)
        //{
        //    IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>();

        //    foreach (var property in typeof(T1).GetProperties())
        //    {
        //        var parameter = new QueryParameterForSqlMapper
        //        {
        //            Name = property.Name,
        //            ParameterDirection = ParameterDirection.Input,
        //            Value = property.GetValue(obj1),
        //            DbType = GetDbType(property.PropertyType)
        //        };

        //        parameterCollection.Add(parameter);

        //    }

        //    foreach (var property in typeof(T2).GetProperties())
        //    {
        //        var parameter = new QueryParameterForSqlMapper
        //        {
        //            Name = property.Name,
        //            ParameterDirection = ParameterDirection.Input,
        //            Value = property.GetValue(obj2),
        //            DbType = GetDbType(property.PropertyType)
        //        };
        //        parameterCollection.Add(parameter);
        //    }
        //    return parameterCollection;
        //}


    }

}
