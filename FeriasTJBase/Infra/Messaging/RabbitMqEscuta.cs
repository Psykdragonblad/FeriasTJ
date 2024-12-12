using FeriasTJBase.Application.Interface;
using FeriasTJBase.Domain.Entities;
using FeriasTJBase.Domain.Interface;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace FeriasTJBase.Infra.Messaging
{
    public class RabbitMqEscuta : BackgroundService
    {
        private readonly string _queueName = "minha-fila";
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDescriptografiaService _descriptografiaService;
        private readonly ILogger<RabbitMqEscuta> _logger;

        public RabbitMqEscuta(IServiceScopeFactory serviceScopeFactory, IDescriptografiaService descriptografiaService, ILogger<RabbitMqEscuta> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _descriptografiaService = descriptografiaService;
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _logger = logger;
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                
                _logger.LogInformation("Mensagem recebida da fila: {Message}", message);

                try
                {
                    //Descriptografar a mensagem
                    var descriptografado = _descriptografiaService.Descriptar(message);
                    var ferias = JsonConvert.DeserializeObject<Ferias>(descriptografado);

                    if (ferias != null)
                    {
                        _logger.LogInformation("Mensagem descriptografada e deserializada com sucesso. Processando dados de férias.");


                        // Cria um novo escopo para resolver IFeriasRepository
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var feriasRepository = scope.ServiceProvider.GetRequiredService<IFeriasRepository>();
                            await feriasRepository.SalvarFerias(ferias);

                            _logger.LogInformation("Dados de férias salvos com sucesso no repositório.");
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Falha ao desserializar a mensagem para o tipo 'Ferias'. Mensagem: {Message}", message);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar a mensagem: {Message}", message);
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _logger.LogInformation("Fechando a conexão e o canal do RabbitMQ.");
            _channel.Close();
            _channel.Dispose();
            _connection.Close();
            base.Dispose();
        }
    }
}