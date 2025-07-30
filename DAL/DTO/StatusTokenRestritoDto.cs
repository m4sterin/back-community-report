namespace back_community_report.DAL.DTO
{
    public class StatusTokenRestritoDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Prefeitura { get; set; }
        public string Setor { get; set; }
        public string Tipo { get; set; }
        public bool Perfil { get; set; }
        public string Token { get; set; }
    }
}