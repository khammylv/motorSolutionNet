using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorSolutionNet.Data;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;
using Client = MotorSolutionNet.Models.Client;

namespace MotorSolutionNet.Controllers
{
    public class ClientController : ApiController
    {
        private readonly ClientData _clientData;
        public ClientController()
        {
            _clientData = new ClientData();
        }
        // GET: api/client
        [HttpGet]
        [Route("api/client")]
        public IHttpActionResult GetClientList()
        {

            return ControllerHelper.ExecuteAction(this, () =>
            {
                var clients = _clientData.ListClients();
                return Ok(clients);
            }, "Ocurrió un error al obtener los clientes.");

}
        // POST: api/client
        [HttpPost]
        [Route("api/client")]
        public IHttpActionResult AddClientControl([FromBody] Client client)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var clientVal = _clientData.GetClientValidation(email: client.Email, identification: client.Identification, companyCode: client.CompanyCode);
                if (clientVal != null)

                    return BadRequest("Cliente Existente.");
                bool ok = _clientData.AddClient(client);
                return ok ? Content(HttpStatusCode.OK, "✅ Cliente agregado") : Content(HttpStatusCode.Conflict, "❌ Error al agregar cliente");
            }, "❌ Error al agregar cliente");
        }
     
        [HttpPut]
        [Route("api/client")]
        public IHttpActionResult UpdateClientControl([FromBody] Client client)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _clientData.UpdateClient(client);
                return ok ? Content(HttpStatusCode.OK, "✅ Cliente actualizado") : Content(HttpStatusCode.Conflict, "❌ Error al actualizar cliente.");

            }, "❌ Error al actualizar cliente.");

        }

        [HttpDelete]
        [Route("api/client/{id}")]
        public IHttpActionResult DeleteClientControl(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _clientData.DeleteClient(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Cliente eliminado") : Content(HttpStatusCode.Conflict, "❌ Error al eliminar");

            }, "❌ Error al eliminar.");
        }

        [HttpGet]
        [Route("api/client/{id}")]
        public IHttpActionResult GetClient(int id )
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var client = _clientData.GetClient(id);
                if (client == null)
                    return NotFound();

                return Ok(client);
            }, "❌ Error al obtener cliente");

        }
        [HttpGet]
        [Route("api/client/company/{id}")]
        public IHttpActionResult GetClientCompany(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var client = _clientData.GetClientByCompany(id);
                return Ok(client);
            }, "Ocurrió un error al obtener los usuarios.");

        }

    }
}