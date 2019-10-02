using MongoDB.Bson.Serialization.Attributes;

namespace SCA.Model.Entities
{
    public class User : Entity
    {
        public User()
        {
        }
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public Profile Profile { get; set; }
        public string Password { get; set; }

        public static string EncryptPassword(string password)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
