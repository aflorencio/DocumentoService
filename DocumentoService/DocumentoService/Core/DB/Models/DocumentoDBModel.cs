using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentoService.Core.DB.Models
{
    public class DocumentoDBModel
    {
        public ObjectId _id { get; set; }
        public ObjectId contactoService { get; set; }
        [BsonIgnoreIfNull]
        public ObjectId ticketService { get; set; }
        [BsonIgnoreIfNull]
        public string directorio { get; set; }
        [BsonIgnoreIfNull]
        public string nombre { get; set; }
        [BsonIgnoreIfNull]
        public string extension { get; set; }
        [BsonIgnoreIfNull]
        public DateTime fechaCreacion { get; set; }
        [BsonIgnoreIfNull]
        public List<string> tags { get; set; }
        [BsonIgnoreIfNull]
        public int acceso { get; set; }
        [BsonIgnoreIfNull]
        public bool visible { get; set; } = true;
    }

}