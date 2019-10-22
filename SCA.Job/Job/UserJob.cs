using System.Threading.Tasks;
using Quartz;
using SCA.Job.Service;

namespace SCA.Job.Job
{
    public class UserJob : IUserJob
    {
        public IUserService userService;
        public UserJob(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await userService.AddUsers();
        }
    }
}