using System;
using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public interface ITipoDao
    {
        // Create
        void Inserir(Tipo objTipo);
        
        // Read
        List<TipoDto> ObterTodos();
        List<TipoDto> ObterPorCategoria(string idCategoria);
        Tipo ObterPorId(string id);
        
        // Update
        void Atualizar(string id, Tipo objTipo);
        
        // Delete
        void Deletar(string id);
    }
}