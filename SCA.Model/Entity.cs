using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SCA.Model
{
    public class Entity
    {
        [BsonIgnore]
        private string _id;
        [BsonId]
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