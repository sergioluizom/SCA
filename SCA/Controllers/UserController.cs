using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SCA.Infraestrutura.Interfaces;
using SCA.Model.CustomModel;
using SCA.Model.Entities;
using SCA.Model.Error;
using SCA.Model.SearchModel;
using SCA.Service.Interfaces;
using SCA.Utils.Http;
using System;
using System.Collections.Generic;
using System.Net;
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
        private IConfiguration configuration;
        private IUserService userService;
        private readonly IAntiCSRFService antiCSRFService;
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="antiCSRFService"></param>
        /// <param name="logger"></param>
        public UserController(IUserService userService, IAntiCSRFService antiCSRFService, ILogger<UserController> logger, IConfiguration configuration)
        {
            this.userService = userService;
            this.antiCSRFService = antiCSRFService;
            this.logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] User user)
        {
            logger.LogInformation($"Chamada de User.Add feita por {antiCSRFService.Login}");
            try
            {
                var result = await ClientApi.PostApiAsync("UserAdd", JsonConvert.SerializeObject(user), configuration, antiCSRFService);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao realizar o cadastro de usuário.");
                var resp = new InternalServerErrorAnswer("99", "Erro ao realizar o cadastro de usuário, tente novamente mais tarde.", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] User user)
        {
            logger.LogInformation($"Chamada de User.Update feita por {antiCSRFService.Login}");
            try
            {
                var result = await userService.Update(user);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao atualizar os dados do usuário.");
                var resp = new InternalServerErrorAnswer("99", "Erro ao atualizar os dados do usuário.", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            logger.LogInformation($"Chamada de User.Delete feita por {antiCSRFService.Login}");
            try
            {
                var result = await userService.Delete(id);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao remover o usuário.");
                var resp = new InternalServerErrorAnswer("99", "Erro ao remover o usuário.", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }

        /// <summary>
        /// Solicitação de Acesso ao sistema
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("RequestAccess")]
        [HttpPost]
        public async Task<ActionResult> RequestAccess(UserRequestAccess user)
        {
            logger.LogInformation($"Chamada de User.RequestAccess feita por {antiCSRFService.Login}");
            try
            {
                var result = await userService.Add(UserRequestAccess.GenerateUser(user));
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao realizar solicitar acesso.");
                var resp = new InternalServerErrorAnswer("99", "Erro ao realizar solicitar acesso.", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }

        /// <summary>
        /// Pesquisar usuários
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Search")]
        [HttpPost]
        public async Task<ActionResult> FindByCriteria(UserSearchModel user)
        {
            logger.LogInformation($"Chamada de User.FindByCriteria feita por {antiCSRFService.Login}");
            try
            {
                var result = await userService.FindByCriteria(user);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao realizar a busca");
                var resp = new InternalServerErrorAnswer("99", "Erro ao realizar a busca", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }

        /// <summary>
        /// Obter usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Find")]
        [HttpGet]
        public async Task<ActionResult> Find(string id)
        {
            logger.LogInformation($"Chamada de User.Find feita por {antiCSRFService.Login}");
            try
            {
                var result = await userService.Find(id);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar dados do usuário");
                var resp = new InternalServerErrorAnswer("99", "Erro ao buscar dados do usuário", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }
    }
}