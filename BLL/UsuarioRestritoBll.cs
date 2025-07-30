using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using back_community_report.DAL.Models; 
using back_community_report.DAL.DAO;
using back_community_report.BLL.Exceptions;

namespace back_community_report.BLL
{
    public class UsuarioRestritoBll : IUsuarioRestritoBll
    {
        public readonly IUsuarioRestritoDao _usuarioRestritoDao;

        public UsuarioRestritoBll(IUsuarioRestritoDao usuarioRestritoDao)
        {
            _usuarioRestritoDao = usuarioRestritoDao;
        }

        public void Inserir(UsuarioRestrito objUsuario)
        {
            bool hasAnyUserEmail = (_usuarioRestritoDao.ObterPorEmail(objUsuario.Email)) != null;

            if (!hasAnyUserEmail) {
                try {
                    _usuarioRestritoDao.Inserir(objUsuario);
                }
                catch(Exception) {
                    throw new System.Exception("Ocorreu um erro durante a criação do usuário.");
                }
            }
            else {
                throw new AlreadyExistsException("Não foi possível efetuar a inserção.");
            }
        }

        public List<UsuarioRestrito> ObterTodos()
        {
            try {
                var usuarios = _usuarioRestritoDao.ObterTodos();    
                return usuarios;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos usuários.");
            }
        }

        public List<UsuarioRestrito> ObterPorPrefeitura(string idPrefeitura)
        {
            try {
                var usuarios = _usuarioRestritoDao.ObterPorPrefeitura(idPrefeitura);    
                return usuarios;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos usuários.");
            }
        }

        public List<UsuarioRestrito> ObterPorSetor(string idSetor)
        {
            try {
                var usuarios = _usuarioRestritoDao.ObterPorSetor(idSetor);    
                return usuarios;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos usuários.");
            }
        }

        public UsuarioRestrito ObterPorLogin(string login)
        {
            bool validLogin = String.IsNullOrWhiteSpace(login); 

            if(validLogin) {
                throw new ArgumentException("Login não pode ser vazio. ObterPorLogin() BLL falhou !");
            }
            
            var usuario = _usuarioRestritoDao.ObterPorLogin(login);

            if(usuario == null) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                return usuario;
            }
        }

        public UsuarioRestrito ObterPorEmail(string email)
        {
            bool validEmail = String.IsNullOrWhiteSpace(email); 

            if(validEmail) {
                throw new ArgumentException("E-mail não pode ser vazio. ObterPorEmail() BLL falhou !");
            }
            
            var usuario = _usuarioRestritoDao.ObterPorEmail(email);

            if(usuario == null) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                return usuario;
            }
        }

        public void Atualizar(string id, UsuarioRestrito objUsuario)
        {
            var usuario = _usuarioRestritoDao.ObterPorEmail(objUsuario.Email);
            bool hasAnyUserEmail = usuario != null;

            if(!hasAnyUserEmail) {
                try {
                    _usuarioRestritoDao.Atualizar(id, objUsuario);
                }
                catch(DbUpdateConcurrencyException e) {
                    throw new DbConcurrencyException(e.Message);
                }
            } 
            else {
                if(usuario.Id == id) {
                    try {
                        _usuarioRestritoDao.Atualizar(id, objUsuario);
                    }
                    catch(DbUpdateConcurrencyException e) {
                        throw new DbConcurrencyException(e.Message);
                    }
                }
                else {
                    throw new AlreadyExistsException("Não foi possível efetuar a atualização.");
                }
            } 
        }

        public void AtualizarSenha(string id, string senhaAntiga, string senhaNova)
        {
            bool validId = String.IsNullOrWhiteSpace(id);
            bool validPassword1 = String.IsNullOrWhiteSpace(senhaAntiga);
            bool validPassword2 = String.IsNullOrWhiteSpace(senhaNova);

            if(validId) {
                throw new ObrigatoryFieldNotNullException("ID não pode ser vazio ou nulo.");
            }
            if(validPassword1) {
                throw new ObrigatoryFieldNotNullException("Senha Atual não pode estar em branco. Informe uma senha válida");
            }
            if(validPassword2) {
                throw new ObrigatoryFieldNotNullException("Senha Nova não pode estar em branco. Informe uma senha válida");
            }

            try {
                _usuarioRestritoDao.AtualizarSenha(id, senhaAntiga, senhaNova);
            }
            catch(IncorrectPasswordException ex) {
                throw new IncorrectPasswordException(ex.Message);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a atualização na tabela.");
            }
        }

        public void RedefinirSenha(string id, string nova_senha)
        {
            bool validId = String.IsNullOrWhiteSpace(id);
            bool validPassword = String.IsNullOrWhiteSpace(nova_senha);

            if(validId) {
                throw new ObrigatoryFieldNotNullException("ID não pode ser vazio ou nulo.");
            }
            if(validPassword) {
                throw new ObrigatoryFieldNotNullException("Senha não pode estar em branco. Informe uma senha válida");
            }

            try {
                _usuarioRestritoDao.RedefinirSenha(id, nova_senha);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a atualização na tabela.");
            }
        }

        public void Deletar(string id)
        {
            var usuario = _usuarioRestritoDao.ObterPorId(id);

            bool hasAnyUser = usuario != null;
            
            if(!hasAnyUser) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                try {
                    _usuarioRestritoDao.Deletar(usuario.Id);
                }
                catch(DbUpdateException) {
                    throw new DbUpdateException("Não foi possível efetuar a remoção.");
                }
            }
        }

        public string CriptografarSenhaSHA256(string senha)
        {
            try {
                return _usuarioRestritoDao.CriptografarSenhaSHA256(senha);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a criptografia da senha.");
            }
        }
    }
}
