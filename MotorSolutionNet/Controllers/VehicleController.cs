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
    public class VehicleController : ApiController
    {
        private readonly VehicleData _vehicleData;

        public VehicleController()
        {
            _vehicleData = new VehicleData();
        }

        [HttpPost]
        [Route("api/vehicle")]
        public IHttpActionResult AddVehicle([FromBody] Vehicle vehicle)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicleVal = _vehicleData.GetVehicleValidation(plate: vehicle.Plate, companyCode: vehicle.CompanyCode);
                if (vehicleVal != null)
                    return BadRequest("Vehiculo Existente.");
                bool ok = _vehicleData.AddVehicle(vehicle);
                return ok ? Content(HttpStatusCode.OK, "✅ Vehiculo agregado") : Content(HttpStatusCode.Conflict, "❌ Error al agregar vehiculo");

            }, "❌ Error al agregar vehiculo");
        }

        [HttpPut]
        [Route("api/vehicle")]
        public IHttpActionResult UpdateVehicle([FromBody] Vehicle vehicle)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _vehicleData.UpdateVehicle(vehicle);
                return ok ? Content(HttpStatusCode.OK, "✅ Vehiculo actualizado") : Content(HttpStatusCode.Conflict, "❌ Error al actualizar vehiculo.");

            }, "❌ Error al actualizar vehiculo.");

        }

        [HttpPatch]
        [Route("api/vehicle/{id}")]
        public IHttpActionResult UpdateDepartureDate(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _vehicleData.UpdateDepartureDate(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Fecha del vehiculo actualizada") : Content(HttpStatusCode.Conflict, "❌ Error al actualizar la fecha de salida del vehiculo");
            }, "❌ Error al actualizar la fecha de salida del vehiculo.");
        }


        [HttpGet]
        [Route("api/vehicle/company/{id}")]
        public IHttpActionResult GetVehicleCompany(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicles = _vehicleData.GetVehiclesByCompany(id);
                return Ok(vehicles);
            }, "Ocurrió un error al obtener los vehiculos.");

        }

        [HttpGet]
        [Route("api/vehicle/client/{id}")]
        public IHttpActionResult GetVehicleClient(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicles = _vehicleData.GetVehiclesByClient(id);
                return Ok(vehicles);
            }, "Ocurrió un error al obtener los vehiculos.");

        }


        [HttpGet]
        [Route("api/vehicle/{id}")]
        public IHttpActionResult GetVehicle(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var vehicle = _vehicleData.GetVehicle(id);
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
                bool ok = _vehicleData.DeleteVehicle(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Vehiculo eliminado") : Content(HttpStatusCode.Conflict, "❌ Error al eliminar");

            }, "❌ Error al eliminar.");
        }

    }


}