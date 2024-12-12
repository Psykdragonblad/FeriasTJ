using FeriasTJ.Domain.Entities;
using FeriasTJ.Infra.Interface;
using FeriasTJ.Models;
using OfficeOpenXml;
namespace FeriasTJ.Application.Service
{
    public class ExcelService : IExcelService
    {

        private readonly string _filePath;
       // private readonly ILogger<ExcelService> _logger;
        //private readonly IRabbitMqEnvia _rabbitMqEnvia;

        public ExcelService(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _filePath = filePath;
            //_rabbitMqEnvia = rabbitMqEnvia;
           // _logger = logger;
        }
        // Método para salvar arquivo no servidor.
        public void SaveFile(IFormFile file, string fileName = "modelo.xls")
        {

            if (file == null)
                throw new Exception("Arquivo não recebido.");

            try
            {
                string dirPath = _filePath;
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                string dataFileName = Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(dataFileName);

                string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };

                if (!allowedExtsnions.Contains(extension))
                    throw new Exception("Extensão não permitida. Arquivos deve conter a extensão .xls ou.xlsx");


                string saveToPath = Path.Combine(dirPath, fileName);

                using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // Método para salvar dados em um arquivo Excel
        public void SaveData(List<string[]> data)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        worksheet.Cells[i + 1, j + 1].Value = data[i][j]; // Preenche a célula
                    }
                }

                FileInfo fi = new FileInfo(_filePath);
                package.SaveAs(fi);
            }
        }

        // Método para ler dados de um arquivo Excel
        public List<string[]> ReadData()
        {
            var result = new List<string[]>();

            using (var package = new ExcelPackage(new FileInfo(_filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lê a primeira planilha

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 1; row <= rowCount; row++)
                {
                    var rowData = new string[colCount];
                    for (int col = 1; col <= colCount; col++)
                    {
                        rowData[col - 1] = worksheet.Cells[row, col].Text; // Lê o conteúdo da célula
                    }
                    result.Add(rowData);
                }
            }

            return result;
        }

        public Ferias ProcessarExcelEmFerias(FileUploadModel model)
        {

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
                    usufruto.Status = worksheet.Cells[row, 8].Text == "Ativo" ? true : false;
                    ferias.Usufrutos.Add(usufruto);
                }
            }
            return ferias;
            
        }

    }
}
