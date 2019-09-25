using Microsoft.AspNetCore.Mvc;
using SCA.ApplicationService.Interfaces;
using System.Threading.Tasks;

namespace SCA.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private IAreaAppService areaAppService;
        public AreaController(IAreaAppService areaAppService)
        {
            this.areaAppService = areaAppService;
        }


        [HttpGet]
        [Route("area")]
        public Task<string> GetArea()
        {
            return areaAppService.Teste();
        }
    }
}
