using MongoDB.Bson;
using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCA.Repository.Implementation
{
    public class OperacaoRepository : IOperacaoRepository
    {
        private readonly Context context;
        public OperacaoRepository(Context context)
        {
            this.context = context;
        }

        public Task<bool> Adicionar(Operacao entity)
        {
            context.Operacaos.InsertOneAsync(entity).GetAwaiter().GetResult();
            return Task.FromResult(true);
        }

        public bool Atualizar(Operacao entity)
        {
            var result = context.Operacaos.ReplaceOneAsync(x => x.Id == entity.Id, entity).GetAwaiter().GetResult();
            return result.IsAcknowledged;
        }

        public bool Excluir(string id)
        {
            var result = context.Operacaos.DeleteOneAsync(x => x.Id == id).GetAwaiter().GetResult();
            return result.IsAcknowledged;
        }
               
        public Operacao ObterPorId(string id)
        {
            var result = context.Operacaos.FindAsync(x => x.Id == id).GetAwaiter().GetResult();
            return result.FirstOrDefault();
        }

        public List<Operacao> ObterTodos()
        {
            return context.Operacaos.FindAsync(new BsonDocument()).GetAwaiter().GetResult().ToList();
        }       
    }
}
