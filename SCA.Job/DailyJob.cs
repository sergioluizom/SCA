using System.Threading.Tasks;
using Quartz;

namespace SCA.Job
{
    public class DailyJob : IDailyJob
    {
        public IHelperService helperService;
        public DailyJob(IHelperService helperService)
        {
            this.helperService = helperService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await helperService.PerformService("");
        }
    }
}