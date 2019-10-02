using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model.Entities;
using SCA.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace SCA.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly Context context;
        public UserRepository(Context context)
        {
            this.context = context;
        }

        public async Task<string> Teste(User user)
        {
            try
            {
                await this.context.Users.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "ok";
        }


        public async Task<User> Get()
        {
            try
            {
                var areas = await this.context.Users.FindAsync<User>(c => c.Name == "Teste");
                return await areas.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void Add(User user)
        {
            try
            {
                this.context.Users.InsertOne(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
