using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace MotorSolutionNet.Services
{
    public class ControllerHelper
    {
        


        public static IHttpActionResult ExecuteAction(ApiController controller, Func<IHttpActionResult> action, string errorMessage = "Ocurrió un error inesperado.")
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                var error = new
                {
                    mensaje = errorMessage,
                    error = ex.Message
                };
                var response = controller.Request.CreateResponse(HttpStatusCode.InternalServerError, error);
                return new ResponseMessageResult(response);
            }
        }


    }


}