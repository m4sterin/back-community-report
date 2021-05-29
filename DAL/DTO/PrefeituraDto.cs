using System.Collections.Generic;

namespace back_community_report.DAL.DTO
{
    public class PrefeituraDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}