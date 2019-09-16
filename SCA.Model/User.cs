namespace SCA.Model
{
    public class User : Entity
    {
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public Profile Profile { get; set; }
    }
}
