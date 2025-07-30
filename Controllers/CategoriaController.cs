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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaBll _categoriaBll;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public CategoriaController(ICategoriaBll categoriaBll, ILoggerManager logger, IMapper mapper)
        {
            _categoriaBll = categoriaBll;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Inserir")]
        public IActionResult Inserir([FromBody]Categoria objCategoria)
        {
            try {
                _categoriaBll.Inserir(objCategoria);
                return Ok(new ApiResponse(200, "Cadastro realizado com sucesso."));
            }
            catch(ObrigatoryFieldNotNullException ex) {
                return BadRequest(new ApiResponse(405, ex.Message));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterTodas")]
        public ActionResult<List<CategoriaDto>> ObterTodas()
        {
            try {
                var resultados = _categoriaBll.ObterTodas();
                return Ok(new ApiOkResponse(resultados));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorSetor/{idSetor}")]
        public ActionResult<List<CategoriaDto>> ObterPorSetor(string idSetor)
        {
            try {
                var resultados = _categoriaBll.ObterPorSetor(idSetor);
                return Ok(new ApiOkResponse(resultados));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorId/{id}")]
        public ActionResult<Categoria> ObterPorId(string id)
        {
            try {
                var resultado = _categoriaBll.ObterPorId(id);
                return Ok(new ApiOkResponse(resultado));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Objeto não encontrado com o id informado."));
            }
            catch(ObrigatoryFieldNotNullException ex) {
                return BadRequest(new ApiResponse(405, ex.Message));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {id}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("Atualizar/{id}")]
        public IActionResult Atualizar(string id, Categoria objCategoria)
        {
            try {
                _categoriaBll.Atualizar(id, objCategoria);
                return Ok(new ApiResponse(200, "As informações foram atualizadas com sucesso."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Objeto não encontrado com o id informado."));
            }
            catch(ObrigatoryFieldNotNullException ex) {
                return BadRequest(new ApiResponse(405, ex.Message));
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
                _categoriaBll.Deletar(id);  
                return Ok(new ApiResponse(200, "Removido com sucesso."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, $"Objeto com o id {id}, não foi encontrado."));
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