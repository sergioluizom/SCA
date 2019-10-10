using System.Collections.Generic;
using System.Threading.Tasks;
using SCA.ApplicationService.Interfaces;
using SCA.Model.Entities;
using SCA.Model.SearchModel;
using SCA.Service.Interfaces;

namespace SCA.ApplicationService.Implementation
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService userService;
        public UserAppService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<User> Add(User user) => await userService.Add(user);
        public async Task<bool> Update(User user) => await userService.Update(user);
        public async Task<bool> Delete(string id) => await userService.Delete(id);
        public async Task<User> Find(string id) => await userService.Find(id);
        public async Task<List<User>> FindByCriteria(UserSearchModel user) => await userService.FindByCriteria(user);
    }
}
