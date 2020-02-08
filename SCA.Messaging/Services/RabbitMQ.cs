using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using SCA.Messaging.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Messaging.Services
{
    public class RabbitMQ : IRabbitMQ
    {
        private readonly IConfiguration configuration;
        private IConnection connection;
        public RabbitMQ(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void CreateConnection()
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = configuration["RabbitMQHost"];
            connectionFactory.UserName = configuration["RabbitMQUserName"];
            connectionFactory.Password = configuration["RabbitMQPassword"];
            connection = connectionFactory.CreateConnection();
        }

        public Task<IModel> CreateModel()
        {
            CreateConnection();
            return Task.FromResult(connection.CreateModel());
        }

        private QueueDeclareOk CreateQueue(IModel channel, string queueName)
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
        public Task<bool> WriteMessageOnQueue(IModel channel, string message, string queueName)
        {
            queueName = configuration[queueName];
            CreateQueue(channel, queueName);
            channel.BasicPublish(string.Empty, queueName, null, Encoding.ASCII.GetBytes(message));
            return Task.FromResult(true);
        }
    }
}
