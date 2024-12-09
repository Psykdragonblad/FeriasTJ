using FeriasTJ.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJ.Infra.Interface
{
    public interface IExcelService
    {
        public void SaveData(List<string[]> data);

        public List<string[]> ReadData();

        public void ProcessarArquivo(FileUploadModel model);
    }
}
