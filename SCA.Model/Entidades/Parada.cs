using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Model.Entidades
{
    public class Parada : Entity
    {
        public OperadorEquipamento OperadorEquipamento { get; set; }
        public Equipamento Equipamento { get; set; }
        public TipoParada TipoParada { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Observacao { get; set; }
    }
}
