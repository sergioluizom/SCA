using Microsoft.AspNetCore.Mvc;
using SCA.ApplicationService.Interfaces;
using SCA.Infraestrutura.Interfaces;
using System.Threading.Tasks;

namespace SCA.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private IAreaAppService areaAppService;
        private readonly IAntiCSRFService antiCSRFService;
        public AreaController(IAreaAppService areaAppService, IAntiCSRFService antiCSRFService)
        {
            this.areaAppService = areaAppService;
            this.antiCSRFService = antiCSRFService;
        }


        [HttpGet]
        [Route("area")]
        public Task<string> GetArea()
        {
            return areaAppService.Teste();
        }
    }
}
