using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.BLL
{
    public interface IUsuarioPublicoBll
    {
        // CREATE
        void Inserir(UsuarioPublico objUsuario);
        
        // READ
        List<UsuarioPublico> ObterTodos();
        UsuarioPublico ObterPorLogin(string loginUsuario);
        UsuarioPublico ObterPorEmail(string emailUsuario);
        
        // UPDATE
        void Atualizar(string id, UsuarioPublico objUsuario);
        void AtualizarSenha(string id, string senhaAntiga, string senhaNova);
        void RedefinirEndereco(string id, EnderecoDto endereco);
        
        // DELETE
        void Deletar(string id);
        
        // UTILS & VALIDATIONS
        string CriptografarSenhaSHA256(string senha);
    }
}