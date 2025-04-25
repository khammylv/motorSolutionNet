using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorSolutionNet.Data;
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
            MySqlConnection conection = null;
            try
            {
                conection = db.GetConnection();
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

        [HttpGet]
        [Route("api/testapi/test")]
        public IHttpActionResult Test()
        {
            return Ok("La API funciona correctamente 🎉");
        }
    }
}