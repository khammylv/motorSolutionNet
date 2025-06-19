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
    public class VehicleController : ApiController
    {
        private readonly VehicleService _vehicleService;
        private readonly PaginationHelper _vehiclePagination;

        public VehicleController()
        {
            _vehicleService = new VehicleService();
            
        }

        [HttpPost]
        [Route("api/vehicle")]
        public IHttpActionResult AddVehicle([FromBody] Vehicle vehicle)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicleVal = _vehicleService.GetVehicleValidation(plate: vehicle.Plate, companyCode: vehicle.CompanyCode);
                if (vehicleVal != null)
                    return BadRequest("Vehiculo Existente.");
                bool ok = _vehicleService.AddVehicle(vehicle);
                return ok ? Content(HttpStatusCode.OK, "✅ Vehiculo agregado") : Content(HttpStatusCode.Conflict, "❌ Error al agregar vehiculo");

            }, "❌ Error al agregar vehiculo");
        }

        [HttpPut]
        [Route("api/vehicle")]
        public IHttpActionResult UpdateVehicle([FromBody] VehicleDTO vehicle)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _vehicleService.UpdateVehicle(vehicle);
                return ok ? Content(HttpStatusCode.OK, "✅ Vehiculo actualizado") : Content(HttpStatusCode.Conflict, "❌ Error al actualizar vehiculo.");

            }, "❌ Error al actualizar vehiculo.");

        }



        [HttpGet]
        [Route("api/vehicle/company/{id}")]
        public IHttpActionResult GetVehicleCompany(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicles = _vehicleService.GetVehiclesByCompany(id, pageIndex, pageSize);
                
                return Ok(vehicles);
            }, "Ocurrió un error al obtener los vehiculos.");

        }

        [HttpGet]
        [Route("api/vehicle/client/{id}")]
        public IHttpActionResult GetVehicleClient(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicles = _vehicleService.GetVehiclesByClient(id, pageIndex, pageSize);
                
                return Ok(vehicles);
            }, "Ocurrió un error al obtener los vehiculos.");

        }


        [HttpGet]
        [Route("api/vehicle/{id}")]
        public IHttpActionResult GetVehicle(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicle = _vehicleService.GetVehicle(id);
                if (vehicle == null)
                    return NotFound();

                return Ok(vehicle);
            }, "❌ Error al obtener vehiculo");

        }

        [HttpDelete]
        [Route("api/vehicle/{id}")]
        public IHttpActionResult DeleteVehicle(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _vehicleService.DeleteVehicle(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Vehiculo eliminado") : Content(HttpStatusCode.Conflict, "❌ Error al eliminar");

            }, "❌ Error al eliminar.");
        }

    }


}