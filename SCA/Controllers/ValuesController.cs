using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Model;
using SCA.Repository;
using SCA.Repository.Implementation;

namespace SCA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// Teste
        /// </summary>
        /// <returns>Retorna lista</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            AreaRepository areaRepository = new AreaRepository(new Infraestrutura.Context()) ;
            areaRepository.Teste();
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("area")]
        public ActionResult<Area> GetArea()
        {
            AreaRepository areaRepository = new AreaRepository(new Infraestrutura.Context());
            return areaRepository.Get().GetAwaiter().GetResult();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
