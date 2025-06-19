using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;
using System.Dynamic;
using MotorSolutionNet.Utilities;
using MotorSolutionNet.DTO;
using System.Web.DynamicData;

namespace MotorSolutionNet.Data
{
    public class UserData
    {
        private readonly ConectionDB _connection;
        private readonly AuthService _userService;
        private readonly Mapping _userMapping;

        public UserData()
        {
            _connection = new ConectionDB();
            _userService = new AuthService();
            _userMapping = new Mapping();
        }

        // Listar usuarios
       /* public List<User> ListUsers()
        {
            DataTable table = _connection.ExecuteQuery(ConfigurationVarUser.ListUser);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _userMapping.MapToEntity<User>(row))
                            .ToList();

            }

            return new List<User>();
        }
       */
        // Agregar un usuario
        public bool AddUser(User user)
        {
            var parameters = _userMapping.ToSqlParameters(user);
            string hashedPassword = _userService.HashPassword(user.UserPassword);
            parameters["@p_UserPassword"] = hashedPassword;
            return _connection.ExecuteProcedure(ConfigurationVarUser.AddUser, parameters);
        }

        // Actualizar un usuario
        public bool UpdateUser(UserDTO user)
        {
            var parameters = _userMapping.ToSqlParameters(user);
            return _connection.ExecuteProcedure(ConfigurationVarUser.UpdateUser, parameters);
        }

        // Obtener un usuario por filtros
        public User GetUser(int? userId = null, string userEmail = null, string userIdentification = null, int? companyCode = null)
        {
            var parameterObject = new
            {
                UserId = userId,
                UserEmail = userEmail,
                UserIdentification = userIdentification,
                CompanyCode = companyCode
            };
            var parameters = _userMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarUser.GetUser, parameters);
            return table?.Rows.Count > 0 ? _userMapping.MapToEntity<User>(table.Rows[0]) : null;
        }

        public User GetUserById(int? userId)
        {
            var parameterObject = new
            {
                UserId = userId
            };
            var parameters = _userMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarUser.GetUserById, parameters);
            return table?.Rows.Count > 0 ? _userMapping.MapToEntity<User>(table.Rows[0]) : null;
        }

        public List<Object> GetUserByCompany(int companyCode)
        {   
            var parameterObject = new
            {
                CompanyCode = companyCode
            };
            var parameters = _userMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarUser.GetUsersByCompany, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _userMapping.GenericMapping(row))
                            .ToList();
            }
            return new List<Object>();
        }
      

        // Eliminar un usuario
        public bool DeleteUser(int userId)
        {
            var parameterObject = new
            {
                UserId = userId,
            };
            var parameters = _userMapping.ToSqlParameters(parameterObject);
            return _connection.ExecuteProcedure(ConfigurationVarUser.DeleteUser, parameters);
        }

        public RoleOptionsResult GetRoleOptions()
        {
            DataTable table = _connection.ExecuteQuery(ConfigurationVarUser.UserEnumRole);
            var result = new RoleOptionsResult { Roles = new Dictionary<string, string>() };
           // System.Diagnostics.Debug.WriteLine("aqui:");
            System.Diagnostics.Debug.WriteLine("aqui:" + table);
            
            if (table?.Rows.Count > 0)
            {
              /*  foreach (DataColumn col in table.Columns)
                {
                    System.Diagnostics.Debug.WriteLine("column name:" + col.ColumnName); 
                }*/
                string enumDefinition = table.Rows[0]["enum_definition"].ToString();
                string valuesString = enumDefinition.Substring(5, enumDefinition.Length - 6);
                string[] values = valuesString.Split(',');
                foreach (var val in values)
                {
                    string cleanValue = val.Trim('\'', ' ');
                    result.Roles[cleanValue] = cleanValue;
                }

            }
            return result;
        }

    }
}