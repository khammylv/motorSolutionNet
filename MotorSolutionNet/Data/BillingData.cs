using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;
using MotorSolutionNet.Utilities;

namespace MotorSolutionNet.Data
{
    public class BillingData
    {
        private readonly ConectionDB _connection;
        private readonly Mapping _billingMapping;

        public BillingData()
        {
            _connection = new ConectionDB();
            _billingMapping = new Mapping();
        }

        public bool AddBilling(Billings billing)
        {
            var parameters = _billingMapping.ToSqlParameters(billing);
            return _connection.ExecuteProcedure(ConfigurationVar.AddBilling, parameters);
        }

        public bool UpdateBilling(Billings billing)
        {
            var parameters = _billingMapping.ToSqlParameters(billing);
            return _connection.ExecuteProcedure(ConfigurationVar.UpdateBilling, parameters);
        }

        public bool DeleteBilling(int billingId) {
            var parameterObject = new
            {
                BillingId = billingId,
            };
            var parameters = _billingMapping.ToSqlParameters(parameterObject);
            return _connection.ExecuteProcedure(ConfigurationVar.DeleteBilling, parameters);
        }

        public Billings GetBilling(int billingId)
        {
            var parameterObject = new
            {
                BillingId = billingId,
            };
            var parameters = _billingMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetBillingByID, parameters);
            return table?.Rows.Count > 0 ? _billingMapping.MapToEntity<Billings>(table.Rows[0]) : null;
        }

      
        public Billings GetFullBilling(int billingId)
        {
            var parameterObject = new
            {
                BillingId = billingId,
            };
            var parameters = _billingMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetFullBillingByID, parameters);
            return table?.Rows.Count > 0 ? _billingMapping.GenericMapping(table.Rows[0]) : null;
        }

        public List<Object> GetBillingsByCompany(int companyCode)
        {
            var parameterObject = new
            {
                CompanyCode = companyCode,
            };
            var parameters = _billingMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetBillingsByCompany, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _billingMapping.GenericMapping(row))
                            .ToList();
            }
            return new List<Object>();
        }
        public List<Object> GetBillingsByClient(int clientId)
        {
            var parameterObject = new
            {
                ClientId = clientId,
            };
            var parameters = _billingMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetBillingsByClient, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _billingMapping.GenericMapping(row))
                            .ToList();
            }
            return new List<Object>();
        }


    }
}