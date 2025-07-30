using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace back_community_report.DAL.Models
{
    public class Tipo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Id_Categoria")]
        public string Id_Categoria { get; set; }
        
        [BsonElement("Descricao")]
        public string Descricao { get; set; }

        [BsonElement("Status")]
        public string Status { get; set; }
    }
}