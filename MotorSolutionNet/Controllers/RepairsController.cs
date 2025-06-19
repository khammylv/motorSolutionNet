using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;

namespace MotorSolutionNet.Controllers
{
    [RoutePrefix("api")]
    public class RepairsController : ApiController
    {
        private readonly RepairService _repairService;
        

        public RepairsController()
        {
            _repairService = new RepairService();
            
        }

        [HttpPost]
        [Route("repair-email")]
        public IHttpActionResult TestEmail([FromBody] EmailSend emailSend)
        {
            try
            {
                EmailSendData emailSendData = new EmailSendData();
                emailSendData.SendEmail(emailSend);
                return Ok("✅ Correo enviado exitosamente.");
            }
            catch (SmtpException smtpEx)
            {
                System.Diagnostics.Debug.WriteLine("Error SMTP: " + smtpEx.Message);
                return Content(HttpStatusCode.InternalServerError, $"❌ Error SMTP al enviar el correo: {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, $"❌ Error al enviar el correo: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("repair")]
        public IHttpActionResult AddRepair([FromBody] RepairsDTO repair)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairService.AddRepair(repair);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparacion agregada") : Content(HttpStatusCode.Conflict, "❌ Error al agregar reparacion AQUI");
            }, "❌ Error al agregar reparacion");
        }
        [HttpGet]
        [Route("repair/status-summary/{id:int}")]
        public IHttpActionResult GetRepairStatusSummary(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var statusSummary = _repairService.GetRepairStatusSummary(id);

                return Ok(statusSummary);
            }, "❌ Error al obtener status");

        }
        [HttpPost]
        [Route("repair-details")]
        public IHttpActionResult AddDetailsRepair([FromBody] RepairDetails repair)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairService.AddDetailRepair(repair);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparacion agregada") : Content(HttpStatusCode.Conflict, "❌ Error al agregar reparacion");
            }, "❌ Error al agregar reparacion");
        }
        [HttpPut]
        [Route("repair-details")]
        public IHttpActionResult UptdateDetailsRepair([FromBody] RepairDetails repair)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairService.UpdateDetailRepair(repair);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparacion editada") : Content(HttpStatusCode.Conflict, "❌ Error al agregar reparacion");
            }, "❌ Error al editar reparacion");
        }
        [HttpPut]
        [Route("repair")]
        public IHttpActionResult UpdateRepair([FromBody] RepairsDTO repair)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairService.UpdateRepair(repair);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparacion editada") : Content(HttpStatusCode.Conflict, "❌ Error al editar reparacion");
            }, "❌ Error al editar reparacion");
        }


        [HttpGet]
        [Route("repair/{id:int}")]
        public IHttpActionResult GetRepair(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repair = _repairService.GetRepair(id);
                if (repair == null)
                    return NotFound();

                return Ok(repair);
            }, "❌ Error al obtener reparacion");

        }

        [HttpPatch]
        [Route("repair")]
        public IHttpActionResult DoneRepair([FromBody] RepairsDTO repairs)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairService.DoneRepair(repairs);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparacion Terminada") : Content(HttpStatusCode.Conflict, "❌ Error al terminar la fecha de la reparación.");
            }, "❌ Error al terminar la fecha de la reparación.");
        }
        [HttpPatch]
        [Route("repair/status/{id:int}")]
        public IHttpActionResult UpdateStatus(int id, [FromBody] Repairs repairs)
        {
            System.Diagnostics.Debug.WriteLine("status:" + repairs.RepairStatus);
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairService.UpdateStatus(id, repairs.RepairStatus);
                return ok ? Content(HttpStatusCode.OK, "✅ Fecha de la reparacion actualizada") : Content(HttpStatusCode.Conflict, "❌ Error al actualizar status.");
            }, "❌ Error al actualizar status.");
        }

        [HttpDelete]
        [Route("repair/{id:int}")]
        public IHttpActionResult DeleteRepair(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairService.DeleteRepair(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparación eliminado") : Content(HttpStatusCode.Conflict, "❌ Error al eliminar");

            }, "❌ Error al eliminar.");
        }
        [HttpDelete]
        [Route("repair-details/{id:int}")]
        public IHttpActionResult DeleteRepairDetails(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairService.DeleteRepairDetails(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparación eliminado") : Content(HttpStatusCode.Conflict, "❌ Error al eliminar");

            }, "❌ Error al eliminar.");
        }


        [HttpGet]
        [Route("repair/client/{id:int}")]
        public IHttpActionResult GetRepairClient(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repairs =_repairService.GetRepairsByClientId(id, pageIndex, pageSize);
                
                return Ok(repairs);
            }, "Ocurrió un error al obtener las reparaciones.");

        }
        [HttpGet]
        [Route("repair-details/{id:int}")]
        public IHttpActionResult GetRepairDetails(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repairsDetails = _repairService.GetRepairsDetails(id);
                
                return Ok(repairsDetails);
            }, "Ocurrió un error al obtener las reparaciones.");

        }
        [HttpGet]
        [Route("repair-detail/{id:int}")]
        public IHttpActionResult GetRepairDetail(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repairsDetails = _repairService.GetRepairDetailById(id);

                return Ok(repairsDetails);
            }, "Ocurrió un error al obtener las reparaciones.");

        }
        [HttpGet]
        [Route("repair-dto/{id:int}")]
        public IHttpActionResult GetRepairDTO(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repairDTO = _repairService.GetRepairDTO(id);
                return Ok(repairDTO);
            }, "Ocurrió un error al obtener las reparaciones.");

        }

        [HttpGet]
        [Route("repair/company/{id:int}")]
        public IHttpActionResult GetRepairCompany(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repairs = _repairService.GetRepairsByCompanyCode(id, pageIndex, pageSize);
                
                return Ok(repairs);
            }, "Ocurrió un error al obtener las reparaciones.");

        }

        [HttpGet]
        [Route("repair/vehicle/{id:int}")]
        public IHttpActionResult GetRepairVehicle(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repairs = _repairService.GetRepairsByVehicle(id, pageIndex, pageSize);
               
                return Ok(repairs);
            }, "Ocurrió un error al obtener las reparaciones.");

        }

        [HttpGet]
        [Route("repair/enum-status")]
        public IHttpActionResult GetRepairStatus()
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var status = _repairService.GetStatusOptions();
                return Ok(status);
            }, "Ocurrió un error al obtener los status del cliente.");
        }


    }
}