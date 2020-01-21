using Newtonsoft.Json;
using RabbitMQ.Client;
using SCA.Model.Entities;
using SCA.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace SCA.Job.Consumers
{
    public class UserConsumer : AsyncDefaultBasicConsumer
    {
        public IUserRepository userRepository;
        public IModel model;
        public UserConsumer(IModel model, IUserRepository userRepository)
        {
            this.model = model;
            this.userRepository = userRepository;
        }

        public override async Task HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            try
            {
                var responseMessage = await userRepository.Add(JsonConvert.DeserializeObject<User>(""));
                if (responseMessage)
                    model.BasicAck(deliveryTag, true);
                else
                    model.BasicReject(deliveryTag, false);
            }
            catch (Exception ex)
            {
                model.BasicReject(deliveryTag, false);
            }
        }
    }
}
