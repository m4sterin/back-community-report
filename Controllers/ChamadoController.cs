using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;
using back_community_report.BLL;
using back_community_report.BLL.Exceptions;
using back_community_report.Utils;
using back_community_report.Extensions.Responses;

namespace back_community_report.Controllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadoController : ControllerBase
    {
        // Injeção de Dependências
        private readonly IChamadoBll _chamadoBll;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public ChamadoController(IChamadoBll chamadoBll, ILoggerManager logger, IMapper mapper)
        {
            _chamadoBll = chamadoBll;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Inserir")]
        public IActionResult Inserir([FromBody]Chamado objChamado)
        {
            try {
                _chamadoBll.Inserir(objChamado);
                return Ok(new ApiResponse(200, "Cadastro realizado com sucesso."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterTodos")]
        public ActionResult<List<ChamadoDto>> ObterTodos()
        {
            var chamados = _chamadoBll.ObterTodos();
            
            List<ChamadoDto> listaChamados = new List<ChamadoDto>();

            foreach(var item in chamados)
            {
                listaChamados.Add(_mapper.Map<ChamadoDto>(item));
            }

            return Ok(new ApiOkResponse(listaChamados));
        }

        [HttpGet("ObterPorPrefeitura/{idPrefeitura}")]
        public ActionResult<List<ChamadoDto>> ObterPorPrefeitura(string idPrefeitura)
        {
            try {
                var chamados = _chamadoBll.ObterPorPrefeitura(idPrefeitura);
                
                List<ChamadoDto> listaChamados = new List<ChamadoDto>();

                foreach(var item in chamados)
                {
                    listaChamados.Add(_mapper.Map<ChamadoDto>(item));
                }

                return Ok(new ApiOkResponse(listaChamados));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }
        
        [HttpGet("ObterPorSetor/{idPrefeitura}/{idSetor}")]
        public ActionResult<List<ChamadoDto>> ObterPorSetor(string idPrefeitura, string idSetor)
        {
            try {
                var chamados = _chamadoBll.ObterPorSetor(idPrefeitura, idSetor);
                
                List<ChamadoDto> listaChamados = new List<ChamadoDto>();

                foreach(var item in chamados)
                {
                    listaChamados.Add(_mapper.Map<ChamadoDto>(item));
                }

                return Ok(new ApiOkResponse(listaChamados));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorUsuarioPublico/{idUsuario}")]
        public ActionResult<List<ChamadoDto>> ObterPorUsuarioPublico(string idUsuario)
        {
            try {
                var chamados = _chamadoBll.ObterPorUsuarioPublico(idUsuario);
                
                List<ChamadoDto> listaChamados = new List<ChamadoDto>();

                foreach(var item in chamados)
                {
                    listaChamados.Add(_mapper.Map<ChamadoDto>(item));
                }

                return Ok(new ApiOkResponse(listaChamados));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorCategoria/{idCategoria}")]
        public ActionResult<List<ChamadoDto>> ObterPorCategoria(string idCategoria)
        {
            try {
                var chamados = _chamadoBll.ObterPorCategoria(idCategoria);
                
                List<ChamadoDto> listaChamados = new List<ChamadoDto>();

                foreach(var item in chamados)
                {
                    listaChamados.Add(_mapper.Map<ChamadoDto>(item));
                }

                return Ok(new ApiOkResponse(listaChamados));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorId/{id}")]
        public ActionResult<Chamado> ObterPorId(string id)
        {
            try {
                var chamado = _chamadoBll.ObterPorId(id);
                return Ok(new ApiOkResponse(_mapper.Map<Chamado>(chamado)));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "Id não pode estar vazio."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Chamado não encontrada com o id informado."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {id}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("Atualizar/{id}")]
        public IActionResult Atualizar(string id, Chamado objChamado)
        {
            try {
                _chamadoBll.Atualizar(id, objChamado);
                return Ok(new ApiResponse(200, "As informações foram atualizadas com sucesso."));
            }
            catch(DbUpdateConcurrencyException) {
                return BadRequest(new ApiResponse(405, "Ocorreu um erro durante a requisição. Tente novamente."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {id}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("EmAtendimento")]
        public IActionResult EmAtendimento(DadosGerenciaDto dados)
        {
            try {
                _chamadoBll.EmAtendimento(dados);
                return Ok(new ApiResponse(200, "Status do chamado alterado com sucesso."));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "Id não pode estar vazio."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Chamado não encontrado com o id informado."));
            }
            catch(DbUpdateConcurrencyException) {
                return BadRequest(new ApiResponse(405, "Ocorreu um erro durante a requisição. Tente novamente."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {dados.idChamado}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("Finalizado")]
        public IActionResult Finalizado(DadosGerenciaDto dados)
        {
            try {
                _chamadoBll.Finalizado(dados);
                return Ok(new ApiResponse(200, "Chamado finalizado com sucesso."));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "Id não pode estar vazio."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Chamado não encontrado com o id informado."));
            }
            catch(DbUpdateConcurrencyException) {
                return BadRequest(new ApiResponse(405, "Ocorreu um erro durante a requisição. Tente novamente."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {dados.idChamado}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("EnviadoOutroSetor")]
        public IActionResult EnviadoOutroSetor(DadosGerenciaDto dados)
        {
            try {
                _chamadoBll.EnviadoOutroSetor(dados);
                return Ok(new ApiResponse(200, "Chamado encaminhado com sucesso."));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "Id não pode estar vazio."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Chamado não encontrado com o id informado."));
            }
            catch(DbUpdateConcurrencyException) {
                return BadRequest(new ApiResponse(405, "Ocorreu um erro durante a requisição. Tente novamente."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {dados.idChamado}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("Rejeitado")]
        public IActionResult Rejeitado(DadosGerenciaDto dados)
        {
            try {
                _chamadoBll.Rejeitado(dados);
                return Ok(new ApiResponse(200, "O chamado foi rejeitado."));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "Id não pode estar vazio."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Chamado não encontrado com o id informado."));
            }
            catch(DbUpdateConcurrencyException) {
                return BadRequest(new ApiResponse(405, "Ocorreu um erro durante a requisição. Tente novamente."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {dados.idChamado}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpDelete("Deletar/{id}")]
        public IActionResult Deletar(string id)
        {
            try {
                _chamadoBll.Deletar(id);  
                return Ok(new ApiResponse(200, "Chamado removido com sucesso."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, $"Chamado com o id {id}, não foi encontrado."));
            }
            catch(DbUpdateException) {
                return BadRequest(new ApiResponse(405, "Ocorreu um erro durante a requisição. Tente novamente."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {id}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("TotalChamados/{idPrefeitura}")]
        public ActionResult<Chamado> TotalChamados(string idPrefeitura)
        {
            try {
                var total = _chamadoBll.TotalChamados(idPrefeitura);
                return Ok(new ApiOkResponse(total));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {idPrefeitura}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("TotalChamadosEmAnalise/{idPrefeitura}")]
        public ActionResult<Chamado> TotalChamadosEmAnalise(string idPrefeitura)
        {
            try {
                var total = _chamadoBll.TotalChamadosEmAnalise(idPrefeitura);
                return Ok(new ApiOkResponse(total));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {idPrefeitura}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("TotalChamadosEmAtendimento/{idPrefeitura}")]
        public ActionResult<Chamado> TotalChamadosEmAtendimento(string idPrefeitura)
        {
            try {
                var total = _chamadoBll.TotalChamadosEmAtendimento(idPrefeitura);
                return Ok(new ApiOkResponse(total));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {idPrefeitura}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("TotalChamadosFinalizados/{idPrefeitura}")]
        public ActionResult<Chamado> TotalChamadosFinalizados(string idPrefeitura)
        {
            try {
                var total = _chamadoBll.TotalChamadosFinalizados(idPrefeitura);
                return Ok(new ApiOkResponse(total));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {idPrefeitura}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }
    }
}