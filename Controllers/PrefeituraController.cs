using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using back_community_report.DAL.Models;
using back_community_report.BLL;
using back_community_report.BLL.Exceptions;
using back_community_report.Utils;
using back_community_report.Extensions.Responses;

namespace back_community_report.Controllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PrefeituraController : ControllerBase
    {
        // Injeção de Dependências
        private readonly IPrefeituraBll _prefeituraBll;
        private ILoggerManager _logger;
        private IMapper _mapper;
        
        public PrefeituraController(IPrefeituraBll prefeituraBll, ILoggerManager logger, IMapper mapper)
        {
            _prefeituraBll = prefeituraBll;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Inserir")]
        public IActionResult Inserir([FromBody]Prefeitura objPrefeitura)
        {
            try {
                _prefeituraBll.Inserir(objPrefeitura);
                return Ok(new ApiResponse(200, "Cadastro realizado com sucesso."));
            }
            catch(AlreadyExistsException) {
                return BadRequest(new ApiResponse(403, "Já existe uma Prefeitura com esse nome."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterTodas")]
        public ActionResult<List<Prefeitura>> ObterTodas()
        {
            var prefeituras = _prefeituraBll.ObterTodas();
            
            List<Prefeitura> listaPrefeitura = new List<Prefeitura>();

            foreach(var item in prefeituras)
            {
                listaPrefeitura.Add(_mapper.Map<Prefeitura>(item));
            }

            return Ok(new ApiOkResponse(listaPrefeitura));
        }

        [HttpGet("ObterPorNome/{nomePrefeitura}")]
        public ActionResult<Prefeitura> ObterPorNome(string nomePrefeitura)
        {
            try {
                var prefeitura = _prefeituraBll.ObterPorNome(nomePrefeitura);
                return Ok(new ApiOkResponse(prefeitura));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "Nome não pode estar vazio."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Prefeitura não encontrada com o nome informado."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorId/{id}")]
        public ActionResult<Prefeitura> ObterPorId(string id)
        {
            try {
                var prefeitura = _prefeituraBll.ObterPorId(id);
                return Ok(new ApiOkResponse(prefeitura));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "Id não pode estar vazio."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Prefeitura não encontrada com o id informado."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {id}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("Atualizar/{id}")]
        public IActionResult Atualizar(string id, Prefeitura objPrefeitura)
        {
            try {
                _prefeituraBll.Atualizar(id, objPrefeitura);
                return Ok(new ApiResponse(200, "As informações foram atualizadas com sucesso."));
            }
            catch(AlreadyExistsException) {
                return BadRequest(new ApiResponse(403, "Já existe uma Prefeitura com esse nome."));
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

        [HttpDelete("Deletar/{id}")]
        public IActionResult Deletar(string id)
        {
            try {
                _prefeituraBll.Deletar(id);  
                return Ok(new ApiResponse(200, "Prefeitura removida com sucesso."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, $"Prefeitura com o id {id}, não foi encontrada."));
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
    }
}