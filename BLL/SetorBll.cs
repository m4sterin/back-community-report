using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using back_community_report.DAL.Models; 
using back_community_report.DAL.DAO;
using back_community_report.BLL.Exceptions;

namespace back_community_report.BLL
{
    public class SetorBll : ISetorBll
    {
        public readonly ISetorDao _setorDao;

        public SetorBll(ISetorDao setorDao)
        {
            _setorDao = setorDao;
        }

        public void Inserir(Setor objSetor)
        {
            bool validNome = String.IsNullOrWhiteSpace(objSetor.Nome); 

            if(validNome) {
                throw new ObrigatoryFieldNotNullException("Descrição não pode ser vazio.");
            }
            
            try {
                _setorDao.Inserir(objSetor);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a criação do setor.");
            }
        }

        public List<Setor> ObterTodos()
        {
            try {
                var resultados = _setorDao.ObterTodos();    
                return resultados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos setors.");
            }
        }

        public List<Setor> ObterPorPrefeitura(string idPrefeitura)
        {   
            try {
                var resultados = _setorDao.ObterPorPrefeitura(idPrefeitura);
                return resultados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos setors.");
            }
        }

        public Setor ObterPorId(string id)
        {
            bool validId = String.IsNullOrWhiteSpace(id); 

            if(validId) {
                throw new ObrigatoryFieldNotNullException("Id não pode ser vazio.");
            }
            
            var resultado = _setorDao.ObterPorId(id);

            if(resultado == null) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                return resultado;
            }
        }

        public void Atualizar(string id, Setor objSetor)
        {
            var resultado = _setorDao.ObterPorId(id);
            bool hasAny = resultado != null;

            if(!hasAny) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                bool validNome = String.IsNullOrWhiteSpace(objSetor.Nome); 

                if(validNome) {
                    throw new ObrigatoryFieldNotNullException("Descrição não pode ser vazio.");
                }

                try {
                    _setorDao.Atualizar(id, objSetor);
                }
                catch(Exception) {
                    throw new System.Exception("Ocorreu um erro durante a atualização do setor.");
                }
            }
        }

        public void Deletar(string id)
        {
            var resultado = _setorDao.ObterPorId(id);
            bool hasAny = resultado != null;
            
            if(!hasAny) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                try {
                    _setorDao.Deletar(resultado.Id);
                }
                catch(Exception) {
                    throw new System.Exception("Ocorreu um erro durante a remoção do setor.");
                }
            }
        }
    }
}
