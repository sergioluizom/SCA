using MongoDB.Bson;
using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace SCA.Repository.Implementation
{
    public class ParadaRepository : IParadaRepository
    {
        private readonly Context context;
        public ParadaRepository(Context context)
        {
            this.context = context;
        }

        public bool Adicionar(Parada entity)
        {
            context.Paradas.InsertOneAsync(entity).GetAwaiter().GetResult();
            return true;
        }

        public bool Atualizar(Parada entity)
        {
            var result = context.Paradas.ReplaceOneAsync(x => x.Id == entity.Id, entity).GetAwaiter().GetResult();
            return result.IsAcknowledged;
        }

        public bool Excluir(string id)
        {
            var result = context.Paradas.DeleteOneAsync(x => x.Id == id).GetAwaiter().GetResult();
            return result.IsAcknowledged;
        }

        public List<Parada> Filtrar()
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
            return new List<Parada>();
        }
       
        public Parada ObterPorId(string id)
        {
            var result = context.Paradas.FindAsync(x => x.Id == id).GetAwaiter().GetResult();
            return result.FirstOrDefault();
        }

        public List<Parada> ObterTodos()
        {
            return context.Paradas.FindAsync(new BsonDocument()).GetAwaiter().GetResult().ToList();
        }       
    }
}
