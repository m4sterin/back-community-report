using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.Models
{
    public class Prefeitura
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; }
        
        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Site")]
        public string Site { get; set; }
        
        [BsonElement("Telefone")]
        public string Telefone { get; set; }

        [BsonElement("Endereco")]
        public EnderecoDto Endereco { get; set; }
    }
}