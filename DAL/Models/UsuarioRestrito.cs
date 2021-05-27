using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_community_report.DAL.Models
{
    public class UsuarioRestrito
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; }
        
        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Login")]
        public string Login { get; set; }

        [BsonElement("Senha")]
        public string Senha { get; set; }

        [BsonElement("Id_Prefeitura")]
        public string Id_Prefeitura { get; set; }

        [BsonElement("Id_Setor")]
        public string Id_Setor { get; set; }

        [BsonElement("Perfil")]
        public bool Perfil { get; set; }
    }
}