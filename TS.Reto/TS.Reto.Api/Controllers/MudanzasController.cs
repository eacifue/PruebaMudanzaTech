using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using System.Web.Http.Results;
using System.Web.Http;
using System.IO;
using TS.Reto.BM;

namespace TS.Reto.WEB.Controllers
{
    
    [RoutePrefix("api/Mudanzas")]
    public class MudanzasController : ApiController
    {
        // GET: api/Mudanzas
        [HttpPost]
        [Route("PostCargarArchivo/{cedula}")]
        public IHttpActionResult PostCargarArchivo(string cedula)
        {
            BMArchivo objArchivo = new BMArchivo();
            string ArchivoRespuesta = String.Empty;
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {

                    var archivo = HttpContext.Current.Request.Files["ArchivoCargado"];

                    Stream a = archivo.InputStream;
                    StreamReader sr = new StreamReader(a);
                    ArchivoRespuesta = objArchivo.CargarArchivo(sr, cedula);

                    
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent(ArchivoRespuesta);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "ArchivoSalida.txt"
                    };

                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                }
                else
                {

                    return null;
                }                

            }
            catch(Exception ex)
            {

            }

            ResponseMessageResult responseMessageResult = ResponseMessage(response);


            return responseMessageResult;
        }




    }
}
