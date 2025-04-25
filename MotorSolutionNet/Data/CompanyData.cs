using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;
using MotorSolutionNet.Utilities;
using MySqlX.XDevAPI;

namespace MotorSolutionNet.Data
{
    public class CompanyData
    {
        private readonly ConectionDB _connection;
        private readonly Mapping _companyMapping;
        public CompanyData()
        {
            _connection = new ConectionDB();
            _companyMapping = new Mapping();
        }
        public List<Company> ListCompanies()
        {
            var table = _connection.ExecuteQuery(ConfigurationVar.ListCompany);

            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _companyMapping.MapToEntity<Company>(row))
                            .ToList();
            }

            return new List<Company>();
        }
           public bool AddCompany(Company company)
        {
            var parameters = _companyMapping.ToSqlParameters(company);
            return _connection.ExecuteProcedure(ConfigurationVar.AddCompany, parameters);
        }
        public bool UpdateCompany(Company company) {
            var parameters = _companyMapping.ToSqlParameters(company);
            return _connection.ExecuteProcedure(ConfigurationVar.UpdateCompany, parameters);
        }
        public Company GetCompany(int? companyCode = null) {
            var parameterObject = new
            {
                CompanyCode = companyCode
            };
            var parameters = _companyMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetCompany, parameters);

            return table?.Rows.Count > 0 ? _companyMapping.MapToEntity<Company>(table.Rows[0]) : null;
        }
        public bool DeleteCompany(int companyCode)
        {
            var parameterObject = new
            {
                CompanyCode = companyCode
            };
            var parameters = _companyMapping.ToSqlParameters(parameterObject);
            return _connection.ExecuteProcedure(ConfigurationVar.DeleteCompany, parameters);
        }

    }
}