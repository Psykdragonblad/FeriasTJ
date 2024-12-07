using FeriasTJ.Domain.Entities;
using FeriasTJ.Infra.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace FeriasTJ.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IRabbitMqEnvia _rabbitMqEnvia;

        public MessageController(IRabbitMqEnvia rabbitMqEnvia)
        {
            _rabbitMqEnvia = rabbitMqEnvia;
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] Ferias message)
        {

            _rabbitMqEnvia.SendFerias(message);
            /*var factory = new ConnectionFactory() { HostName = "localhost", Port =5672 };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var queueName = "minha-fila";
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);
            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);*/

            return Ok("Mensagem enviada para a fila");
        }
    }
}
