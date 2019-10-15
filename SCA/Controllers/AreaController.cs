using Microsoft.AspNetCore.Mvc;
using SCA.Infraestrutura.Interfaces;
using SCA.Service.Interfaces;
using System.Threading.Tasks;

namespace SCA.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private IAreaService areaService;
        private readonly IAntiCSRFService antiCSRFService;
        public AreaController(IAreaService areaService, IAntiCSRFService antiCSRFService)
        {
            this.areaService = areaService;
            this.antiCSRFService = antiCSRFService;
        }


        [HttpGet]
        [Route("area")]
        public Task<string> GetArea()
        {
            return areaService.Teste();
        }
    }
}
