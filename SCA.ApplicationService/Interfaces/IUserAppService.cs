using System.Collections.Generic;
using System.Threading.Tasks;
using SCA.Model.Entities;
using SCA.Model.SearchModel;

namespace SCA.ApplicationService.Interfaces
{
    public interface IUserAppService
    {
        void Add(User user);
        Task<List<User>> FindByCriteria(UserSearchModel user);
        Task<User> Find(string id);
    }
}
