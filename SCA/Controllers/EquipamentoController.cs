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

    public class EquipamentoController : ControllerBase
    {
        private readonly IEquipamentoService service;
        private readonly IAntiCSRFService antiCSRFService;
        private readonly ILogger<EquipamentoController> logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="antiCSRFService"></param>
        /// <param name="logger"></param>
        public EquipamentoController(IEquipamentoService service, IAntiCSRFService antiCSRFService, ILogger<EquipamentoController> logger)
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
        public ActionResult Adicionar([FromBody] Equipamento entity)
        {
            logger.LogInformation($"Chamada de Equipamento.Adicionar");
            try
            {
                var result = service.Adicionar(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao adicionar o equipamento.");
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
        public ActionResult Atualizar([FromBody] Equipamento entity)
        {
            logger.LogInformation($"Chamada de Equipamento.Atualizar");
            try
            {
                var result = service.Atualizar(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao atualizar o equipamento.");
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
        [ProducesResponseType(typeof(Equipamento), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult ObterPorId([FromQuery] string id)
        {
            logger.LogInformation($"Chamada de Equipamento.ObterPorId");
            try
            {
                return StatusCode((int)HttpStatusCode.OK, service.ObterPorId(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao obter o equipamento.");
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
            logger.LogInformation($"Chamada de Equipamento.Excluir");
            try
            {
                var result = service.Excluir(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao remover o equipamento.");
                return BadRequest();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Filtrar")]
        [ProducesResponseType(typeof(List<Equipamento>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "admin")]
        public ActionResult Filtrar(string id)
        {
            logger.LogInformation($"Chamada de Equipamento.Filtrar");
            try
            {
                return StatusCode((int)HttpStatusCode.OK, service.Filtrar(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao filtrar o equipamento.");
                return BadRequest();
            }
        }
    }
}
