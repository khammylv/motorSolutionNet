using System.Net;
using System.Web.Http;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;


namespace MotorSolutionNet.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserService _userService;
        

        public UserController()
        {
            _userService = new UserService();
           

        }

      

        // POST: api/user   
        [HttpPost]
        [Route("api/user")]
        public IHttpActionResult AddUserControl([FromBody] User user)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var userVal = _userService.GetUser(userEmail: user.UserEmail, userIdentification: user.UserIdentification);
                if (userVal != null)

                    return BadRequest("Este usuario ya existe.");
                bool ok = _userService.AddUser(user);
                return ok ? Content(HttpStatusCode.OK, "✅ Usuario agregado") : Content(HttpStatusCode.Conflict, "❌ Error al agregar usuario");
            }, "❌ Error al agregar usuario.");

        }


   
        [HttpPut]
        [Route("api/user")]
        public IHttpActionResult PutUser( [FromBody] UserDTO user)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                bool resultado = _userService.UpdateUser(user);
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
                bool resultado = _userService.DeleteUser(id);
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
                var user = _userService.GetUserById(userId: id);
                if (user == null)
                    return Content(HttpStatusCode.NotFound, $"⚠️ El usuario con ID {id} no existe.");

                return Ok(user);
            }, "❌ Error al encontrar usuario.");
        }

 
        [HttpGet]
        [Route("api/user/company/{id}")]
        public IHttpActionResult GetUserCompanyCode(int id, int pageIndex, int pageSize)
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                
                var result = _userService.GetUserByCompany(companyCode: id, pageIndex, pageSize);
                return Ok(result);
            }, "Ocurrió un error al obtener los usuarios.");

        }

        [HttpGet]
        [Route("api/user/role")]
        public IHttpActionResult GetUserRole()
        {
            return ControllerHelper.ExecuteAction(this, () =>
            {
                var roles = _userService.GetRoleOptions();
                return Ok(roles);
            }, "Ocurrió un error al obtener los roles de usuario.");
        }
    }
}