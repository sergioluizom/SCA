using SCA.Model.Enums;
using System;

namespace SCA.Model.Entidades
{
    public class Manutencao: Entity
    {
        public TipoManutencao TipoManutencao { get; set; }
        public TipoServico TipoServico { get; set; }
        public Equipamento Equipamento { get; set; }
        public string Descricao { get; set; }
        public DateTime DataProgramada { get; set; }
        public StatusManutencaoEnum Status { get; set; }
        public DateTime? DataPrevistaConclusao { get; set; }
        public string Observacao { get; set; }
    }
}
