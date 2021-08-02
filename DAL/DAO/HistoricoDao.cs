using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;
using MongoDB.Driver;

namespace back_community_report.DAL.DAO
{
    public class HistoricoDao : IHistoricoDao
    {
        public readonly IMongoContext _context;

        public HistoricoDao(IMongoContext context)
        {
            _context = context;
        }

        public void Inserir(Historico objHistorico)
        {
            _context.CollectionHistorico.InsertOne(objHistorico);
        }
        
        public List<HistoricoDto> ObterTodos()
        {
            List<HistoricoDto> resultado = new List<HistoricoDto>();
            
            var historicos = _context.CollectionHistorico.Find(hist => true).ToList();

            foreach (var item in historicos)
            {
                var cor = "";
                if (item.Status_Atual == "Em Análise") {
                    cor = "blue";
                } else if (item.Status_Atual == "Em Atendimento") {
                    cor = "amber";
                } else if (item.Status_Atual == "Finalizado") {
                    cor = "green";
                } else if (item.Status_Atual == "Enviado para o Setor Responsável") {
                    cor = "orange";
                } else {
                    cor = "red";
                }
                
                HistoricoDto hist = new HistoricoDto {
                    Id = item.Id,
                    Id_Chamado = item.Id_Chamado,
                    Status_Atual = item.Status_Atual,
                    Setor_Responsavel = item.Setor_Responsavel,
                    Data_Atual = item.Data_Atual,
                    Justificativa = item.Justificativa,
                    Responsavel_Gestao = item.Responsavel_Gestao,
                    Color = cor
                };

                resultado.Add(hist);
            }

            return resultado;
        }

        public List<HistoricoDto> ObterPorChamado(string idChamado)
        {
            List<HistoricoDto> resultado = new List<HistoricoDto>();
            
            var sort = Builders<Historico>.Sort.Descending(hist => hist.Data_Atual);
            var historicos = _context.CollectionHistorico.Find<Historico>(hist => hist.Id_Chamado == idChamado).Sort(sort).ToList();
            
            foreach (var item in historicos)
            {
                var cor = "";
                if (item.Status_Atual == "Em Análise") {
                    cor = "blue";
                } else if (item.Status_Atual == "Em Atendimento") {
                    cor = "amber";
                } else if (item.Status_Atual == "Finalizado") {
                    cor = "green";
                } else if (item.Status_Atual == "Enviado para o Setor Responsável") {
                    cor = "orange";
                } else {
                    cor = "red";
                }
                
                HistoricoDto hist = new HistoricoDto {
                    Id = item.Id,
                    Id_Chamado = item.Id_Chamado,
                    Status_Atual = item.Status_Atual,
                    Setor_Responsavel = item.Setor_Responsavel,
                    Data_Atual = item.Data_Atual,
                    Justificativa = item.Justificativa,
                    Responsavel_Gestao = item.Responsavel_Gestao,
                    Color = cor
                };

                resultado.Add(hist);
            }

            return resultado;
        }

        public void Deletar(string idChamado)
        {
            var historicos = _context.CollectionHistorico.Find<Historico>(hist => hist.Id_Chamado == idChamado).ToList();
            
            foreach (var item in historicos)
            {
                _context.CollectionHistorico.DeleteOne(hist => hist.Id_Chamado == item.Id_Chamado);
            }
        }
    }
}