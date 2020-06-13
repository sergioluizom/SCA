using MongoDB.Bson.Serialization.Serializers;

namespace SCA.Model.Entidades
{
    public class Anexo : Entity
    {
        public string Nome { get; set; }
        public byte[] Arquivo { get; set; }
    }
}