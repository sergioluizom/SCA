using System.Collections.Generic;

namespace SCA.Model.Entidades
{
    public class Anexo : Entity
    {
        public Anexo()
        {
            Arquivo = new List<byte>();
        }
        public string Nome { get; set; }
        public List<byte> Arquivo { get; set; }
    }
}