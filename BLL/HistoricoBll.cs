using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using back_community_report.DAL.Models; 
using back_community_report.DAL.DTO;
using back_community_report.DAL.DAO;
using back_community_report.BLL.Exceptions;

namespace back_community_report.BLL
{
    public class HistoricoBll : IHistoricoBll
    {
        public readonly IHistoricoDao _historicoDao;

        public HistoricoBll(IHistoricoDao historicoDao)
        {
            _historicoDao = historicoDao;
        }

        public List<HistoricoDto> ObterPorChamado(string idChamado)
        {
            bool validId = String.IsNullOrWhiteSpace(idChamado); 

            if(validId){
                throw new ArgumentException("Id não pode ser vazio. ObterPorChamado() BLL falhou !");
            }
            
            try {
                var resultado = _historicoDao.ObterPorChamado(idChamado);
                return resultado;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos históricos.");
            }
        }
    }
}
