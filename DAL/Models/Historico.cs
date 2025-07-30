using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace back_community_report.DAL.Models
{
    public class Historico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Id_Chamado")]
        public string Id_Chamado { get; set; }

        [BsonElement("Status_Atual")]
        public string Status_Atual { get; set; }

        [BsonElement("Setor_Responsavel")]
        public string Setor_Responsavel { get; set; }

        [BsonElement("Data_Atual")]
        public DateTime Data_Atual { get; set; }

        [BsonElement("Justificativa")]
        public string Justificativa { get; set; }

        [BsonElement("Responsavel_Gestao")]
        public string Responsavel_Gestao { get; set; }
    }
}