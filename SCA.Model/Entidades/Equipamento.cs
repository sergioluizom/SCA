using System.Collections.Generic;

namespace SCA.Model.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class Equipamento : Entity
    {
        public Equipamento()
        {
            Fotos = new List<Anexo>();
        }
        /// <summary>
        /// 
        /// </summary>
        public string Placa { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TipoEquipamento TipoEquipamento { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PlacaReal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Empresa Empresa { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Chassi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CapacidadeTanque { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Anexo> Fotos { get; set; }
    }
}