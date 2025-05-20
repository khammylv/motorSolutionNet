using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorSolutionNet.Data;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;

namespace MotorSolutionNet.Controllers
{
    public class BillingController : ApiController
    {
        private readonly BillingData _billingData;
        private readonly PaginationHelper _billingPagination;

        public BillingController()
        {
            _billingData = new BillingData();
            _billingPagination = new PaginationHelper();
        }

        [HttpPost]
        [Route("api/billing")]
        public IHttpActionResult AddBilling([FromBody] Billings billing)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _billingData.AddBilling(billing);
                return ok ? Content(HttpStatusCode.OK, "✅ Factura agregada") : Content(HttpStatusCode.Conflict, "❌ Error al agregar factura");
            }, "❌ Error al agregar factura");
        }

        [HttpGet]
        [Route("api/billing/{id}")]
        public IHttpActionResult GetBilling(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repair = _billingData.GetBilling(id);
                if (repair == null)
                    return NotFound();

                return Ok(repair);
            }, "❌ Error al obtener factura");

        }


        [HttpDelete]
        [Route("api/billing/{id}")]
        public IHttpActionResult DeleteBilling(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _billingData.DeleteBilling(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Factura eliminado") : Content(HttpStatusCode.Conflict, "❌ Error al eliminar");

            }, "❌ Error al eliminar.");
        }

        [HttpGet]
        [Route("api/billing/full/{id}")]
        public IHttpActionResult GetFullBilling(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repair = _billingData.GetFullBilling(id);
                if (repair == null)
                    return NotFound();

                return Ok(repair);
            }, "❌ Error al obtener factura");

        }

        [HttpGet]
        [Route("api/billing/client/{id}")]
        public IHttpActionResult GetBillingClient(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var billing = _billingData.GetBillingsByClient(id);
                var result = _billingPagination.Paginate(billing, pageIndex, pageSize);
                return Ok(result);
            }, "Ocurrió un error al obtener las reparaciones.");

        }

        [HttpGet]
        [Route("api/billing/company/{id}")]
        public IHttpActionResult GetBillingCompany(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicles = _billingData.GetBillingsByCompany(id);
                var result = _billingPagination.Paginate(vehicles, pageIndex, pageSize);
                return Ok(result);
            }, "Ocurrió un error al obtener las reparaciones.");

        }
    }
}