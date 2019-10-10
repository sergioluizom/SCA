using MongoDB.Bson.Serialization.Attributes;
using SCA.Model.Enums;
using System;

namespace SCA.Model.Entities
{
    public class User : Entity
    {
        public User()
        {
        }

        public User(string documentNumber, string name, string jobFunction, string email)
        {
            DocumentNumber = documentNumber;
            Name = name;
            JobFunction = jobFunction;
            Status = UserStatus.Pending;
            Email = email;
        }

        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string JobFunction { get; set; }
        public UserStatus Status { get; set; }
        public Profile Profile { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }

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
