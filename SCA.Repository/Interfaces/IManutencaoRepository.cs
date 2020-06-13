using SCA.Model.Entidades;

namespace SCA.Repository.Interfaces
{
    public interface IManutencaoRepository
    {
        bool Adicionar(Manutencao entity);
        bool Atualizar(Manutencao entity);
        Manutencao ObterPorId(string id);
        bool Excluir(string id);
        Manutencao Filtrar(string id);
    }
}
