using System.Collections.Generic;
using back_community_report.DAL.Models;

namespace back_community_report.BLL
{
    public interface IPrefeituraBll
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
    }
}