using FeriasTJ.Domain.Entities;
using FeriasTJ.Infra.Interface;
using FeriasTJ.Infra.Service;
using FeriasTJ.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FeriasTJ.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelService _excelService;
        private readonly IRabbitMqEnvia _rabbitMqEnvia;

        public ExcelController(IExcelService excelService, IRabbitMqEnvia rabbitMqEnvia)
        {
            _excelService = excelService;
            _rabbitMqEnvia = rabbitMqEnvia;
        }

        [HttpPost]
        public async Task<IActionResult> LerArquivo([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
                return BadRequest("No file uploaded.");
            var ferias = new Ferias();


            using (var package = new ExcelPackage(new FileInfo(model.File.FileName)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lê a primeira planilha

                ferias.Matricula = Convert.ToInt32(worksheet.Name.Substring(0, 7));

                ferias.PeriodoAquisitivoInicial = Convert.ToDateTime(worksheet.Cells[2, 3].Text);
                ferias.PeriodoAquisitivoFinal = Convert.ToDateTime(worksheet.Cells[2, 6].Text);
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 4; row <= rowCount; row++)
                {
                    var usufruto = new Usufruto();
                    usufruto.UsufrutoInicial = Convert.ToDateTime(worksheet.Cells[row, 3].Text);
                    usufruto.UsufrutoFinal = Convert.ToDateTime(worksheet.Cells[row, 6].Text);
                    usufruto.Status = (worksheet.Cells[row, 8].Text == "Ativo") ? true : false;
                    ferias.Usufrutos.Add(usufruto);
                }


            }



            _rabbitMqEnvia.SendFerias(ferias);

            //List<string[]> data = new List<string[]>();
            //string[] periodo = { message.PeriodoAquisitivoFinal.ToString() };

            //data.Add(periodo);
            //var jsonMessage = JsonConvert.SerializeObject(message);
            //_excelService.SaveData(jsonMessage);
            return Ok("Mensagem enviada para a fila");

        }

    }
}
