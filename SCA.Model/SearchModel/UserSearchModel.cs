using SCA.Model.Enums;

namespace SCA.Model.SearchModel
{
    public class UserSearchModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string JobFunction { get; set; }
        public string DocumentNumber { get; set; }
        public UserStatus? UserStatus { get; set; }
    }
}