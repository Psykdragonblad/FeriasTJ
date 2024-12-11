using FeriasTJBase.Application.Dtos.Ferias;
using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJBase.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeriasController(IFeriasService feriasService) : ControllerBase
    {
        private readonly IFeriasService _feriasService = feriasService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Ferias>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllFeriasComUsufruto()
        {
            var listaFerias = await _feriasService.GetAllFeriasComUsufruto();

            return Ok(listaFerias);
        }

        [HttpGet("PeriodosAquisitivos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ConsultaPeriodoAquisitivoDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllFerias()
        {
            var listaFerias = await _feriasService.GetAllFerias();

            return Ok(listaFerias);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsultaPeriodoAquisitivoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPeriodoAquisitivoPorId(int id)
        {
            var periodoAquisitivo = await _feriasService.GetPeriodoAquisitivoPorIdAsync(id);

            if (periodoAquisitivo == null)
            {
                return NotFound(new { message = "Período Aquisitivo não encontrado" });
            }

            return Ok(periodoAquisitivo);
        }
    }
}
