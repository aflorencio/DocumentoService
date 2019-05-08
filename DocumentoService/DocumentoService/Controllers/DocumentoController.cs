using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DocumentoService.Controllers
{
    public class DocumentoController : ApiController
    {
        private Core.MainCore _ = new Core.MainCore();

        #region GET
        // GET: api/Documento
        [HttpGet]
        [Route("api/Documento")]
        public List<Core.DB.Models.DocumentoDBModel> Get()
        {
            var data = _.ReadAll();

            return data;
        }

        // GET: api/Documento/5
        [HttpGet]
        [Route("api/Documento/{id}")]
        public Core.DB.Models.DocumentoDBModel Get(string id)
        {
            var data = _.ReadId(id);

            return data;
        }
        #endregion

        #region POST
        // POST: api/Documento
        [HttpPost]
        [Route("api/Documento")]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            var jsonString = await request.Content.ReadAsStringAsync();
            var otro = request.Headers;
            var requestt = HttpContext.Current.Request;
            var fileName = requestt.Headers["algo"];

            Core.DB.Models.DocumentoDBModel account = JsonConvert.DeserializeObject<Core.DB.Models.DocumentoDBModel>(jsonString);
            _.CrearPresupuesto(account);

            // return "OK!";
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
        #endregion

        #region PUT

        // PUT: api/Documento/5
        [HttpPut]
        [Route("api/Documento/{id}")]
        public string Put(string id, FormDataCollection value)
        {
            var name = value.FirstOrDefault().Key.ToString();
            var valor = value.FirstOrDefault().Value.ToString();

            Core.DB.Models.DocumentoDBModel obj = new Core.DB.Models.DocumentoDBModel();
            var existeMetodo = obj.GetType().GetProperty(name) == null ? false : true;
            if (existeMetodo == true)
            {
                _.Update(id, name, valor);
                return "OK!";
            }

            return "Error";
        }

        #endregion

        #region DELETE

        // DELETE: api/Documento/5
        [HttpDelete]
        [Route("api/Documento/{id}")]
        public void Delete(string id)
        {
            _.Delete(id);
        }

        #endregion
















        [HttpPost]
        [Route("api/Documento/upload")]
        public async Task<HttpResponseMessage> PostUserImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {

                            var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension); //Para almacenarlo en la dir del proyecto de forma virtual
                            var filePath2 = @"C:\Userimage\" + postedFile.FileName + extension; // Para almacenarlo en una direccion fisica de forma fisica ejemplo en C


                            postedFile.SaveAs(filePath2);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("Error some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }











    }
}

