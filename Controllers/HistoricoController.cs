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
    public class HistoricoController : ControllerBase
    {
        private readonly IHistoricoBll _historicoBll;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public HistoricoController(IHistoricoBll historicoBll, ILoggerManager logger, IMapper mapper)
        {
            _historicoBll = historicoBll;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("ObterPorChamado/{idChamado}")]
        public ActionResult<List<HistoricoDto>> ObterPorChamado(string idChamado)
        {
            try {
                var resultado = _historicoBll.ObterPorChamado(idChamado);
                return Ok(new ApiOkResponse(resultado));
            }
            catch(ArgumentException) {
                return BadRequest(new ApiResponse(403, "ID não pode estar vazio."));
            }
            catch(FormatException) {
                return BadRequest(new ApiResponse(406, $"ID informado {idChamado}, não é válido."));
            }
            catch(Exception) {
                return BadRequest(new ApiResponse(500, "Ops! Ocorreu um erro."));
            }
        }
    }
}