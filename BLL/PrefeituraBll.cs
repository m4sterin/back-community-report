using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using back_community_report.DAL.Models; 
using back_community_report.DAL.DAO;
using back_community_report.BLL.Exceptions;

namespace back_community_report.BLL
{
    public class PrefeituraBll : IPrefeituraBll
    {
        public readonly IPrefeituraDao _prefeituraDao;

        public PrefeituraBll(IPrefeituraDao prefeituraDao)
        {
            _prefeituraDao = prefeituraDao;
        }

        public void Inserir(Prefeitura objPrefeitura)
        {
            bool hasAnyPref = (_prefeituraDao.ObterPorNome(objPrefeitura.Nome)) != null;

            if (!hasAnyPref) {
                try {
                    _prefeituraDao.Inserir(objPrefeitura);
                }
                catch(Exception) {
                    throw new System.Exception("Ocorreu um erro durante a criação da prefeitura.");
                }
            }
            else {
                throw new AlreadyExistsException("Não foi possível efetuar a inserção.");
            }
        }

        public List<Prefeitura> ObterTodas()
        {
            try {
                var prefeituras = _prefeituraDao.ObterTodas();    
                return prefeituras;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nas prefeituras.");
            }
        }

        public Prefeitura ObterPorNome(string nome)
        {
            bool validNome = String.IsNullOrWhiteSpace(nome); 

            if(validNome){
                throw new ArgumentException("Nome não pode ser vazio. ObterPorNome() BLL falhou !");
            }
            
            var prefeitura = _prefeituraDao.ObterPorNome(nome);

            if(prefeitura == null){
                throw new NotFoundException("Prefeitura não encontrada.");
            }
            else {
                return prefeitura;
            }
        }

        public Prefeitura ObterPorId(string id)
        {
            bool validId = String.IsNullOrWhiteSpace(id); 

            if(validId) {
                throw new ArgumentException("Id não pode ser vazio. ObterPorId() BLL falhou !");
            }
            
            var prefeitura = _prefeituraDao.ObterPorId(id);

            if(prefeitura == null) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                return prefeitura;
            }
        }

        public void Atualizar(string id, Prefeitura objPrefeitura)
        {
            var prefeitura = _prefeituraDao.ObterPorNome(objPrefeitura.Nome);
            bool hasAnyPref = prefeitura != null;

            if(!hasAnyPref) {
                try {
                    _prefeituraDao.Atualizar(id, objPrefeitura);
                }
                catch(DbUpdateConcurrencyException e) {
                    throw new DbConcurrencyException(e.Message);
                }
            } 
            else {
                if(prefeitura.Id == id) {
                    try {
                        _prefeituraDao.Atualizar(id, objPrefeitura);
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

        public void Deletar(string id)
        {
            var prefeitura = _prefeituraDao.ObterPorId(id);
            bool hasAnyPref = prefeitura != null;
            
            if(!hasAnyPref) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                try {
                    _prefeituraDao.Deletar(prefeitura.Id);
                }
                catch(DbUpdateException) {
                    throw new DbUpdateException("Não foi possível efetuar a remoção.");
                }
            }
        }
    }
}
