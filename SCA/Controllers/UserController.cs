using Microsoft.AspNetCore.Mvc;
using SCA.ApplicationService.Interfaces;
using SCA.Infraestrutura.Interfaces;
using SCA.Model.Entities;
using System.Threading.Tasks;

namespace SCA.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserAppService userAppService;
        private readonly IAntiCSRFService antiCSRFService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAppService"></param>
        /// <param name="antiCSRFService"></param>
        public UserController(IUserAppService userAppService, IAntiCSRFService antiCSRFService)
        {
            this.userAppService = userAppService;
            this.antiCSRFService = antiCSRFService;
        }


        [HttpPost]
        public async Task<ActionResult> Add(User user)
        {
            userAppService.Add(user);
            return new JsonResult("ok");
        }
    }
}
