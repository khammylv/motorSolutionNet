using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;

namespace MotorSolutionNet.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly CompanyService _companyService;
       
        public CompanyController() {
            _companyService = new CompanyService(); 
            
        }

        /*[HttpGet]
        [Route("api/company")]
        public IHttpActionResult GetCompaniesList(int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var companies = _companyService.ListCompanies();
                var result = _companyPagination.Paginate(companies, pageIndex, pageSize);
                return Ok(result);
            }, "Ocurrió un error al obtener las compañias.");   
        }
        */
        [HttpPost]
        [Route("api/company")]
        public IHttpActionResult AddCompany([FromBody] Company company)
        {
             return ControllerHelper.ExecuteAction(this, () =>
            {
                var companyVal = _companyService.GetCompanyVal(companyEmail: company.CompanyEmail, nit: company.Nit);
                if (companyVal != null)
                    return BadRequest("Esta compañia ya existe.");

                bool ok = _companyService.AddCompany(company);
                return ok ? Content(HttpStatusCode.OK, "Compañia agregada") : Content(HttpStatusCode.Conflict, "Ocurrió un error al agregar la compañia. AQUI");

            }, "Ocurrió un error al agregar la compañia.");
        }

        [HttpPut]
        [Route("api/company")]
        public IHttpActionResult UpdateCompany( [FromBody] CompanyDTO company)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _companyService.UpdateCompany(company);
                return ok ? Content(HttpStatusCode.OK, "Compañia actualizada") : Content(HttpStatusCode.Conflict, "Ocurrió un error al actualizar la compañia.");

            }, "Ocurrió un error al actualizar la compañia.");
        }

        [HttpDelete]
        [Route("api/company/{id}")]
        public IHttpActionResult DeleteCompany(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _companyService.DeleteCompany(id);
                return ok ? Content(HttpStatusCode.OK, "Compañia eliminada") : Content(HttpStatusCode.Conflict, "Ocurrió un error al eliminar la compañia.");

            }, "Ocurrió un error al eliminar la compañia.");
        }

        [HttpGet]
        [Route("api/company/{id}")]
        public IHttpActionResult GetCompanyById(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var company = _companyService.GetCompany(id);
                if (company == null)
                    return NotFound();

                return Ok(company);
            }, "Ocurrió un error al encontrar la compañia.");

        }

        [HttpGet]
        [Route("api/company/sumary/{id}")]
        public IHttpActionResult GetCompanySumary(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var company = _companyService.GetCompanySummary(id);

                return Ok(company);
            }, "Ocurrió un error al encontrar la compañia.");

        }

    }
}