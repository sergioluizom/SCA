using SCA.Model.Entidades;
using System.Collections.Generic;

namespace SCA.Service.Interfaces
{
    public interface IOperacaoService
    {
        void Adicionar(Operacao entity);
        bool Atualizar(Operacao entity);
        Operacao ObterPorId(string id);
        bool Excluir(string id);
        List<Operacao> ObterTodos();
    }
}
