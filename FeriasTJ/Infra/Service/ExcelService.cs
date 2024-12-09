using FeriasTJ.Infra.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
namespace FeriasTJ.Infra.Service
{
    public class ExcelService : IExcelService
    {

        private readonly string _filePath;

        public ExcelService()
        {
            _filePath = Directory.GetCurrentDirectory();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
                throw;
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
    }
}
