using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using dotenv.net;
using MotorSolutionNet.Utilities;


namespace MotorSolutionNet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            string projectRootPath = HostingEnvironment.ApplicationPhysicalPath;
            string envPath = Path.Combine(projectRootPath, ".env");
            System.Diagnostics.Debug.WriteLine($"ENV PATH: {envPath}");

            // Cargar variables de entorno desde el archivo .env
            DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { envPath }));
           // DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { @"D:\Proyectos Sena\proyectos\motorSolutionNet\MotorSolutionNet\.env" }));

            
            // Configuración y servicios de Web API

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
