using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using SCA.Service.Adapters.Interfaces;
using SCA.Service.Interfaces;
using System.Collections.Generic;

namespace SCA.Service.Implementation
{
    public class OperacaoService : IOperacaoService
    {
        private readonly IOperacaoRepository repository;
        private IRabbitMQ rabbitMQ;
        private const string queueOperacao = "QueueOperacao";
        public OperacaoService(IOperacaoRepository repository, IRabbitMQ rabbitMQ)
        {
            this.repository = repository;
            this.rabbitMQ = rabbitMQ;
        }

        public void Adicionar(Operacao entity)
        {
            rabbitMQ.WriteMessageOnQueue( JsonConvert.SerializeObject(entity), queueOperacao);
        }

        public bool Atualizar(Operacao entity)
        {
            return repository.Atualizar(entity);
        }

        public bool Excluir(string id)
        {
            return repository.Excluir(id);
        }

        public Operacao ObterPorId(string id)
        {
            return repository.ObterPorId(id);
        }

        public List<Operacao> ObterTodos()
        {
            return repository.ObterTodos();
        }
    }
}