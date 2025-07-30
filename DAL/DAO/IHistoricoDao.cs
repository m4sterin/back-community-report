using System;
using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public interface IHistoricoDao
    {
        // CREATE
        void Inserir(Historico objHistorico);
        
        // READ
        List<HistoricoDto> ObterTodos();
        List<HistoricoDto> ObterPorChamado(string idChamado);
        
        // DELETE
        void Deletar(string idChamado);
    }
}