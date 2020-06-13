using SCA.Model.Entidades;

namespace SCA.Service.Interfaces
{
    public interface IManutencaoService
    {
        bool Adicionar(Manutencao entity);
        bool Atualizar(Manutencao equipamento);
        Manutencao ObterPorId(string id);
        bool Excluir(string id);
        Manutencao Filtrar(string id);
    }
}
