using FeriasTJ.Application.Interface;
using FeriasTJ.Infra.Interface;
using FeriasTJ.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJ.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController(ILogger<ExcelController> logger, IProcessarArquivoService processarArquivoService) : ControllerBase
    {
        private readonly IProcessarArquivoService _processarArquivoService = processarArquivoService;
        private readonly ILogger<ExcelController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ProcessarArquivo([FromForm] FileUploadModel model)
        {
            _logger.LogInformation("Iniciando o ProcessarArquivo");

            try
            {
                if (model.File == null || model.File.Length == 0)
                {
                    _logger.LogWarning("Arquivo não encontrado");
                    return BadRequest("No file uploaded.");
                }
                _processarArquivoService.ProcessarArquivo(model);

                _logger.LogInformation("ProcessarArquivo processado com sucesso.");
                return Ok("Mensagem enviada para a fila");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar o ProcessarArquivo");
                return StatusCode(500, new { message = "Erro interno no servidor." });
            }           

        }

    }
}
