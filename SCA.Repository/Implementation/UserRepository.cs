using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model.Entities;
using SCA.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SCA.Model.SearchModel;

namespace SCA.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly Context context;
        public UserRepository(Context context)
        {
            this.context = context;
        }

        public async Task<List<User>> FindByCriteria(UserSearchModel user)
        {
            try
            {
                var filtro = Builders<User>.Filter.Exists(x => x.Id);
                if (!string.IsNullOrEmpty(user.DocumentNumber))
                    filtro &= Builders<User>.Filter.Where(x => x.DocumentNumber.Contains(user.DocumentNumber));

                if (!string.IsNullOrEmpty(user.Email))
                    filtro &= Builders<User>.Filter.Where(x => x.Email.ToLower().Contains(user.Email.ToLower()));

                if (!string.IsNullOrEmpty(user.Name))
                    filtro &= Builders<User>.Filter.Where(x => x.Name.ToLower().Contains(user.Name.ToLower()));

                if (!string.IsNullOrEmpty(user.JobFunction))
                    filtro &= Builders<User>.Filter.Where(x => x.JobFunction.ToLower().Contains(user.JobFunction.ToLower()));

                if (user.UserStatus.HasValue)
                    filtro &= Builders<User>.Filter.Eq(x => x.Status, user.UserStatus);
                return await context.Users.Find(filtro).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> Add(User user)
        {
            await context.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<bool> Update(User user)
        {
            var result = await context.Users.ReplaceOneAsync(x => x.Id == user.Id, user);
            return result.IsAcknowledged;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await context.Users.DeleteOneAsync(x => x.Id == id);
            return result.IsAcknowledged;
        }

        public async Task<User> Find(string id)
        {
            var user = await context.Users.FindAsync(x => x.Id == id);
            return user.FirstOrDefault();
        }
    }
}