using MongoDB.Driver;
using SCA.Infraestrutura;
using SCA.Model;
using System;
using System.Threading.Tasks;

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
                await this.context.Users.InsertOneAsync(new Model.User()
                {
                    Name = "Teste"
                });


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
