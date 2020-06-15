using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Model.Entidades
{
    public class Usuario : Entity
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; }
    }
}
