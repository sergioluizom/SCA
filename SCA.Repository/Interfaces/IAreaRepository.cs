using SCA.Model;
using System.Threading.Tasks;

namespace SCA.Repository.Interfaces
{
    public interface IAreaRepository
    {
        Task<string> Teste(Area area);
    }
}
