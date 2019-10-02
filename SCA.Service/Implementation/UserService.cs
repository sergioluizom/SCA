using SCA.Model.Entities;
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
    }
}
