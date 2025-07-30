using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.BLL
{
    public interface IHistoricoBll
    {
        // READ
        List<HistoricoDto> ObterPorChamado(string idChamado);
    }
}