using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace back_community_report.DAL.Models
{
    public class Arquivo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Id_Chamado")]
        public string Id_Chamado { get; set; }

        [BsonElement("Arquivo_Hash")]
        public string Arquivo_Hash { get; set; }
    }
}