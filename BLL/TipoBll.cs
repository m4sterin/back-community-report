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
    public class TipoBll : ITipoBll
    {
        public readonly ITipoDao _tipoDao;

        public TipoBll(ITipoDao tipoDao)
        {
            _tipoDao = tipoDao;
        }

        public void Inserir(Tipo objTipo)
        {
            bool validDesc = String.IsNullOrWhiteSpace(objTipo.Descricao); 

            if(validDesc) {
                throw new ObrigatoryFieldNotNullException("Descrição não pode ser vazio.");
            }
            
            try {
                _tipoDao.Inserir(objTipo);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a criação do tipo.");
            }
        }

        public List<TipoDto> ObterTodos()
        {
            try {
                var resultados = _tipoDao.ObterTodos();    
                return resultados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos tipos.");
            }
        }

        public List<TipoDto> ObterPorCategoria(string idCategoria)
        {   
            try {
                var resultados = _tipoDao.ObterPorCategoria(idCategoria);
                return resultados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos tipos.");
            }
        }

        public Tipo ObterPorId(string id)
        {
            bool validId = String.IsNullOrWhiteSpace(id); 

            if(validId) {
                throw new ObrigatoryFieldNotNullException("Id não pode ser vazio.");
            }
            
            var resultado = _tipoDao.ObterPorId(id);

            if(resultado == null) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                return resultado;
            }
        }

        public void Atualizar(string id, Tipo objTipo)
        {
            var resultado = _tipoDao.ObterPorId(id);
            bool hasAny = resultado != null;

            if(!hasAny) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                bool validDesc = String.IsNullOrWhiteSpace(objTipo.Descricao); 

                if(validDesc) {
                    throw new ObrigatoryFieldNotNullException("Descrição não pode ser vazio.");
                }

                try {
                    _tipoDao.Atualizar(id, objTipo);
                }
                catch(Exception) {
                    throw new System.Exception("Ocorreu um erro durante a atualização do tipo.");
                }
            }
        }

        public void Deletar(string id)
        {
            var resultado = _tipoDao.ObterPorId(id);
            bool hasAny = resultado != null;
            
            if(!hasAny) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                try {
                    _tipoDao.Deletar(resultado.Id);
                }
                catch(Exception) {
                    throw new System.Exception("Ocorreu um erro durante a remoção do tipo.");
                }
            }
        }
    }
}
