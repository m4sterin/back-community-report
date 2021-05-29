using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public interface IChamadoDao
    {
        // Create
        void Inserir(Chamado objChamado);
        
        // Read
        List<ChamadoDto> ObterTodos();
        List<ChamadoDto> ObterPorPrefeitura(string idPrefeitura);
        List<ChamadoDto> ObterPorSetor(string idPrefeitura, string idSetor);
        List<ChamadoDto> ObterPorUsuarioPublico(string idUsuario);
        List<ChamadoDto> ObterPorCategoria(string idCategoria);
        Chamado ObterPorId(string id);
        
        // Update
        void Atualizar(string id, Chamado objChamado);
        void EmAtendimento(DadosGerenciaDto dados);
        void Finalizado(DadosGerenciaDto dados);
        void EnviadoOutroSetor(DadosGerenciaDto dados);
        void Rejeitado(DadosGerenciaDto dados);
        
        // Delete
        void Deletar(string id);

        // Utils & Validations
        int TotalChamados(string idPrefeitura);
        int TotalChamadosEmAnalise(string idPrefeitura);
        int TotalChamadosEmAtendimento(string idPrefeitura);
        int TotalChamadosFinalizados(string idPrefeitura);
    }
}