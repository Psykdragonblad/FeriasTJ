using FeriasTJ.Application.Interface;
using FeriasTJ.Domain.Entities;
using FeriasTJ.Infra.Interface;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace FeriasTJ.Infra.Messaging
{
    public class RabbitMqEnvia : IRabbitMqEnvia
    {
        private readonly ICriptografiaSerivce _criptografiaSerivce;

        public RabbitMqEnvia(ICriptografiaSerivce criptografiaSerivce)
        {
            _criptografiaSerivce = criptografiaSerivce;
        }

        public void SendFerias(Ferias ferias)
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
        }
    }
}
