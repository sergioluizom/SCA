using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCA.Infraestrutura.Interfaces;
using SCA.Model.Entidades;
using SCA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;

namespace SCA.Modulo.Controle.Processos
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class OperacaoController : ControllerBase
    {
        private readonly IOperacaoService service;
        private readonly IAntiCSRFService antiCSRFService;
        private readonly ILogger<OperacaoController> logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="antiCSRFService"></param>
        /// <param name="logger"></param>
        public OperacaoController(IOperacaoService service, IAntiCSRFService antiCSRFService, ILogger<OperacaoController> logger)
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
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize(Roles = "admin")]
        public ActionResult Adicionar([FromBody] Operacao entity)
        {
            logger.LogInformation($"Chamada de Operacao.Adicionar");
            try
            {
                service.Adicionar(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao adicionar a operação.");
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
        public ActionResult Atualizar([FromBody] Operacao entity)
        {
            logger.LogInformation($"Chamada de Operacao.Atualizar");
            try
            {
                var result = service.Atualizar(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao atualizar a operação.");
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
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult ObterPorId([FromQuery] string id)
        {
            logger.LogInformation($"Chamada de Operacao.ObterPorId");
            try
            {
                return StatusCode((int)HttpStatusCode.OK, service.ObterPorId(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao obter a operação.");
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
            logger.LogInformation($"Chamada de Operacao.Excluir");
            try
            {
                var result = service.Excluir(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao remover a operação.");
                return BadRequest();
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterTodos")]
        [ProducesResponseType(typeof(List<Operacao>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult ObterTodos()
        {
            logger.LogInformation($"Chamada de Operacao.ObterTodos");
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
    }
}
