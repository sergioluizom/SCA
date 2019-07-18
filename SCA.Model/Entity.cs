using System;

namespace SCA.Model
{
    public class Entity
    {
        private string _id;
        public string Id
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_id))
                    _id = Guid.NewGuid().ToString();
                return _id;
            }
            set => _id = value;
        }
    }
}