using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using System;

namespace SCA.Repository.Implementation
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        private readonly Context context;
        public EquipamentoRepository(Context context)
        {
            this.context = context;
        }

        public bool Adicionar(Equipamento entity)
        {
            context.Equipamentos.InsertOneAsync(entity).GetAwaiter().GetResult();
            return true;
        }

        public bool Atualizar(Equipamento entity)
        {
            var result = context.Equipamentos.ReplaceOneAsync(x => x.Id == entity.Id, entity).GetAwaiter().GetResult();
            return result.IsAcknowledged;
        }

        public bool Excluir(string id)
        {
            var result = context.Equipamentos.DeleteOneAsync(x => x.Id == id).GetAwaiter().GetResult();
            return result.IsAcknowledged;
        }

        public Equipamento Filtrar(string id)
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
            return new Equipamento();
        }

        public Equipamento ObterPorId(string id)
        {
            var user = context.Equipamentos.FindAsync(x => x.Id == id).GetAwaiter().GetResult();
            return user.FirstOrDefault();
        }

    }
}
