using System;
using back_community_report.DAL.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_community_report.DAL.Models
{
    public class Chamado
    {
        // Informaçõe do Chamado
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Privacidade")]
        public string Privacidade { get; set; }

        [BsonElement("Id_Categoria")]
        public string Id_Categoria { get; set; }

        [BsonElement("Id_Tipo")]
        public string Id_Tipo { get; set; }

        [BsonElement("Observacao")]
        public string Observacao { get; set; }

        [BsonElement("Endereco")]
        public EnderecoDto Endereco { get; set; }

        [BsonElement("Status")]
        public string Status { get; set; }

        [BsonElement("Data_Abertura")]
        public DateTime Data_Abertura { get; set; }

        [BsonElement("Data_Conclusao")]
        public DateTime? Data_Conclusao { get; set; }
        
        [BsonElement("Id_Prefeitura")]
        public string Id_Prefeitura { get; set; }
        
        [BsonElement("Id_Setor")]
        public string Id_Setor { get; set; }

        [BsonElement("Justificativa")]
        public string Justificativa { get; set; }

        // Informação do "dono do chamado"
        [BsonElement("Id_Usuario_Publico")]
        public string Id_Usuario_Publico { get; set; }

        [BsonElement("Imagem")]
        public string Imagem { get; set; }
    }
}