using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.Models
{
    public class UsuarioPublico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; }
        
        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Cpf")]
        public string Cpf { get; set; }

        [BsonElement("Endereco")]
        public EnderecoDto Endereco { get; set; }

        [BsonElement("Prefeitura")]
        public string Prefeitura { get; set; }

        [BsonElement("Login")]
        public string Login { get; set; }

        [BsonElement("Senha")]
        public string Senha { get; set; }
    }
}