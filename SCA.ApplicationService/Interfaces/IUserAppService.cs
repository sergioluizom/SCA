using System.Collections.Generic;
using System.Threading.Tasks;
using SCA.Model.Entities;
using SCA.Model.SearchModel;

namespace SCA.ApplicationService.Interfaces
{
    public interface IUserAppService
    {
        Task<List<User>> FindByCriteria(UserSearchModel user);
        Task<User> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(string id);
        Task<User> Find(string id);
    }
}
