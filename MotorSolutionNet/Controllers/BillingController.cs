using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;

namespace MotorSolutionNet.Controllers
{
    public class BillingController : ApiController
    {
        private readonly BillingService _billingService;
        

        public BillingController()
        {
            _billingService = new BillingService();
          
        }

        [HttpPost]
        [Route("api/billing")]
        public IHttpActionResult AddBilling([FromBody] BillingDTO billing)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var billingVal = _billingService.GetBillingByRepairId(billing.RepairId);
                System.Diagnostics.Debug.WriteLine("billingVal: " + billingVal);
                if (billingVal != null)
                    return BadRequest("Factura Existente para esta reparacion.");
                bool ok = _billingService.AddBilling(billing);
                return ok ? Content(HttpStatusCode.OK, "✅ Factura agregada") : Content(HttpStatusCode.Conflict, "❌ Error al agregar factura");
            }, "❌ Error al agregar factura");
        }

   

        [HttpGet]
        [Route("api/billing/{id}")]
        public IHttpActionResult GetBilling(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repair = _billingService.GetBilling(id);
                if (repair == null)
                    return NotFound();

                return Ok(repair);
            }, "❌ Error al obtener factura");

        }



        [HttpGet]
        [Route("api/billing/full/{id}")]
        public IHttpActionResult GetFullBilling(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repair = _billingService.GetFullBilling(id);
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
                var billing = _billingService.GetBillingsByClient(id, pageIndex, pageSize);
                
                return Ok(billing);
            }, "Ocurrió un error al obtener las reparaciones.");

        }

        [HttpGet]
        [Route("api/billing/company/{id}")]
        public IHttpActionResult GetBillingCompany(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var billing = _billingService.GetBillingsByCompany(id, pageIndex, pageSize);
               
                return Ok(billing);
            }, "Ocurrió un error al obtener las reparaciones.");

        }
    }
}