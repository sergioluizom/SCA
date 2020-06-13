using SCA.Model.Entidades;

namespace SCA.Repository.Interfaces
{
    public interface IEquipamentoRepository
    {
        bool Adicionar(Equipamento entity);
        bool Atualizar(Equipamento entity);
        Equipamento ObterPorId(string id);
        bool Excluir(string id);
        Equipamento Filtrar(string id);
    }
}
