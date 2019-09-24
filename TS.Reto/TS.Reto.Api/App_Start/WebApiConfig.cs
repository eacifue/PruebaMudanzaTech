using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Text;

namespace TS.Reto.WEB
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            // Configure Web API para usar solo la autenticación de token de portador.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //-- Elimina el formato XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //Configuracion UTF-8 es requerida para que se pueda leer el json con todos los caracteres.
            Encoding oldDefault = Encoding.GetEncoding("utf-8");
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.Clear();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.Add(oldDefault);
            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "asesora/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );




        }
    }
}
