using SCA.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Repository
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
    }
}
