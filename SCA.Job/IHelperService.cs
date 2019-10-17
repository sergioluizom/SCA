using System.Threading.Tasks;

namespace SCA.Job
{
    public interface IHelperService
    {
        Task PerformService(string schedule);
    }
}