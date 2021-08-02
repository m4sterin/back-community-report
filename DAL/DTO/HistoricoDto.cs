using System;
using back_community_report.DAL.Models;

namespace back_community_report.DAL.DTO
{
    public class HistoricoDto
    {
        public string Id { get; set; }
        public string Id_Chamado { get; set; }
        public string Status_Atual { get; set; }
        public string Setor_Responsavel { get; set; }
        public DateTime Data_Atual { get; set; }
        public string Justificativa { get; set; }
        public string Responsavel_Gestao { get; set; }
        public string Color { get; set; }
    }
}