using System;

namespace back_community_report.DAL.DTO
{
    public class CategoriaDto
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Id_Setor { get; set; }
        public string Setor { get; set; }
        public string Status { get; set; }
    }
}