using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;
using MotorSolutionNet.Utilities;
using MySqlX.XDevAPI;
using Client = MotorSolutionNet.Models.Client;

namespace MotorSolutionNet.Data
{
    public class ClientData
    {
        private readonly ConectionDB _connection;
        private readonly Mapping _clientMapping;

        public ClientData()
        {
            _connection = new ConectionDB();
            _clientMapping = new Mapping();
        }

        public List<Client> ListClients()
        {
            DataTable table = _connection.ExecuteQuery(ConfigurationVar.ListClient);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _clientMapping.MapToEntity<Client>(row))
                            .ToList();

            }

            return new List<Client>();
        }

        public bool AddClient(Client client)
        { 
            var parameters = _clientMapping.ToSqlParameters(client);
            return _connection.ExecuteProcedure(ConfigurationVar.AddClient, parameters);
        }

 
        public bool UpdateClient(Client client)
        {
            var parameters = _clientMapping.ToSqlParameters(client);
            return _connection.ExecuteProcedure(ConfigurationVar.UpdateClient, parameters);
        }
        public bool DeleteClient(int clientId)
        {
            var parameterObject = new
            {
                ClientId = clientId,
            };
            var parameters = _clientMapping.ToSqlParameters(parameterObject);
            return _connection.ExecuteProcedure(ConfigurationVar.DeleteClient, parameters);
        }
        public Client GetClient(int clientId)
        {
            var parameterObject = new
            {
                ClientId = clientId,
              
            };
            var parameters = _clientMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetClient, parameters);
          
            return table?.Rows.Count > 0 ? _clientMapping.MapToEntity<Client>(table.Rows[0]): null;
        }
        public Client GetClientValidation(int? clientId = null, string email = null, string identification = null, int? companyCode = null)
        {
            var parameterObject = new
            {
                ClientId = clientId,
                Email = email,
                Identification = identification,
                CompanyCode = companyCode
            };
            var parameters = _clientMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetClientValidation, parameters);
            return table?.Rows.Count > 0 ? _clientMapping.MapToEntity<Client>(table.Rows[0]) : null;
        }
        public List<Object> GetClientByCompany(int companyCode)
        {
            var parameterObject = new
            {
                CompanyCode = companyCode
            };
            var parameters = _clientMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetClientByCompany, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _clientMapping.GenericMapping(row))
                            .ToList();
            }
            return new List<object>();
  
        }
    }
}