using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;
using Client = MotorSolutionNet.Models.Client;

namespace MotorSolutionNet.Controllers
{
    public class ClientController : ApiController
    {
        private readonly ClientServices _clientService;
        
        public ClientController()
        {
            _clientService = new ClientServices();
            
        }
        // GET: api/client
       /* [HttpGet]
        [Route("api/client")]
        public IHttpActionResult GetClientList()
        {

            return ControllerHelper.ExecuteAction(this, () =>
            {
                var clients = _clientData.ListClients();
                return Ok(clients);
            }, "Ocurrió un error al obtener los clientes.");

        }*/
        // POST: api/client
        [HttpPost]
        [Route("api/client")]
        public IHttpActionResult AddClientControl([FromBody] Client client)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var clientVal = _clientService.GetClientValidation(email: client.Email, identification: client.Identification, companyCode: client.CompanyCode);
                System.Diagnostics.Debug.WriteLine("Client: " + clientVal);
                if (clientVal != null)
                  return BadRequest("Cliente Existente.");
                bool ok = _clientService.AddClient(client);
                return ok ? Content(HttpStatusCode.OK, "✅ Cliente agregado") : Content(HttpStatusCode.Conflict, "❌ Error al agregar cliente");
            }, "❌ Error al agregar cliente");
        }
     
        [HttpPut]
        [Route("api/client")]
        public IHttpActionResult UpdateClientControl([FromBody] ClientDTO client)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _clientService.UpdateClient(client);
                return ok ? Content(HttpStatusCode.OK, "✅ Cliente actualizado") : Content(HttpStatusCode.Conflict, "❌ Error al actualizar cliente.");

            }, "❌ Error al actualizar cliente.");

        }

        [HttpDelete]
        [Route("api/client/{id}")]
        public IHttpActionResult DeleteClientControl(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool ok = _clientService.DeleteClient(id);
                return ok ? Content(HttpStatusCode.OK, "✅ Cliente eliminado") : Content(HttpStatusCode.Conflict, "❌ Error al eliminar");

            }, "❌ Error al eliminar.");
        }

        [HttpGet]
        [Route("api/client/{id}")]
        public IHttpActionResult GetClient(int id )
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var client = _clientService.GetClient(id);
                if (client == null)
                    return NotFound();

                return Ok(client);
            }, "❌ Error al obtener cliente");

        }
        [HttpGet]
        [Route("api/client/company/{id}")]
        public IHttpActionResult GetClientCompany(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var client = _clientService.GetClientByCompany(companyCode: id, pageIndex, pageSize);
               
                return Ok(client);
            }, "Ocurrió un error al obtener los usuarios.");

        }

    }
}