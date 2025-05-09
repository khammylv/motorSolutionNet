using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorSolutionNet.Data;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;

namespace MotorSolutionNet.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly CompanyData _companyData;
        private readonly PaginationHelper _companyPagination;
        public CompanyController() {
            _companyData = new CompanyData(); 
            _companyPagination = new PaginationHelper();
        }

        [HttpGet]
        [Route("api/company")]
        public IHttpActionResult GetCompaniesList(int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var companies = _companyData.ListCompanies();
                var result = _companyPagination.Paginate(companies, pageIndex, pageSize);
                return Ok(result);
            }, "Ocurrió un error al obtener las compañias.");   
        }

        [HttpPost]
        [Route("api/company")]
        public IHttpActionResult AddCompany([FromBody] Company company)
        {
             return ControllerHelper.ExecuteAction(this, () =>
            {
                var companyVal = _companyData.GetCompanyVal(companyEmail: company.CompanyEmail, nit: company.Nit);
                if (companyVal != null)
                    return BadRequest("Esta compañia ya existe.");

                bool ok = _companyData.AddCompany(company);
                return ok ? Content(HttpStatusCode.OK, "Compañia agregada") : Content(HttpStatusCode.Conflict, "Ocurrió un error al agregar la compañia.");

            }, "Ocurrió un error al agregar la compañia.");
        }

        [HttpPut]
        [Route("api/company")]
        public IHttpActionResult UpdateCompany( [FromBody] Company company)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _companyData.UpdateCompany(company);
                return ok ? Content(HttpStatusCode.OK, "Compañia actualizada") : Content(HttpStatusCode.Conflict, "Ocurrió un error al actualizar la compañia.");

            }, "Ocurrió un error al actualizar la compañia.");
        }

        [HttpDelete]
        [Route("api/company/{id}")]
        public IHttpActionResult DeleteCompany(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _companyData.DeleteCompany(id);
                return ok ? Content(HttpStatusCode.OK, "Compañia eliminada") : Content(HttpStatusCode.Conflict, "Ocurrió un error al eliminar la compañia.");

            }, "Ocurrió un error al eliminar la compañia.");
        }

        [HttpGet]
        [Route("api/company/{id}")]
        public IHttpActionResult GetCompanyById(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var company = _companyData.GetCompany(id);
                if (company == null)
                    return NotFound();

                return Ok(company);
            }, "Ocurrió un error al encontrar la compañia.");

        }

    }
}