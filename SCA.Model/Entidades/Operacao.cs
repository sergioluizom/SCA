using System;

namespace SCA.Model.Entidades
{
    public class Operacao : Entity
    {
        public OperadorEquipamento OperadorEquipamento { get; set; }
        public Equipamento Equipamento { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
        public Area Area { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Observacao { get; set; }
        public int? Tonelada { get; set; }
    }
}
