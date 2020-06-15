using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Job.Consumers
{
    public class OperacaoConsumer : AsyncDefaultBasicConsumer
    {
        public IOperacaoRepository repository;
        public IModel model;
        public ILogger Logger;
        public OperacaoConsumer(IModel model, IOperacaoRepository repository, ILogger logger)
        {
            this.model = model;
            this.repository = repository;
            this.Logger = logger;
        }

        public override Task HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            try
            {
                var responseMessage = repository.Adicionar(JsonConvert.DeserializeObject<Operacao>(Encoding.UTF8.GetString(body))).Result;
                if (responseMessage)
                    model.BasicAck(deliveryTag, true);
                else
                    model.BasicReject(deliveryTag, false);
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro ao processar fila", ex);
                model.BasicReject(deliveryTag, false);
            }
            return Task.FromResult(true);
        }
    }
}
