using System;
using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public interface ICategoriaDao
    {
        // Create
        void Inserir(Categoria objCategoria);
        
        // Read
        List<CategoriaDto> ObterTodas();
        List<CategoriaDto> ObterPorSetor(string idSetor);
        Categoria ObterPorId(string id);
        string ObterPorDescricao(string descricao);
        
        // Update
        void Atualizar(string id, Categoria objCategoria);
        
        // Delete
        void Deletar(string id);
    }
}