using System;

namespace back_community_report.DAL.DTO
{
    public class TipoDto
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Id_Categoria { get; set; }
        public string Categoria { get; set; }
        public string Status { get; set; }
    }
}