using Newtonsoft.Json;
using SCA.Model.Entities;
using SCA.Model.SearchModel;
using SCA.Repository.Interfaces;
using SCA.Service.Adapters.Interfaces;
using SCA.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCA.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Add(User user)
        {
            
            await userRepository.Add(user);
        }
        public async Task<bool> Update(User user) => await userRepository.Update(user);
        public async Task<bool> Delete(string id) => await userRepository.Delete(id);
        public async Task<User> Find(string id) => await userRepository.Find(id);
        public async Task<List<User>> FindByCriteria(UserSearchModel user) => await userRepository.FindByCriteria(user);
    }
}