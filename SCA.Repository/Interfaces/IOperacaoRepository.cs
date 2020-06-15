using SCA.Model.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCA.Repository.Interfaces
{
    public interface IOperacaoRepository
    {
        Task<bool> Adicionar(Operacao entity);
        bool Atualizar(Operacao entity);
        Operacao ObterPorId(string id);
        bool Excluir(string id);
        List<Operacao> ObterTodos();
    }
}
