using SCA.Model.Entidades;

namespace SCA.Service.Interfaces
{
    public interface IEquipamentoService
    {
        bool Adicionar(Equipamento entity);
        bool Atualizar(Equipamento entity);
        Equipamento ObterPorId(string id);
        bool Excluir(string id);
        Equipamento Filtrar(string id);
    }
}
