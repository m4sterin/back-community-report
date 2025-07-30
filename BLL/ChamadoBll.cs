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
    public class ChamadoBll : IChamadoBll
    {
        public readonly IChamadoDao _chamadoDao;

        public ChamadoBll(IChamadoDao chamadoDao)
        {
            _chamadoDao = chamadoDao;
        }

        public void Inserir(Chamado objChamado)
        {
            try {
                _chamadoDao.Inserir(objChamado);
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a criação do chamado.");
            }
        }

        public List<ChamadoDto> ObterTodos()
        {
            try {
                var chamados = _chamadoDao.ObterTodos();    
                return chamados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos chamados.");
            }
        }

        public List<ChamadoDto> ObterPorPrefeitura(string idPrefeitura)
        {
            try {
                var chamados = _chamadoDao.ObterPorPrefeitura(idPrefeitura);
                return chamados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos chamados.");
            }
        }

        public List<ChamadoDto> ObterPorSetor(string idPrefeitura, string idSetor)
        {
            try {
                var chamados = _chamadoDao.ObterPorSetor(idPrefeitura, idSetor);
                return chamados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos chamados.");
            }
        }

        public List<ChamadoDto> ObterPorUsuarioPublico(string idUsuario)
        {
            try {
                var chamados = _chamadoDao.ObterPorUsuarioPublico(idUsuario);
                return chamados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos chamados.");
            }
        }

        public List<ChamadoDto> ObterPorCategoria(string idCategoria)
        {
            try {
                var chamados = _chamadoDao.ObterPorCategoria(idCategoria);
                return chamados;
            }
            catch(Exception) {
                throw new System.Exception("Ocorreu um erro durante a consulta nos chamados.");
            }
        }

        public Chamado ObterPorId(string id)
        {
            bool validId = String.IsNullOrWhiteSpace(id); 

            if(validId) {
                throw new ArgumentException("Id não pode ser vazio. ObterPorId() BLL falhou !");
            }
            
            var chamado = _chamadoDao.ObterPorId(id);

            if(chamado == null) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                return chamado;
            }
        }

        public void Atualizar(string id, Chamado objChamado)
        {
            var chamado = _chamadoDao.ObterPorId(id);
            bool hasAnyChamado = chamado != null;

            if(!hasAnyChamado) {
                throw new NotFoundException("Id não encontrado.");
            } 
            else {
                try {
                    _chamadoDao.Atualizar(id, objChamado);
                }
                catch(DbUpdateConcurrencyException e) {
                    throw new DbConcurrencyException(e.Message);
                }
            }
        }

        public void EmAtendimento(DadosGerenciaDto dados)
        {
            bool validId = String.IsNullOrWhiteSpace(dados.idChamado); 

            if(validId) {
                throw new ArgumentException("Id não pode ser vazio. EmAtendimento() BLL falhou !");
            }
            
            var chamado = _chamadoDao.ObterPorId(dados.idChamado);
            bool hasAnyChamado = chamado != null;

            if(!hasAnyChamado) {
                throw new NotFoundException("Id não encontrado. EmAtendimento() BLL falhou !");
            }
            else {
                try {
                    _chamadoDao.EmAtendimento(dados);
                }
                catch(DbUpdateConcurrencyException e) {
                    throw new DbConcurrencyException(e.Message);
                }
            }
        }

        public void Finalizado(DadosGerenciaDto dados)
        {
            bool validId = String.IsNullOrWhiteSpace(dados.idChamado); 

            if(validId) {
                throw new ArgumentException("Id não pode ser vazio. Finalizado() BLL falhou !");
            }
            
            var chamado = _chamadoDao.ObterPorId(dados.idChamado);
            bool hasAnyChamado = chamado != null;

            if(!hasAnyChamado) {
                throw new NotFoundException("Id não encontrado. Finalizado() BLL falhou !");
            } 
            else {
                try {
                    _chamadoDao.Finalizado(dados);
                }
                catch(DbUpdateConcurrencyException e) {
                    throw new DbConcurrencyException(e.Message);
                }
            }
        }

        public void EnviadoOutroSetor(DadosGerenciaDto dados)
        {
            bool validId = String.IsNullOrWhiteSpace(dados.idChamado); 

            if(validId) {
                throw new ArgumentException("Id não pode ser vazio. EnviadoOutroSetor() BLL falhou !");
            }
            
            var chamado = _chamadoDao.ObterPorId(dados.idChamado);
            bool hasAnyChamado = chamado != null;

            if(!hasAnyChamado) {
                throw new NotFoundException("Id não encontrado. EnviadoOutroSetor() BLL falhou !");
            } 
            else {
                try {
                    _chamadoDao.EnviadoOutroSetor(dados);
                }
                catch(DbUpdateConcurrencyException e) {
                    throw new DbConcurrencyException(e.Message);
                }
            }
        }

        public void Rejeitado(DadosGerenciaDto dados)
        {
            bool validId = String.IsNullOrWhiteSpace(dados.idChamado); 

            if(validId) {
                throw new ArgumentException("Id não pode ser vazio. Rejeitado() BLL falhou !");
            }
            
            var chamado = _chamadoDao.ObterPorId(dados.idChamado);
            bool hasAnyChamado = chamado != null;

            if(!hasAnyChamado) {
                throw new NotFoundException("Id não encontrado. Rejeitado() BLL falhou !");
            } 
            else {
                try {
                    _chamadoDao.Rejeitado(dados);
                }
                catch(DbUpdateConcurrencyException e) {
                    throw new DbConcurrencyException(e.Message);
                }
            }
        }

        public void Deletar(string id)
        {
            var chamado = _chamadoDao.ObterPorId(id);
            bool hasAnyChamado = chamado != null;
            
            if(!hasAnyChamado) {
                throw new NotFoundException("Id não encontrado.");
            }
            else {
                try {
                    _chamadoDao.Deletar(chamado.Id);
                }
                catch(DbUpdateException) {
                    throw new DbUpdateException("Não foi possível efetuar a remoção.");
                }
            }
        }

        public int TotalChamados(string idPrefeitura)
        {
            return _chamadoDao.TotalChamados(idPrefeitura);
        }
        public int TotalChamadosEmAnalise(string idPrefeitura)
        {
            return _chamadoDao.TotalChamadosEmAnalise(idPrefeitura);
        }
        public int TotalChamadosEmAtendimento(string idPrefeitura)
        {
            return _chamadoDao.TotalChamadosEmAtendimento(idPrefeitura);
        }
        public int TotalChamadosFinalizados(string idPrefeitura)
        {
            return _chamadoDao.TotalChamadosFinalizados(idPrefeitura);
        }
    }
}
