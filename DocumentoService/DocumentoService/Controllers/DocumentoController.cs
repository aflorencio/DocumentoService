using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
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
    }
}
