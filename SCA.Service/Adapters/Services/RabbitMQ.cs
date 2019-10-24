using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SCA.Service.Adapters.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Service.Adapters.Services
{
    public class RabbitMQ : IRabbitMQ
    {
        IConfiguration configuration;
        IConnection connection;
        IModel channel;
        private ulong tag = 0;
        public RabbitMQ(IConfiguration configuration)
        {
            this.configuration = configuration;
            CreateConnection();
            channel = connection.CreateModel();
        }
        private void CreateConnection()
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = configuration["RabbitMQHost"];
            connectionFactory.UserName = configuration["RabbitMQUserName"];
            connectionFactory.Password = configuration["RabbitMQPassword"];
            connection = connectionFactory.CreateConnection();
        }

        private QueueDeclareOk CreateQueue(string queueName)
        {
            QueueDeclareOk queue;
            queue = channel.QueueDeclare(queueName, false, false, false, null);
            return queue;
        }

        /// <summary>
        /// Inserir objeto na fila
        /// </summary>
        /// <param name="message"></param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public async Task<bool> WriteMessageOnQueue(string message, string queueName)
        {
            queueName = configuration[queueName];
            CreateQueue(queueName);
            channel.BasicPublish(string.Empty, queueName, null, Encoding.ASCII.GetBytes(message));
            return true;
        }

        /// <summary>
        /// Recuperar objeto da fila
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public async Task<T> RetrieveSingleMessage<T>(string queueName)
        {
            BasicGetResult data;
            queueName = configuration[queueName];
            data = channel.BasicGet(queueName, false);
            tag = data.DeliveryTag;
            var result = data != null ? Encoding.UTF8.GetString(data.Body) : null;


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (channel, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                tag = ea.DeliveryTag;
            };

            return JsonConvert.DeserializeObject<T>(result);
        }

        public void Dispose()
        {
            channel.BasicAck(tag, false);
            channel.Dispose();
        }
    }
}
