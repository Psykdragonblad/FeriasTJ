using FeriasTJ.Application.Interface;
using FeriasTJ.Domain.Entities;
using FeriasTJ.Infra.Interface;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace FeriasTJ.Infra.Messaging
{
    public class RabbitMqEnvia(ICriptografiaSerivce criptografiaSerivce, ILogger<RabbitMqEnvia> logger) : IRabbitMqEnvia
    {
        private readonly ICriptografiaSerivce _criptografiaSerivce = criptografiaSerivce;
        private readonly ILogger<RabbitMqEnvia> _logger = logger;

        public void EnviarFerias(Ferias ferias)
        {
            _logger.LogInformation("Iniciando o EnviarFerias");
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "minha-fila", durable: false, exclusive: false, autoDelete: false, arguments: null);

                // Serializa o objeto Ferias em JSON
                var jsonMessage = JsonConvert.SerializeObject(ferias);

                // Criptogragar a mensagem
                var encriptado = _criptografiaSerivce.Encriptar(jsonMessage);
                var body = Encoding.UTF8.GetBytes(encriptado);
                
                channel.BasicPublish(exchange: "", routingKey: "minha-fila", basicProperties: null, body: body);

                _logger.LogInformation("Mensagem Enviada com sucesso para o rabbitmq");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar a mensagem para o rabbitmq");
                throw new Exception(ex.Message);
            }            
        }
    }
}
