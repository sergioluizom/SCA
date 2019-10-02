using SCA.ApplicationService.Interfaces;
using SCA.Model.Entities;
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
    }
}
