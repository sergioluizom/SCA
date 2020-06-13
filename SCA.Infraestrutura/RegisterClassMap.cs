using MongoDB.Bson.Serialization;
using SCA.Model.Entidades;

namespace SCA.Infraestrutura
{
    public class RegisterClassMap
    {
        public static void RegisterClass()
        {
            Register<Area>();
            Register<Equipamento>();
            Register<TipoEquipamento>();
            Register<Empresa>();
            Register<Manutencao>();
        }

        private static void Register<T>()
        {
            BsonClassMap.RegisterClassMap<T>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
