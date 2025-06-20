using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MotorSolutionNet.DTO;
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
        private readonly AuthService _userService;

        public CompanyData()
        {
            _connection = new ConectionDB();
            _companyMapping = new Mapping();
            _userService = new AuthService();
        }
        public List<Company> ListCompanies()
        {
            var table = _connection.ExecuteQuery(ConfigurationVarCompany.ListCompany);

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
            string hashedPassword = _userService.HashPassword(company.CompanyPassword);
            string hashedPasswordEmail = EncryptionHelper.Encrypt(company.PasswordEmail);
            parameters["@p_CompanyPassword"] = hashedPassword;
            parameters["@p_PasswordEmail"] = hashedPasswordEmail;
            return _connection.ExecuteProcedure(ConfigurationVarCompany.AddCompany, parameters);
        }
        public bool UpdateCompany(CompanyDTO company) {
            var parameters = _companyMapping.ToSqlParameters(company);
             return _connection.ExecuteProcedure(ConfigurationVarCompany.UpdateCompany, parameters);
        }
        public CompanyDTO GetCompany(int? companyCode = null) {
            var parameterObject = new
            {
                CompanyCode = companyCode
            };
            var parameters = _companyMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarCompany.GetCompanByID, parameters);

            return table?.Rows.Count > 0 ? _companyMapping.MapToEntity<CompanyDTO>(table.Rows[0]) : null;
        }
        public Company GetCompanyVal(int? companyCode = null, string companyEmail = null, string nit = null)
        {
            var parameterObject = new
            {
                CompanyCode = companyCode,
                CompanyEmail = companyEmail,
                Nit = nit
            };
            var parameters = _companyMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarCompany.GetCompany, parameters);

            return table?.Rows.Count > 0 ? _companyMapping.MapToEntity<Company>(table.Rows[0]) : null;
        }
        public bool DeleteCompany(int companyCode)
        {
            var parameterObject = new
            {
                CompanyCode = companyCode
            };
            var parameters = _companyMapping.ToSqlParameters(parameterObject);
            return _connection.ExecuteProcedure(ConfigurationVarCompany.DeleteCompany, parameters);
        }

        public CompanySummaryResult GetCompanySummary(int companyCode)
        {
            var parameterObject = new
            {
                CompanyCode = companyCode
            };

            var parameters = _companyMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarCompany.SumaryCompany, parameters);

            var result = new CompanySummaryResult();

            if (table?.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                result.Resumen["total_users"] = row["total_users"] != DBNull.Value ? Convert.ToInt32(row["total_users"]) : 0;
                result.Resumen["total_clients"] = row["total_clients"] != DBNull.Value ? Convert.ToInt32(row["total_clients"]) : 0;
                result.Resumen["total_vehicles"] = row["total_vehicles"] != DBNull.Value ? Convert.ToInt32(row["total_vehicles"]) : 0;
                result.Resumen["total_bills"] = row["total_bills"] != DBNull.Value ? Convert.ToInt32(row["total_bills"]) : 0;
            }

            return result;
        }

    }
}