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

    [RoutePrefix("api/login")]
    public class UserAuthController : ApiController
    {
        private readonly UserData _userData;
        private readonly UserService _userService;
        private readonly JwtService _jwtToken;

        public UserAuthController()
        {
            _userData = new UserData(); 
            _userService = new UserService();
            _jwtToken = new JwtService();
        }


        [HttpPost]
        [Route("user")]
        public IHttpActionResult UserLogin([FromBody] User user)
        {

            return ControllerHelper.ExecuteAction(this, () =>
            {
                var existingUser = _userData.GetUser(userEmail: user.UserEmail);
                if (existingUser == null)
                {
                    return NotFound();
                }
                bool isPasswordValid = _userService.VerifyPassword(user.UserPassword, existingUser.UserPassword);

                if (!isPasswordValid)
                {
                    return Content(HttpStatusCode.Unauthorized, "Contraseña incorrecta");
                }

                string token = _jwtToken.GenerateToken(existingUser.UserId, existingUser.UserEmail, existingUser.UserRole);
                System.Diagnostics.Debug.WriteLine("Token generado: " + token);

                return Ok(new { token });

            }, "❌ Error durante el inicio de sesión.");

          
        }


    }
}