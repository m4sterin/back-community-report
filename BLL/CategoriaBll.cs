using System;
using System.Collections.Generic;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;
using back_community_report.DAL.DAO;
using back_community_report.BLL.Exceptions;

namespace back_community_report.BLL
{
    public class CategoriaBll : ICategoriaBll
    {
        public readonly ICategoriaDao _categoriaDao;

        public CategoriaBll(ICategoriaDao categoriaDao)
        {
            _categoriaDao = categoriaDao;
        }

        public void Inserir(Categoria objCategoria)
        {
            bool validDesc = String.IsNullOrWhiteSpace(objCategoria.Descricao); 

            if(validDesc) {
                throw new ObrigatoryFieldNotNullException("Descrição não pode ser vazio.");
            }
            
            try {
                _categoriaDao.Inserir(objCategoria);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a criação da categoria.");
            }
        }

        public List<CategoriaDto> ObterTodas()
        {
            try {
                var resultados = _categoriaDao.ObterTodas();    
                return resultados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nas categorias.");
            }
        }

        public List<CategoriaDto> ObterPorSetor(string idSetor)
        {
            try {
                var resultados = _categoriaDao.ObterPorSetor(idSetor);    
                return resultados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nas categorias.");
            }
        }

        public Categoria ObterPorId(string id)
        {
            bool validId = String.IsNullOrWhiteSpace(id); 

            if(validId) {
                throw new ObrigatoryFieldNotNullException("Id não pode ser vazio.");
            }
            
            var resultado = _categoriaDao.ObterPorId(id);

            if(resultado == null) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                return resultado;
            }
        }

        public void Atualizar(string id, Categoria objCategoria)
        {
            var resultado = _categoriaDao.ObterPorId(id);
            bool hasAny = resultado != null;

            if(!hasAny) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                bool validDesc = String.IsNullOrWhiteSpace(objCategoria.Descricao); 

                if(validDesc) {
                    throw new ObrigatoryFieldNotNullException("Descrição não pode ser vazio.");
                }

                try {
                    _categoriaDao.Atualizar(id, objCategoria);
                }
                catch(Exception) {
                    throw new System.Exception("Ocorreu um erro durante a atualização da categoria.");
                }
            }
        }

        public void Deletar(string id)
        {
            var resultado = _categoriaDao.ObterPorId(id);
            bool hasAny = resultado != null;
            
            if(!hasAny) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                try {
                    _categoriaDao.Deletar(resultado.Id);
                }
                catch(Exception) {
                    throw new System.Exception("Ocorreu um erro durante a remoção da categoria.");
                }
            }
        }
    }
}
