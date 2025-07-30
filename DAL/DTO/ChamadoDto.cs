using System;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DTO
{
    public class ChamadoDto
    {
        // Informações do Chamado
        public string Id { get; set; }
        public string Privacidade { get; set; }
        public string Id_Categoria { get; set; }
        public string Categoria { get; set; }
        public string Id_Tipo { get; set; }
        public string Tipo { get; set; }
        public string Observacao { get; set; }
        public EnderecoDto Endereco { get; set; }
        public string Status { get; set; }
        public DateTime Data_Abertura { get; set; }
        public DateTime? Data_Conclusao { get; set; }
        public string Id_Prefeitura { get; set; }
        public string Prefeitura { get; set; }
        public string Id_Setor { get; set; }
        public string Setor { get; set; }
        public string Justificativa { get; set; }

        // Informação do "dono" do Chamado
        public string Id_Usuario_Publico { get; set; }
        public string Usuario_Publico { get; set; }

        // Hash da Imagem cadastrada no banco
        public string Imagem { get; set; }
    }
}