using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCA.Infraestrutura.Interfaces;
using SCA.Model.Entidades;
using SCA.Model.Error;
using SCA.Service.Interfaces;
using System;
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
        public ActionResult Adicionar([FromBody] Equipamento entity)
        {
            logger.LogInformation($"Chamada de Equipamento.Adicionar feita por {antiCSRFService.Login}");
            try
            {
                var result = service.Adicionar(entity);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao adicionar o equipamento.");
                var resp = new InternalServerErrorAnswer("99", "Erro ao adicionar o equipamento.", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar")]
        public ActionResult Atualizar([FromBody] Equipamento entity)
        {
            logger.LogInformation($"Chamada de Equipamento.Atualizar feita por {antiCSRFService.Login}");
            try
            {
                var result = service.Atualizar(entity);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao atualizar o equipamento.");
                var resp = new InternalServerErrorAnswer("99", "Erro ao atualizar o equipamento.", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorId/{id}")]
        public ActionResult ObterPorId([FromQuery] string id)
        {
            logger.LogInformation($"Chamada de Equipamento.ObterPorId feita por {antiCSRFService.Login}");
            try
            {
                var result = service.ObterPorId(id);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao obter o equipamento.");
                var resp = new InternalServerErrorAnswer("99", "Erro ao obter o equipamento.", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Excluir/{id}")]
        public ActionResult Excluir([FromQuery] string id)
        {
            logger.LogInformation($"Chamada de Equipamento.Excluir feita por {antiCSRFService.Login}");
            try
            {
                var result = service.Excluir(id);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao remover o equipamento.");
                var resp = new InternalServerErrorAnswer("99", "Erro ao remover o equipamento.", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Filtrar")]
        public ActionResult Filtrar(string id)
        {
            logger.LogInformation($"Chamada de Equipamento.Filtrar feita por {antiCSRFService.Login}");
            try
            {
                var result = service.Filtrar(id);
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao filtrar o equipamento.");
                var resp = new InternalServerErrorAnswer("99", "Erro ao filtrar o equipamento.", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, resp);
            }
        }
    }
}
