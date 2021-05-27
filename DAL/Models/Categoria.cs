using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace back_community_report.DAL.Models
{
    public class Categoria
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Descricao")]
        public string Descricao { get; set; }

        [BsonElement("Id_Setor")]
        public string Id_Setor { get; set; }

        [BsonElement("Status")]
        public string Status { get; set; } // Esse status é para definir se a categoria está ou não ativa
    }
}