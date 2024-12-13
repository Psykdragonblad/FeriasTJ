using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJBase.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsufrutoController(IUsufrutoService usufrutoService, ILogger<UsufrutoController> logger) : ControllerBase
    {
        private readonly IUsufrutoService _usufrutoService = usufrutoService;
        private readonly ILogger<UsufrutoController> _logger = logger;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Usufruto>))]
        public async Task<IActionResult> GetAllUsufruto() 
        {
            _logger.LogInformation("Iniciando o GetAllUsufruto");
            try
            {
                var listaUsufruto = await _usufrutoService.GetAllUsufruto();

                _logger.LogInformation("GetAllUsufruto obitido com sucesso");
                return Ok(listaUsufruto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Erro ao executar GetAllUsufruto");
                return StatusCode(500, new { message = "Erro interno no servidor."});
            }
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Usufruto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsufrutoById(int id)
        {
            _logger.LogInformation("Iniciando o GetUsufrutoById com o ID {id}", id);
            try
            {
                var usufruto = await _usufrutoService.GetUsufrutoPeloId(id);

                if (usufruto == null)
                {
                    _logger.LogWarning("Usufruto não encontrado para o ID: {id}", id);
                    return NotFound(new { message = "Usufruto não encontrado" });
                }

                _logger.LogInformation("GetUsufrutoById obitido com sucesso");
                return Ok(usufruto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Erro ao executar GetUsufrutoById. ID: {id}", id);
                return StatusCode(500, new { message = "Erro interno no servidor." });
            }
           
        }
    }
}
