using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocumentoService.Core.DB.Query
{
    

    public class DocumentoQuery
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<DB.Models.DocumentoDBModel> _presupuestoCollection;

        public DocumentoQuery(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("DocumentoService");
            _presupuestoCollection = _database.GetCollection<DB.Models.DocumentoDBModel>("documentos");    
        }

        #region CREATE

        public async Task CrearDocumento(DB.Models.DocumentoDBModel documento) //CREATE
        {
            await _presupuestoCollection.InsertOneAsync(documento);
        }

        #endregion

        #region READ

        public List<DB.Models.DocumentoDBModel> GetAllDocumento()
        {
            return _presupuestoCollection.Find(new BsonDocument()).ToList();
        }

        public DB.Models.DocumentoDBModel GetDocumentoById(string id)
        {
            try
            {
                var filter = Builders<DB.Models.DocumentoDBModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var data = _presupuestoCollection.Find(filter).FirstOrDefault();
                return data;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region UPDATE

        public bool UpdateDocumento(string id, string udateFieldName, string updateFieldValue)
        {
            var filter = Builders<DB.Models.DocumentoDBModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<DB.Models.DocumentoDBModel>.Update.Set(udateFieldName, updateFieldValue);

            var result = _presupuestoCollection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        #endregion

        #region DELETE

        public bool DeleteDocumentoById(string id)
        {
            var filter = Builders<DB.Models.DocumentoDBModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = _presupuestoCollection.DeleteOne(filter);
            return result.DeletedCount != 0;
        }


        #endregion
    }
}