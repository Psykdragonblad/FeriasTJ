using FeriasTJ.Domain.Entities;
using FeriasTJ.Models;

namespace FeriasTJ.Application.Interface
{
    public interface IProcessarArquivoService
    {
        void ProcessarArquivo(FileUploadModel model);
    }
}