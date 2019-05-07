using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentoService.Core
{
    public class MainCore
    {
        #region CREATE

        public void CrearPresupuesto(DB.Models.DocumentoDBModel data)
        {

            DB.Query.DocumentoQuery qCreate = new DB.Query.DocumentoQuery("mongodb://51.83.73.69:27017");
            qCreate.CrearDocumento(data);

        }

        #endregion

        #region READ

        public List<DB.Models.DocumentoDBModel> ReadAll()
        {
            DB.Query.DocumentoQuery qReadAll = new DB.Query.DocumentoQuery("mongodb://51.83.73.69:27017");

            return qReadAll.GetAllDocumento();
        }

        public Core.DB.Models.DocumentoDBModel ReadId(string id)
        {

            DB.Query.DocumentoQuery qReadId = new DB.Query.DocumentoQuery("mongodb://51.83.73.69:27017");

            var data = qReadId.GetDocumentoById(id);
            return data;
        }

        // ESTE ES EL CAMPO DE BUSQUEDA. BUSCA POR VALOR CLAVE Y AHORA MISMO NO ESTA IMPLEMENTADO EN EL QUERY


        //public List<Core.DB.Models.PresupuestoDBModel> ReadValue(string fieldName, string fieldValue)
        //{
        //    Core.DB.Query.PresupuestoQuery qReadId = new Core.DB.Query.PresupuestoQuery("mongodb://51.83.73.69:27017");

        //    var data = qReadId.GetPresupuestoByField(fieldName, fieldValue);
        //    return data;

        //}

        #endregion

        #region UPDATE

        public void Update(string id, string updateFieldName, string updateFieldValue)
        {

            DB.Query.DocumentoQuery qUpdate = new DB.Query.DocumentoQuery("mongodb://51.83.73.69:27017");
            qUpdate.UpdateDocumento(id, updateFieldName, updateFieldValue);

        }

        #endregion

        #region DELETE

        public void Delete(string id)
        {
            DB.Query.DocumentoQuery qDelete = new DB.Query.DocumentoQuery("mongodb://51.83.73.69:27017");
            qDelete.DeleteDocumentoById(id);
        }

        #endregion
    }
}