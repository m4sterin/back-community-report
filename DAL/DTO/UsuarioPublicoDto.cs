namespace back_community_report.DAL.DTO
{
    public class UsuarioPublicoDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public EnderecoDto Endereco { get; set; }
        public string Prefeitura { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}