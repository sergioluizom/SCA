using System.Threading.Tasks;
using SCA.ApplicationService.Interfaces;
using SCA.Service.Interfaces;

namespace SCA.ApplicationService.Implementation
{
    public class AreaAppService : IAreaAppService
    {
        private readonly IAreaService areaService;
        public AreaAppService(IAreaService areaService)
        {
            this.areaService = areaService;
        }
        public async Task<string> Teste()
        {
            return await areaService.Teste();
        }
    }
}
