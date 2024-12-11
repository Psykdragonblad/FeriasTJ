using FeriasTJ.Infra.Interface;
using FeriasTJ.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJ.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController(IExcelService excelService) : ControllerBase
    {
        private readonly IExcelService _excelService = excelService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProcessarArquivo([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
                return BadRequest("No file uploaded.");
         
            _excelService.ProcessarArquivo(model);
            return Ok("Mensagem enviada para a fila");

        }

    }
}
