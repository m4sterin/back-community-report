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
    public class SetorController : ControllerBase
    {
        private readonly ISetorBll _setorBll;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public SetorController(ISetorBll setorBll, ILoggerManager logger, IMapper mapper)
        {
            _setorBll = setorBll;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Inserir")]
        public IActionResult Inserir([FromBody]Setor objSetor)
        {
            try {
                _setorBll.Inserir(objSetor);
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
        public ActionResult<List<Setor>> ObterTodos()
        {
            try {
                var resultados = _setorBll.ObterTodos();
                return Ok(new ApiOkResponse(resultados));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorPrefeitura/{idPrefeitura}")]
        public ActionResult<List<Setor>> ObterPorPrefeitura(string idPrefeitura)
        {
            try {
                var resultados = _setorBll.ObterPorPrefeitura(idPrefeitura);
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
        public ActionResult<Setor> ObterPorId(string id)
        {
            try {
                var resultado = _setorBll.ObterPorId(id);
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
        public IActionResult Atualizar(string id, Setor objSetor)
        {
            try {
                _setorBll.Atualizar(id, objSetor);
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
                _setorBll.Deletar(id);  
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