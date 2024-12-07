using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace FeriasTJBase.Infra.Messaging
{
    public class RabbitMqEscuta : BackgroundService
    {
        private readonly string _queueName = "minha-fila";
        private IModel _channel;
        private IConnection _connection;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDescriptografiaService _descriptografiaService;

        public RabbitMqEscuta(IServiceScopeFactory serviceScopeFactory, IDescriptografiaService descriptografiaService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _descriptografiaService = descriptografiaService;
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                //Descriptografar a mensagem
                var descriptografado = _descriptografiaService.Descriptar(message);
                var ferias = JsonConvert.DeserializeObject<Ferias>(descriptografado);

                if (ferias != null)
                {
                    //Console.WriteLine($"Entrou: {message}");

                    // Cria um novo escopo para resolver IFeriasRepository
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var feriasRepository = scope.ServiceProvider.GetRequiredService<IFeriasRepository>();
                        await feriasRepository.SalvarFerias(ferias);
                    }
                }
                //Console.WriteLine($"Mensagem recebida: {message}");
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _channel.Dispose();
            _connection.Close();
            base.Dispose();
        }
    }
}