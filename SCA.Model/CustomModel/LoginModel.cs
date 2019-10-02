using SCA.Model.Entities;

namespace SCA.Model.CustomModel
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public bool PasswordIsValid(string password, User user)
        {
            return User.EncryptPassword(password) == user.Password;
        }
    }
}