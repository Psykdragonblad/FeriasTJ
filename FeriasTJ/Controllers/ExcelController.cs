using FeriasTJ.Infra.Interface;
using FeriasTJ.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJ.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessarArquivo([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
                return BadRequest("No file uploaded.");
         
            _excelService.ProcessarArquivo(model);
            return Ok("Mensagem enviada para a fila");

        }

    }
}
