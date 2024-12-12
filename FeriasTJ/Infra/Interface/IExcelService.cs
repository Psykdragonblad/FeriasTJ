using FeriasTJ.Domain.Entities;
using FeriasTJ.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeriasTJ.Infra.Interface
{
    public interface IExcelService
    {
        public void SaveData(List<string[]> data);

        public List<string[]> ReadData();

        public Ferias ProcessarExcelEmFerias(FileUploadModel model);
    }
}
