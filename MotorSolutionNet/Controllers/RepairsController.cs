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
    public class RepairsController : ApiController
    {
        private readonly RepairData _repairData;
        private readonly PaginationHelper _repairPagination;

        public RepairsController()
        {
            _repairData = new RepairData();
            _repairPagination = new PaginationHelper();
        }



        [HttpPost]
        [Route("api/repair")]
        public IHttpActionResult AddRepair([FromBody] Repairs repair)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairData.AddRepair(repair);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparacion agregada") : Content(HttpStatusCode.Conflict, "❌ Error al agregar reparacion");
            }, "❌ Error al agregar reparacion");
        }

        [HttpPut]
        [Route("api/repair")]
        public IHttpActionResult UpdateRepair([FromBody] Repairs repair)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairData.UpdateRepair(repair);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparacion editada") : Content(HttpStatusCode.Conflict, "❌ Error al editar reparacion");
            }, "❌ Error al editar reparacion");
        }


        [HttpGet]
        [Route("api/repair/{id}")]
        public IHttpActionResult GetRepair(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var repair = _repairData.GetRepair(id);
                if (repair == null)
                    return NotFound();

                return Ok(repair);
            }, "❌ Error al obtener reparacion");

        }

        [HttpPatch]
        [Route("api/repair/{id}")]
        public IHttpActionResult UpdateDepartureDate(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairData.UpdateDepartureDate(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Fecha de la reparacion actualizada") : Content(HttpStatusCode.Conflict, "❌ Error al actualizar la fecha de la reparación.");
            }, "❌ Error al actualizar la fecha de la reparación.");
        }

        [HttpDelete]
        [Route("api/repair/{id}")]
        public IHttpActionResult DeleteRepair(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _repairData.DeleteRepair(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Reparación eliminado") : Content(HttpStatusCode.Conflict, "❌ Error al eliminar");

            }, "❌ Error al eliminar.");
        }

        [HttpGet]
        [Route("api/repair/client/{id}")]
        public IHttpActionResult GetRepairClient(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicles =_repairData.GetRepairsByClientId(id);
                var result = _repairPagination.Paginate(vehicles, pageIndex, pageSize);
                return Ok(result);
            }, "Ocurrió un error al obtener las reparaciones.");

        }

        [HttpGet]
        [Route("api/repair/company/{id}")]
        public IHttpActionResult GetRepairCompany(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicles = _repairData.GetRepairsByCompanyCode(id);
                var result = _repairPagination.Paginate(vehicles, pageIndex, pageSize);
                return Ok(result);
            }, "Ocurrió un error al obtener las reparaciones.");

        }

        [HttpGet]
        [Route("api/repair/vehicle/{id}")]
        public IHttpActionResult GetRepairVehicle(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicles = _repairData.GetRepairsByVehicle(id);
                var result = _repairPagination.Paginate(vehicles, pageIndex, pageSize);
                return Ok(result);
            }, "Ocurrió un error al obtener las reparaciones.");

        }


    }
}