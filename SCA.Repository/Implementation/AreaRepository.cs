using SCA.Infraestrutura;
using SCA.Model;
using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SCA.Repository.Implementation
{
    public class AreaRepository
    {
        private readonly Context context;
        public AreaRepository(Context context)
        {
            this.context = context;
        }

        public async Task<string> Teste()
        {
            try
            {
                await this.context.Areas.InsertOneAsync(new Model.Area() { Nome = "Teste" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "ok";
        }


        public async Task<Area> Get()
        {
            try
            {
                var areas = await this.context.Areas.FindAsync<Area>(c => c.Nome == "Teste");
               return await areas.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Area();
        }
    }
}
