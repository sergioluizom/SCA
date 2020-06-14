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

        public List<Operacao> Filtrar()
        {
            //try
            //{
            //    var filtro = Builders<User>.Filter.Exists(x => x.Id);
            //    if (!string.IsNullOrEmpty(user.DocumentNumber))
            //        filtro &= Builders<User>.Filter.Where(x => x.DocumentNumber.Contains(user.DocumentNumber));

            //    if (!string.IsNullOrEmpty(user.Email))
            //        filtro &= Builders<User>.Filter.Where(x => x.Email.ToLower().Contains(user.Email.ToLower()));

            //    if (!string.IsNullOrEmpty(user.Name))
            //        filtro &= Builders<User>.Filter.Where(x => x.Name.ToLower().Contains(user.Name.ToLower()));

            //    if (!string.IsNullOrEmpty(user.JobFunction))
            //        filtro &= Builders<User>.Filter.Where(x => x.JobFunction.ToLower().Contains(user.JobFunction.ToLower()));

            //    if (user.UserStatus.HasValue)
            //        filtro &= Builders<User>.Filter.Eq(x => x.Status, user.UserStatus);
            //    return await context.Users.Find(filtro).ToListAsync();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return new List<Operacao>();
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
