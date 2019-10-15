using RabbitMQ.Client;
using System.Threading.Tasks;

namespace SCA.Service.Adapters.Interfaces
{
    public interface IRabbitMQ
    {
        void Dispose();
        Task<T> RetrieveSingleMessage<T>(string queueName);
        Task<bool> WriteMessageOnQueue(string message, string queueName);
    }
}