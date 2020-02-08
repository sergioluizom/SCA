using RabbitMQ.Client;
using System.Threading.Tasks;

namespace SCA.Messaging.Interfaces
{
    public interface IRabbitMQ
    {
        Task<bool> WriteMessageOnQueue(IModel channel, string message, string queueName);
        Task<IModel> CreateModel();
    }
}