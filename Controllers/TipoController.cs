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
    public class TipoController : ControllerBase
    {
        private readonly ITipoBll _tipoBll;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public TipoController(ITipoBll tipoBll, ILoggerManager logger, IMapper mapper)
        {
            _tipoBll = tipoBll;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Inserir")]
        public IActionResult Inserir([FromBody]Tipo objTipo)
        {
            try {
                _tipoBll.Inserir(objTipo);
                return Ok(new ApiResponse(200, "Cadastro realizado com sucesso."));
            }
            catch(ObrigatoryFieldNotNullException ex) {
                return BadRequest(new ApiResponse(405, ex.Message));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterTodos")]
        public ActionResult<List<TipoDto>> ObterTodos()
        {
            try {
                var resultados = _tipoBll.ObterTodos();
                return Ok(new ApiOkResponse(resultados));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorCategoria/{idCategoria}")]
        public ActionResult<List<TipoDto>> ObterPorCategoria(string idCategoria)
        {
            try {
                var resultados = _tipoBll.ObterPorCategoria(idCategoria);
                return Ok(new ApiOkResponse(resultados));
            }
            catch(ObrigatoryFieldNotNullException ex) {
                return BadRequest(new ApiResponse(405, ex.Message));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorId/{id}")]
        public ActionResult<Tipo> ObterPorId(string id)
        {
            try {
                var resultado = _tipoBll.ObterPorId(id);
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
        public IActionResult Atualizar(string id, Tipo objTipo)
        {
            try {
                _tipoBll.Atualizar(id, objTipo);
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
                _tipoBll.Deletar(id);  
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