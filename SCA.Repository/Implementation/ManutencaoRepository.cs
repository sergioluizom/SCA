using MongoDB.Bson;
using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace SCA.Repository.Implementation
{
    public class ManutencaoRepository : IManutencaoRepository
    {
        private readonly Context context;
        public ManutencaoRepository(Context context)
        {
            this.context = context;
        }

        public bool Adicionar(Manutencao entity)
        {
            context.Manutencaos.InsertOneAsync(entity).GetAwaiter().GetResult();
            return true;
        }

        public bool Atualizar(Manutencao entity)
        {
            var result = context.Manutencaos.ReplaceOneAsync(x => x.Id == entity.Id, entity).GetAwaiter().GetResult();
            return result.IsAcknowledged;
        }

        public bool Excluir(string id)
        {
            var result = context.Manutencaos.DeleteOneAsync(x => x.Id == id).GetAwaiter().GetResult();
            return result.IsAcknowledged;
        }

        public bool Liberar(string id)
        {
            var entity = ObterPorId(id);
            entity.Status = Model.Enums.StatusManutencaoEnum.Liberado;
            var result = context.Manutencaos.ReplaceOneAsync(x => x.Id == entity.Id, entity).GetAwaiter().GetResult();
            return result.IsAcknowledged;
        }

        public Manutencao ObterPorId(string id)
        {
            var result = context.Manutencaos.FindAsync(x => x.Id == id).GetAwaiter().GetResult();
            return result.FirstOrDefault();
        }

        public List<Manutencao> ObterTodos()
        {
            return context.Manutencaos.FindAsync(new BsonDocument()).GetAwaiter().GetResult().ToList();
        }

        public List<Manutencao> ObterConcluidas()
        {
            return context.Manutencaos.FindAsync(x=> x.Status == Model.Enums.StatusManutencaoEnum.Concluida).GetAwaiter().GetResult().ToList();
        }

        public List<Manutencao> ObterCadastradas()
        {
            return context.Manutencaos.FindAsync(x => x.Status == Model.Enums.StatusManutencaoEnum.Cadastrada).GetAwaiter().GetResult().ToList();
        }
    }
}
