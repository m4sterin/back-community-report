using System;
using back_community_report.DAL.Models;

namespace back_community_report.DAL.DTO
{
    public class SetorDto
    {
        public string Id { get; set; }
        public string Id_Prefeitura { get; set; }
        public string Nome { get; set; }
        public string Responsavel { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
        public string Status { get; set; }
    }
}