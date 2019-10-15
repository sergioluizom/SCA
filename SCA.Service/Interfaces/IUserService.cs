using System.Collections.Generic;
using System.Threading.Tasks;
using SCA.Model.Entities;
using SCA.Model.SearchModel;

namespace SCA.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> FindByCriteria(UserSearchModel user);
        Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(string id);
        Task<User> Find(string id);
    }
}
