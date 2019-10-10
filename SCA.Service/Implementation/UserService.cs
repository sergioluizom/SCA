using System.Collections.Generic;
using System.Threading.Tasks;
using SCA.Model.Entities;
using SCA.Model.SearchModel;
using SCA.Repository.Interfaces;
using SCA.Service.Interfaces;

namespace SCA.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Add(User user)
        {
            userRepository.Add(user);
        }

        public Task<User> Find(string id) => userRepository.Find(id);

        public async Task<List<User>> FindByCriteria(UserSearchModel user)
        {
            return await userRepository.FindByCriteria(user);
        }
    }
}
