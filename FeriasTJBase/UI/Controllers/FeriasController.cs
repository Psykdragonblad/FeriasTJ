using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJBase.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeriasController(IFeriasService feriasService, ILogger<FeriasController> logger) : ControllerBase
    {
        private readonly IFeriasService _feriasService = feriasService;
        private readonly ILogger<FeriasController> _logger = logger;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Ferias>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllFeriasComUsufruto()
        {
            _logger.LogInformation("Iniciando o GetAllFeriasComUsufruto");
            try
            {
                var listaFerias = await _feriasService.GetAllFeriasComUsufruto();

                _logger.LogInformation("GetAllFeriasComUsufruto obitido com sucesso");
                return Ok(listaFerias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar GetAllFeriasComUsufruto");
                return StatusCode(500, new { message = "Erro interno no servidor." });
            }

        }

        [HttpGet("PeriodosAquisitivos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ConsultaPeriodoAquisitivoDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllFerias()
        {
            _logger.LogInformation("Iniciando o GetAllFerias");
            try
            {
                var listaFerias = await _feriasService.GetAllFerias();

                _logger.LogInformation("GetAllFerias obitido com sucesso");
                return Ok(listaFerias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar GetAllFerias");
                return StatusCode(500, new { message = "Erro interno no servidor." });
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsultaPeriodoAquisitivoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPeriodoAquisitivoPorId(int id)
        {
            _logger.LogInformation("Iniciando o GetPeriodoAquisitivoPorId com o ID {id}",id);
            try
            {
                var periodoAquisitivo = await _feriasService.GetPeriodoAquisitivoPorIdAsync(id);

                if (periodoAquisitivo == null)
                {
                    _logger.LogWarning("Período Aquisitivo não encontrado para o ID: {id}", id);
                    return NotFound(new { message = "Período Aquisitivo não encontrado" });
                }
                _logger.LogInformation("GetPeriodoAquisitivoPorId obitido com sucesso");
                return Ok(periodoAquisitivo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar GetPeriodoAquisitivoPorId. ID: {Id}", id);
                return StatusCode(500, new { message = "Erro interno no servidor." });
            }
            
        }
    }
}
