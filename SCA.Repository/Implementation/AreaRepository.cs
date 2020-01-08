using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model.Entities;
using SCA.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace SCA.Repository.Implementation
{
    public class AreaRepository : IAreaRepository
    {
        private readonly Context context;
        public AreaRepository(Context context)
        {
            this.context = context;
        }

        public async Task<string> Teste(Area area)
        {
            try
            {
                await this.context.Areas.InsertOneAsync(area);
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
        }
    }
}
