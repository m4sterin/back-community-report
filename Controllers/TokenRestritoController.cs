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
    public class TokenRestritoController : ControllerBase
    {
        // Injeção de Dependências
        private readonly IUsuarioRestritoBll _usuarioRestritoBll;
        private ILoggerManager _logger;
        private IMapper _mapper;
        
        // Método Construtor
        public TokenRestritoController(IUsuarioRestritoBll usuarioRestritoBll, ILoggerManager logger, IMapper mapper)
        {
            _usuarioRestritoBll = usuarioRestritoBll;
            _logger = logger;
            _mapper = mapper;
        }

        //
        [AllowAnonymous]
        [HttpPost("LoginRestrito")]
        public IActionResult LoginRestrito([FromBody] LoginDto dados)
        {
            var encontrado = _usuarioRestritoBll.ObterPorLogin(dados.Login);
            var password = _usuarioRestritoBll.CriptografarSenhaSHA256(dados.Senha);

            if (encontrado != null)
            {
                if (dados.Login == encontrado.Login && password == encontrado.Senha)
                {
                    /*
                    Em caso de possuir credenciais especiais de acesso (perfil super por exemplo)
                    usar trecho abaixo
                    */
                    if (encontrado.Perfil)
                    {
                        var statusToken = GerarTokenAcesso(dados.Login, encontrado);
                        return Ok(new ObjectResult(statusToken));
                    }
                    else if (!encontrado.Perfil)
                    {
                        var statusToken = GerarTokenAcesso(dados.Login, encontrado);
                        return Ok(new ObjectResult(statusToken));
                    }
                    else
                    {
                        return BadRequest(new JsonResult("Usuário não tem permissão."));
                    }
                }
            }

            return BadRequest(new JsonResult("Usuário ou senha incorreto."));
        }

        //
        private StatusTokenRestritoDto GerarTokenAcesso(string login, UsuarioRestrito objeto)
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

            var statusToken = new StatusTokenRestritoDto {
                Id = objeto.Id,
                Nome = objeto.Nome,
                Email = objeto.Email,
                Prefeitura = objeto.Id_Prefeitura,
                Setor = objeto.Id_Setor,
                Tipo = "Usuário Restrito",
                Perfil = objeto.Perfil,
                Token = tokenString
            };

            return statusToken;
        }
    }
}