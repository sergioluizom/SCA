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
        private readonly IRabbitMQ rabbitMQ;
        private const string QueueUserAdd = "QueueUserAdd";
        private const string QueueUserUpdate = "QueueUserUpdate";
        private const string QueueUserDelete = "QueueUserDelete";
        public UserService(IUserRepository userRepository, IRabbitMQ rabbitMQ)
        {
            this.userRepository = userRepository;
            this.rabbitMQ = rabbitMQ;
        }

        public async Task<bool> Add(User user) => await rabbitMQ.WriteMessageOnQueue(JsonConvert.SerializeObject(user), QueueUserAdd);
        public async Task<bool> Update(User user) => await rabbitMQ.WriteMessageOnQueue(JsonConvert.SerializeObject(user), QueueUserUpdate);
        public async Task<bool> Delete(string id) => await rabbitMQ.WriteMessageOnQueue(JsonConvert.SerializeObject(id), QueueUserDelete);
        public async Task<User> Find(string id) => await userRepository.Find(id);
        public async Task<List<User>> FindByCriteria(UserSearchModel user) => await userRepository.FindByCriteria(user);
    }
}