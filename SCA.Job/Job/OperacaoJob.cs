using Quartz;
using System.Threading.Tasks;

namespace SCA.Job.Job
{
    public class OperacaoJob : IOperacaoJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
        }
    }
}