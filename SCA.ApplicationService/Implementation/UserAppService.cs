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

        public void Add(User user)
        {
            userService.Add(user);
        }

        public Task<User> Find(string id) => userService.Find(id);

        public async Task<List<User>> FindByCriteria(UserSearchModel user)
        {
            return await userService.FindByCriteria(user);
        }
    }
}
