using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorSolutionNet.Data;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;
using Mysqlx;

namespace MotorSolutionNet.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserData _userData;
       

        public UserController()
        {
            _userData = new UserData(); 
          
        }

        
        [HttpGet]
        [Route("api/user")]
        public IHttpActionResult GetUserList()
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var users = _userData.ListUsers();
                return Ok(users);
            }, "Ocurrió un error al obtener los usuarios.");
        }

        // POST: api/user   
        [HttpPost]
        [Route("api/user")]
        public IHttpActionResult AddUserControl([FromBody] User user)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var userVal = _userData.GetUser(userEmail: user.UserEmail, userIdentification: user.UserIdentification);
                if (userVal != null)

                    return BadRequest("Este usuario ya existe.");
                bool ok = _userData.AddUser(user);
                return ok ? Content(HttpStatusCode.OK, "✅ Usuario agregado") : Content(HttpStatusCode.Conflict, "❌ Error al agregar usuario");
            }, "❌ Error al agregar usuario.");

        }


   
        [HttpPut]
        [Route("api/user")]
        public IHttpActionResult PutUser( [FromBody] User user)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool resultado = _userData.UpdateUser(user);
                return resultado ? Content(HttpStatusCode.OK, "Usuario actualizado") : Content(HttpStatusCode.Conflict, "❌ Error al actualizar");

            }, "❌ Error al actualizar.");

        }
 


        // DELETE: api/user/{id}
        [HttpDelete]
        [Route("api/user/{id}")]
        public IHttpActionResult Delete(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool resultado = _userData.DeleteUser(id);
                return resultado ? Content(HttpStatusCode.OK, "Usuario eliminado") : Content(HttpStatusCode.Conflict, "❌ Error al eliminar");
            
            }, "❌ Error al eliminar.");


        }

        // GET: api/user/{id}
        [HttpGet]
        [Route("api/user/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var user = _userData.GetUser(userId: id);
                if (user == null)
                   return NotFound();
                
                return Ok(user);
            }, "❌ Error al encontrar usuario.");
        }

        [HttpGet]
        [Route("api/user/company/{id}")]
        public IHttpActionResult GetUserCompanyCode(int id)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var users = _userData.GetUserByCompany(companyCode: id);
                return Ok(users);
            }, "Ocurrió un error al obtener los usuarios.");
    
        }


        [HttpGet]
        [Route("api/user/role")]
        public IHttpActionResult GetUserRole()
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var roles = _userData.GetRoleOptions();
                return Ok(roles);
            }, "Ocurrió un error al obtener los roles de usuario.");
        }
    }
}