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

        public Equipamento ObterPorId(string id)
        {
            var user = context.Equipamentos.FindAsync(x => x.Id == id).GetAwaiter().GetResult();
            return user.FirstOrDefault();
        }

    }
}
