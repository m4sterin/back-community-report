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
    public class UsuarioPublicoController : ControllerBase
    {
        private readonly IUsuarioPublicoBll _usuarioPublicoBll;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public UsuarioPublicoController(IUsuarioPublicoBll usuarioPublicoBll, ILoggerManager logger, IMapper mapper)
        {
            _usuarioPublicoBll = usuarioPublicoBll;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Inserir")]
        public IActionResult Inserir([FromBody]UsuarioPublico objUsuario)
        {
            try {
                _usuarioPublicoBll.Inserir(_mapper.Map<UsuarioPublico>(objUsuario));
                return Ok(new ApiResponse(200, "Cadastro realizado com sucesso."));
            }
            catch(AlreadyExistsException) {
                return BadRequest(new ApiResponse(403, "Já existe um usuário com esse e-mail."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterTodos")]
        public ActionResult<List<UsuarioPublico>> ObterTodos()
        {
            var usuarios = _usuarioPublicoBll.ObterTodos();
            
            List<UsuarioPublico> listaUsuarios = new List<UsuarioPublico>();

            foreach(var item in usuarios)
            {
                listaUsuarios.Add(_mapper.Map<UsuarioPublico>(item));
            }

            return Ok(new ApiOkResponse(listaUsuarios));
        }

        [HttpGet("ObterPorLogin/{login}")]
        public ActionResult<UsuarioPublico> ObterPorLogin(string login)
        {
            try {
                var usuario = _usuarioPublicoBll.ObterPorLogin(login);
                return Ok(new ApiOkResponse(_mapper.Map<UsuarioPublico>(usuario)));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "Login não pode estar vazio."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Usuário não encontrado com o login informado"));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpGet("ObterPorEmail/{email}")]
        public ActionResult<UsuarioPublico> ObterPorEmail(string email)
        {
            try {
                var usuario = _usuarioPublicoBll.ObterPorEmail(email);
                return Ok(new ApiOkResponse(_mapper.Map<UsuarioPublico>(usuario)));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "E-mail não pode estar vazio."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Usuário não encontrado com o e-mail informado"));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("Atualizar/{id}")]
        public IActionResult Atualizar(string id, UsuarioPublico objUsuario)
        {
            try {
                _usuarioPublicoBll.Atualizar(id, objUsuario);
                return Ok(new ApiResponse(200, "As informações foram atualizadas com sucesso."));
            }
            catch(AlreadyExistsException) {
                return BadRequest(new ApiResponse(403, "Já existe um usuário com esse e-mail."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {id}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("AtualizarSenha/{id}/{senhaAntiga}/{senhaNova}")]
        public IActionResult AtualizarSenha(string id, string senhaAntiga, string senhaNova)
        {
            try {
                _usuarioPublicoBll.AtualizarSenha(id, senhaAntiga, senhaNova);
                return Ok(new ApiResponse(200, "A sua senha foi redefinida com sucesso."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Usuário não encontrado com o ID informado"));
            }
            catch(ObrigatoryFieldNotNullException ex) {
                return BadRequest(new ApiResponse(405, ex.Message));
            }
            catch(IncorrectPasswordException ex){
                return BadRequest(new ApiResponse(405, ex.Message));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"Id informado {id}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }

        [HttpPut("RedefinirEndereco/{id}")]
        public IActionResult RedefinirEndereco(string id, EnderecoDto endereco)
        {
            try {
                _usuarioPublicoBll.RedefinirEndereco(id, endereco);
                return Ok(new ApiResponse(200, "O endereço foi registrado com sucesso."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, "Usuário não encontrado com o ID informado"));
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
                _usuarioPublicoBll.Deletar(id);  
                return Ok(new ApiResponse(200, "Conta removida com sucesso."));
            }
            catch(NotFoundException) {
                return NotFound(new ApiResponse(404, $"Usuário com o id {id}, não foi encontrado."));
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