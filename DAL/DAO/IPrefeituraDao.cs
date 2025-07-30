using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public interface IPrefeituraDao
    {
        // Create
        void Inserir(Prefeitura objPrefeitura);
        
        // Read
        List<Prefeitura> ObterTodas();
        Prefeitura ObterPorNome(string nome);
        Prefeitura ObterPorId(string id);
        
        // Update
        void Atualizar(string id, Prefeitura objPrefeitura);
        
        // Delete
        void Deletar(string id);

        // UTILS E VALIDATIONS
        string ObterIdPorNome(string nome);
    }
}