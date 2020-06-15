using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using SCA.Service.Adapters.Interfaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCA.Service.Adapters.Consumers
{
    public class OperacaoConsumerRabbitService : BackgroundService
    {
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQ rabbitMQ;
        private IOperacaoRepository operacaoRepository;
        private IConfiguration configuration;

        public OperacaoConsumerRabbitService(ILoggerFactory loggerFactory, IRabbitMQ rabbitMQ, IOperacaoRepository operacaoRepository, IConfiguration configuration)
        {
            this._logger = loggerFactory.CreateLogger<OperacaoConsumerRabbitService>();
            this.rabbitMQ = rabbitMQ;
            _channel = this.rabbitMQ.GetChannel();
            this.operacaoRepository = operacaoRepository;
            this.configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                var content = JsonConvert.DeserializeObject<Operacao>(Encoding.UTF8.GetString(ea.Body));

                // handle the received message  
                operacaoRepository.Adicionar(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(configuration["QueueOperacao"], false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {
            // we just print this message   
            _logger.LogInformation($"consumer received {content}");
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}