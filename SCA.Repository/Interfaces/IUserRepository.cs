using SCA.Model.Entities;
using SCA.Model.SearchModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCA.Repository.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        Task<List<User>> FindByCriteria(UserSearchModel user);
        Task<User> Find(string id);
    }
}
