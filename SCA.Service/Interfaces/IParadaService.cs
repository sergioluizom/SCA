using SCA.Model.Entidades;
using System.Collections.Generic;

namespace SCA.Service.Interfaces
{
    public interface IParadaService
    {
        bool Adicionar(Parada entity);
        bool Atualizar(Parada entity);
        Parada ObterPorId(string id);
        bool Excluir(string id);
        List<Parada> ObterTodos();
    }
}
