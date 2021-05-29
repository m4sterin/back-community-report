using System.Collections.Generic;
using back_community_report.DAL.Models;

namespace back_community_report.BLL
{
    public interface IUsuarioRestritoBll
    {
        // CREATE
        void Inserir(UsuarioRestrito objUsuario);
        
        // READ
        List<UsuarioRestrito> ObterTodos();
        List<UsuarioRestrito> ObterPorPrefeitura(string idPrefeitura);
        List<UsuarioRestrito> ObterPorSetor(string idSetor);
        UsuarioRestrito ObterPorLogin(string login);
        UsuarioRestrito ObterPorEmail(string email);
        
        // UPDATE
        void Atualizar(string id, UsuarioRestrito objUsuario);
        void AtualizarSenha(string id, string senhaAntiga, string senhaNova);
        void RedefinirSenha(string id, string novaSenha);
        
        // DELETE
        void Deletar(string id);
        
        // UTILS & VALIDATIONS
        string CriptografarSenhaSHA256(string senha);
    }
}