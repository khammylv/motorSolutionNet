using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Utilities;

namespace MotorSolutionNet.Services
{
    public class CompanyService
    {
        private readonly CompanyData _companyData;
        private readonly PaginationHelper _companyPagination;
        public CompanyService()
        {
            _companyData = new CompanyData();
            _companyPagination = new PaginationHelper();
        }
        public Company GetCompanyVal(int? companyCode = null, string companyEmail = null, string nit = null)
        {
            return _companyData.GetCompanyVal(companyCode, companyEmail, nit);
        }
        public bool AddCompany(Company company)
        {
            bool isValid = ConfigManager.IsObjectValid(company);
            if (!isValid)
            {
                throw new Exception("La ccompañia tiene campos Vacios");

            }
            return _companyData.AddCompany(company);
        }
        public bool UpdateCompany(CompanyDTO company)
        {
            bool isValid = ConfigManager.IsObjectValid(company);
            if (!isValid)
            {
                throw new Exception("La ccompañia tiene campos Vacios");

            }
            return _companyData.UpdateCompany(company);
        }
        public bool DeleteCompany(int companyCode)
        {
            return _companyData.DeleteCompany(companyCode);
        }
        public CompanyDTO GetCompany(int? companyCode = null)
        {
            return _companyData.GetCompany(companyCode);
        }

    }
}