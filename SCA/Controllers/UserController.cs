using Microsoft.AspNetCore.Mvc;
using SCA.ApplicationService.Interfaces;
using SCA.Infraestrutura.Interfaces;
using SCA.Model.CustomModel;
using SCA.Model.Entities;
using SCA.Model.SearchModel;
using System.Collections.Generic;
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
        /// <param name = "userAppService" ></param>
        /// <param name="antiCSRFService"></param>
        public UserController(IUserAppService userAppService, IAntiCSRFService antiCSRFService)
        {
            this.userAppService = userAppService;
            this.antiCSRFService = antiCSRFService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<User> Add([FromBody] User user) => await userAppService.Add(user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        public async Task<bool> Update([FromBody] User user) => await userAppService.Update(user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpDelete]
        public async Task<bool> Delete(string id) => await userAppService.Delete(id);

        /// <summary>
        /// Solicitação de Acesso ao sistema
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("RequestAccess")]
        [HttpPost]
        public async Task<ActionResult> RequestAccess(UserRequestAccess user)
        {
            userAppService.Add(UserRequestAccess.GenerateUser(user));
            return new JsonResult("ok");
        }
        /// <summary>
        /// Pesquisar usuários
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Search")]
        [HttpPost]
        public async Task<List<User>> FindByCriteria(UserSearchModel user) => await userAppService.FindByCriteria(user);

        /// <summary>
        /// Obter usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Find")]
        [HttpGet]
        public async Task<User> Find(string id) => await userAppService.Find(id);
    }
}
