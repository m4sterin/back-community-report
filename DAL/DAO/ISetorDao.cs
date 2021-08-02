using System;
using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public interface ISetorDao
    {
        // Create
        void Inserir(Setor objSetor);
        
        // Read
        List<Setor> ObterTodos();
        List<Setor> ObterPorPrefeitura(string idPrefeitura);
        Setor ObterPorId(string id);
        
        // Update
        void Atualizar(string id, Setor objSetor);
        
        // Delete
        void Deletar(string id);

        // Utils e Validations
        string ObterIdPorNome(string nome);
    }
}