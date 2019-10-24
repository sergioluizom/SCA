using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SCA.Model.Entities;
using SCA.Model.SearchModel;
using SCA.Repository.Interfaces;
using SCA.Service.Adapters.Interfaces;
using SCA.Service.Interfaces;

namespace SCA.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRabbitMQ rabbitMQ;
        private const string QueueUserAdd = "QueueUserAdd";
        public UserService(IUserRepository userRepository, IRabbitMQ rabbitMQ)
        {
            this.userRepository = userRepository;
            this.rabbitMQ = rabbitMQ;
        }

        public async Task<bool> AddUserQueue(User user)
        {
            var result = rabbitMQ.WriteMessageOnQueue(JsonConvert.SerializeObject(user), QueueUserAdd);
            return await result;
        }

        public async Task<bool> Add()
        {
            var user = await rabbitMQ.RetrieveSingleMessage<User>(QueueUserAdd);
            rabbitMQ.Dispose();
            return await userRepository.Add(user);
        }

        public async Task<bool> Update(User user) => await userRepository.Update(user);
        public async Task<bool> Delete(string id) => await userRepository.Delete(id);
        public async Task<User> Find(string id) => await userRepository.Find(id);
        public async Task<List<User>> FindByCriteria(UserSearchModel user) => await userRepository.FindByCriteria(user);

    }
}
