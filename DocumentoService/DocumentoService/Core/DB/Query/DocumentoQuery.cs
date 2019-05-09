using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using DBModel = DocumentoService.Core.DB.Models.DocumentoDBModel;

namespace DocumentoService.Core.DB.Query
{
    

    public class DocumentoQuery
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<DBModel> _Collection;

        public DocumentoQuery(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("DocumentoService");
            _Collection = _database.GetCollection<DBModel>("documento");    
        }

        #region CREATE

        public async Task Create(DBModel documento) //CREATE
        {
            await _Collection.InsertOneAsync(documento);
        }

        #endregion

        #region READ

        public List<DBModel> ReadAll()
        {
            try
            {

            return _Collection.Find(new BsonDocument()).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DBModel ReadById(string id)
        {
            try
            {
                var filter = Builders<DBModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var data = _Collection.Find(filter).FirstOrDefault();
                return data;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        #endregion

        #region UPDATE

        public bool Update(string id, string udateFieldName, string updateFieldValue)
        {
            var filter = Builders<DBModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<DBModel>.Update.Set(udateFieldName, updateFieldValue);

            var result = _Collection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        #endregion

        #region DELETE

        public bool DeleteById(string id)
        {
            var filter = Builders<DBModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = _Collection.DeleteOne(filter);
            return result.DeletedCount != 0;
        }


        #endregion
    }
}