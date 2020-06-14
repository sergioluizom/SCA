using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCA.Infraestrutura.Interfaces;
using SCA.Model.Entidades;
using SCA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;

namespace SCA.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class ManutencaoController : ControllerBase
    {
        private readonly IManutencaoService service;
        private readonly IAntiCSRFService antiCSRFService;
        private readonly ILogger<EquipamentoController> logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="antiCSRFService"></param>
        /// <param name="logger"></param>
        public ManutencaoController(IManutencaoService service, IAntiCSRFService antiCSRFService, ILogger<EquipamentoController> logger)
        {
            this.service = service;
            this.antiCSRFService = antiCSRFService;
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Adicionar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult Adicionar([FromBody] Manutencao entity)
        {
            logger.LogInformation($"Chamada de Manutencao.Adicionar");
            try
            {
                service.Adicionar(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao adicionar a manutenção.");
                return BadRequest();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult Atualizar([FromBody] Manutencao entity)
        {
            logger.LogInformation($"Chamada de Manutencao.Atualizar");
            try
            {
                service.Atualizar(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao atualizar a manutenção.");
                return BadRequest();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorId/{id}")]
        [ProducesResponseType(typeof(Manutencao), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult ObterPorId([FromQuery] string id)
        {
            logger.LogInformation($"Chamada de Manutencao.ObterPorId");
            try
            {
                return StatusCode((int)HttpStatusCode.OK, service.ObterPorId(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao obter a manutenção.");
                return BadRequest();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Excluir/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult Excluir([FromRoute] string id)
        {
            logger.LogInformation($"Chamada de Manutencao.Excluir");
            try
            {
                service.Excluir(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao remover a manutenção.");
                return BadRequest();
            }
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterTodos")]
        [ProducesResponseType(typeof(List<Manutencao>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult ObterTodos()
        {
            logger.LogInformation($"Chamada de Manutencao.ObterTodos");
            try
            {
                return StatusCode((int)HttpStatusCode.OK, service.ObterTodos());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao ObterTodos.");
                return BadRequest();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterConcluidas")]
        [ProducesResponseType(typeof(List<Manutencao>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult ObterConcluidas()
        {
            logger.LogInformation($"Chamada de Manutencao.ObterConcluidas");
            try
            {
                return StatusCode((int)HttpStatusCode.OK, service.ObterConcluidas());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao ObterConcluidas.");
                return BadRequest();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterCadastradas")]
        [ProducesResponseType(typeof(List<Manutencao>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult ObterCadastradas()
        {
            logger.LogInformation($"Chamada de Manutencao.ObterCadastradas");
            try
            {
                return StatusCode((int)HttpStatusCode.OK, service.ObterCadastradas());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao ObterCadastradas.");
                return BadRequest();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Liberar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult Liberar(string id)
        {
            logger.LogInformation($"Chamada de Manutencao.Liberar");
            try
            {
                service.Liberar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao liberar a manutenção.");
                return BadRequest();
            }
        }
    }
}
