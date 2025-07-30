using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO; 
using back_community_report.DAL.DAO;
using back_community_report.BLL.Exceptions;

namespace back_community_report.BLL
{
    public class UsuarioPublicoBll : IUsuarioPublicoBll
    {
        public readonly IUsuarioPublicoDao _usuarioPublicoDao;

        public UsuarioPublicoBll(IUsuarioPublicoDao usuarioPublicoDao)
        {
            _usuarioPublicoDao = usuarioPublicoDao;
        }

        public void Inserir(UsuarioPublico objUsuario)
        {
            bool hasAnyUserEmail = (_usuarioPublicoDao.ObterPorEmail(objUsuario.Email)) != null;

            if (!hasAnyUserEmail) {
                try {
                    _usuarioPublicoDao.Inserir(objUsuario);
                }
                catch(Exception) {
                    throw new System.Exception("Ocorreu um erro durante a criação do usuário.");
                }
            }
            else {
                throw new AlreadyExistsException("Não foi possível efetuar a inserção.");
            }
        }

        public List<UsuarioPublico> ObterTodos()
        {
            try {
                var usuarios = _usuarioPublicoDao.ObterTodos();    
                return usuarios;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos usuários.");
            }
        }

        public UsuarioPublico ObterPorLogin(string login)
        {
            bool validLogin = String.IsNullOrWhiteSpace(login); 

            if(validLogin) {
                throw new ArgumentException("Login não pode ser vazio. ObterPorLogin() BLL falhou !");
            }
            
            var usuario = _usuarioPublicoDao.ObterPorLogin(login);

            if(usuario == null) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                return usuario;
            }
        }

        public UsuarioPublico ObterPorEmail(string email)
        {
            bool validEmail = String.IsNullOrWhiteSpace(email); 

            if(validEmail) {
                throw new ArgumentException("E-mail não pode ser vazio. ObterPorEmail() BLL falhou !");
            }
            
            var usuario = _usuarioPublicoDao.ObterPorEmail(email);

            if(usuario == null) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                return usuario;
            }
        }

        public void Atualizar(string id, UsuarioPublico objUsuario)
        {
            var usuario = _usuarioPublicoDao.ObterPorEmail(objUsuario.Email);
            bool hasAnyUserEmail = usuario != null;

            if(!hasAnyUserEmail) {
                try {
                    _usuarioPublicoDao.Atualizar(id, objUsuario);
                }
                catch(DbUpdateConcurrencyException e) {
                    throw new DbConcurrencyException(e.Message);
                }
            } 
            else {
                if(usuario.Id == id) {
                    try {
                        _usuarioPublicoDao.Atualizar(id, objUsuario);
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
                _usuarioPublicoDao.AtualizarSenha(id, senhaAntiga, senhaNova);
            }
            catch (IncorrectPasswordException ex) {
                throw new IncorrectPasswordException(ex.Message);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a atualização na tabela.");
            }
        }

        public void RedefinirEndereco(string id, EnderecoDto endereco)
        {
            bool validId = String.IsNullOrWhiteSpace(id);

            if(validId) {
                throw new ObrigatoryFieldNotNullException("ID não pode ser vazio ou nulo.");
            }

            try {
                _usuarioPublicoDao.RedefinirEndereco(id, endereco);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a atualização na tabela.");
            }
        }

        public void Deletar(string id)
        {
            var usuario = _usuarioPublicoDao.ObterPorId(id);
            bool hasAnyUser = usuario != null;
            
            if(!hasAnyUser) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                try {
                    _usuarioPublicoDao.Deletar(usuario.Id);
                }
                catch(DbUpdateException) {
                    throw new DbUpdateException("Não foi possível efetuar a remoção.");
                }
            }
        }

        public string CriptografarSenhaSHA256(string senha)
        {
            try {
                return _usuarioPublicoDao.CriptografarSenhaSHA256(senha);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a criptografia da senha.");
            }
        }
    }
}
