using FeriasTJ.Application.Interface;
using FeriasTJ.Domain.Entities;
using FeriasTJ.Infra.Interface;
using FeriasTJ.Models;

namespace FeriasTJ.Application.Service
{
    public class ProcessarArquivoService(IExcelService excelService, IRabbitMqEnvia rabbitMqEnvia, ILogger<ProcessarArquivoService> logger) : IProcessarArquivoService
    {
        private readonly IExcelService _excelService = excelService;
        private readonly IRabbitMqEnvia _rabbitMqEnvia = rabbitMqEnvia;
        private readonly ILogger<ProcessarArquivoService> _logger = logger;
        public void ProcessarArquivo(FileUploadModel model)
        {
            try
            {
                _logger.LogInformation("Iniciando o processamento do arquivo");
                var ferias = _excelService.ProcessarExcelEmFerias(model);
                _rabbitMqEnvia.EnviarFerias(ferias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar o arquivo");
                throw;
            }
        }
    }
}
