using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;
using back_community_report.BLL;
using back_community_report.BLL.Exceptions;
using back_community_report.Utils;
using back_community_report.Extensions.Responses;
using MongoDB.Driver;

namespace back_community_report.Controllers
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenPublicoController : ControllerBase
    {
        private readonly IUsuarioPublicoBll _usuarioPublicoBll;
        private ILoggerManager _logger;
        private IMapper _mapper;
        
        public TokenPublicoController(IUsuarioPublicoBll usuarioPublicoBll, ILoggerManager logger, IMapper mapper)
        {
            _usuarioPublicoBll = usuarioPublicoBll;
            _logger = logger;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("LoginPublico")]
        public IActionResult LoginPublico([FromBody] LoginDto dados)
        {
            var encontradoByLogin = _usuarioPublicoBll.ObterPorLogin(dados.Login);
            var password = _usuarioPublicoBll.CriptografarSenhaSHA256(dados.Senha);
            // var encontradoByCpf = _usuarioPublicoBll.ObterUsuarioPorLogin(dados.Login);

            if (encontradoByLogin != null)
            {
                if (dados.Login == encontradoByLogin.Login && password == encontradoByLogin.Senha)
                {
                    // Gerar Token de Acesso
                    var statusToken = GerarTokenAcesso(dados.Login, encontradoByLogin);
                    return Ok(new ObjectResult(statusToken));
                }
                else {
                    return BadRequest(new JsonResult("Usuário ou senha incorreto."));
                }
            }
            /* else if (encontradoByCpf != null) {
                if (dados.Login == encontradoByCpf.Login && dados.Senha == encontradoByCpf.Senha)
                {
                    // Gerar Token de Acesso
                    var statusToken = GerarTokenAcesso(dados.Login, encontradoByCpf);
                    return Ok(new ObjectResult(statusToken));
                }
            } */

            return BadRequest(new JsonResult("Não foi encontrado um usuário com este e-mail."));
        }

        private StatusTokenPublicoDto GerarTokenAcesso(string login, UsuarioPublico objeto)
        {
            var claims = new Claim[]
            {
                // Define as claims 
                new Claim("Name", objeto.Nome),
                new Claim("Email", objeto.Email),
                new Claim("Id", objeto.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            // Gera o token
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HJaM5dvQy5WUEQ6LPR7yRrcO4m2Mse4u94FMsgXtMjrc66XeM34sdPWQ2ilEA9fo"));
            SigningCredentials signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtHeader jwtHeader = new JwtHeader(signingCredential);
            JwtPayload jwtPayload = new JwtPayload(claims);
            JwtSecurityToken token = new JwtSecurityToken(jwtHeader, jwtPayload);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var statusToken = new StatusTokenPublicoDto {
                Nome = objeto.Nome,
                Email = objeto.Email,
                Cep = objeto.Endereco.CEP,
                Prefeitura = objeto.Prefeitura,
                Tipo = "Usuário Público",
                Id = objeto.Id,
                Token = tokenString
            };

            return statusToken;
        }
    }
}