using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCA.Infraestrutura;
using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using System;
using System.Net;

namespace SCA.Modulo.Controle.Processos
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ILogger<AuthenticationController> logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="antiCSRFService"></param>
        /// <param name="logger"></param>
        public AuthenticationController(ILogger<AuthenticationController> logger, IUsuarioRepository usuarioRepository)
        {
            this.logger = logger;
            this.usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate([FromBody] Usuario model)
        {
            try
            {
                logger.LogInformation("Chamada de Authentication.Authenticate");
                // Recupera o usuário
                var user = usuarioRepository.RecuperarUsuarioPorLoginSenha(model.Login, model.Senha);
                // Verifica se o usuário existe
                if (model == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                // Gera o Token
                var token = TokenService.GenerateToken(model);

                // Oculta a senha
                user.Senha = "";

                // Retorna os dados
                return new
                {
                    user = user,
                    token = token
                };
            }
            catch (Exception ex)
            {
                logger.LogError("Erro ao autenticar", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("CriarUsuarioAdmin")]
        public ActionResult CriarUsuarioAdmin()
        {
            try
            {
                logger.LogInformation("Chamada de Authentication.CriarUsuarioAdmin");
                return StatusCode((int)HttpStatusCode.OK, usuarioRepository.CriarUsuarioAdmin());
            }
            catch (Exception ex)
            {
                logger.LogError("Erro ao criar usuário", ex);
                return BadRequest();
            }
        }

    }
}
