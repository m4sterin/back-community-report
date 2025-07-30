namespace back_community_report.DAL.DTO
{
    public class UsuarioRestritoDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Id_Prefeitura { get; set; }
        public string Id_Setor { get; set; }
        public bool Perfil { get; set; }
    }
}