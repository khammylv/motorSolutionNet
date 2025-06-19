using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using dotenv.net;
using MotorSolutionNet.Data;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;
using MotorSolutionNet.Utilities;
using MySql.Data.MySqlClient;

namespace MotorSolutionNet.Controllers
{
    public class TestApiController : ApiController
    {
        [HttpGet]
        [Route("api/testdb/connection")]
        public IHttpActionResult TestConection()
        {
            ConectionDB db = new ConectionDB();
            SqlConnection conection = null;

            try
            {   conection = db.GetConnection();
                conection.Open();
               
                
                
                return Ok("✅ Conexión exitosa usando la clase ConexionDB.");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, $"❌ Error de conexión: {ex.Message}");
            }
            finally
            {
                if (conection != null)
                {
                    conection.Dispose();
                }
            }
        }
        [HttpPost]
        [Route("api/testapi/test")]
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
        [HttpGet]
        [Route("api/testapi/test")]
        public IHttpActionResult Test()
        {
            return Ok("La API funciona correctamente 🎉");
        }
    }
}