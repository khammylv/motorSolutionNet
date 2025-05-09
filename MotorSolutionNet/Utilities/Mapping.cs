using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Services
{
    public class Mapping
    {
        public dynamic GenericMapping(DataRow row)
        {
            dynamic user = new ExpandoObject();
            var dict = (IDictionary<string, object>)user;

            foreach (DataColumn column in row.Table.Columns)
            {
                dict[column.ColumnName] = row[column];
            }

            return user;
        }

        public T MapToEntity<T>(DataRow row) where T : new()
        {
            T entity = new T();
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                string columnName = prop.Name;

                if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
                {
                    try
                    {
                        var value = Convert.ChangeType(row[columnName], prop.PropertyType);
                        prop.SetValue(entity, value);
                    }
                    catch
                    {
                        
                        prop.SetValue(entity, row[columnName]);
                    }
                }
            }

            return entity;
        }


        public  Dictionary<string, object> ToSqlParameters<T>(T entity)
        {
            var parameters = new Dictionary<string, object>();

            foreach (var property in typeof(T).GetProperties())
            {
                var paramName = $"@p_{property.Name}"; 
                parameters.Add(paramName, property.GetValue(entity));
            }

            return parameters;
        }
    }
}