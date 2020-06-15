using MongoDB.Bson.Serialization.Attributes;
using SCA.Model.Enums;
using System;

namespace SCA.Model.Entidades
{
    public class Manutencao : Entity
    {
        public TipoManutencao TipoManutencao { get; set; }
        public TipoServico TipoServico { get; set; }
        public Equipamento Equipamento { get; set; }
        public string Descricao { get; set; }
        private DateTime dataProgramada;
        public DateTime DataProgramada { get => dataProgramada; set => dataProgramada = value != null ? DateTime.Now : value; }
        [BsonIgnore]
        private StatusManutencaoEnum? status;

        public StatusManutencaoEnum? Status {
            get {
                if (!status.HasValue)
                    status = StatusManutencaoEnum.Cadastrada;
                return status;
            }
            set => status = value;
        }
        public DateTime? DataPrevistaConclusao { get; set; }
        public string Observacao { get; set; }
    }
}
