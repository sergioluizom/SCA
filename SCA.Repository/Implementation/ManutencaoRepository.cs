using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;

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

        public Manutencao Filtrar(string id)
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
            return new Manutencao();
        }

        public Manutencao ObterPorId(string id)
        {
            var user = context.Manutencaos.FindAsync(x => x.Id == id).GetAwaiter().GetResult();
            return user.FirstOrDefault();
        }

    }
}
