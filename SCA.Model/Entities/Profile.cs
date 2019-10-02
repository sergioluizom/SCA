using System.Collections.Generic;

namespace SCA.Model.Entities
{
    public class Profile : Entity
    {
        public Profile()
        {
            Rules = new HashSet<RuleProfile>();
        }
        public string Name { get; set; }
        public ICollection<RuleProfile> Rules { get; set; }
    }
}