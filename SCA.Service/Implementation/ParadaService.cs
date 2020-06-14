using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using SCA.Service.Interfaces;
using System.Collections.Generic;

namespace SCA.Service.Implementation
{
    public class ParadaService : IParadaService
    {
        private readonly IParadaRepository repository;

        public ParadaService(IParadaRepository repository)
        {
            this.repository = repository;
        }

        public bool Adicionar(Parada entity)
        {
            return repository.Adicionar(entity);
        }

        public bool Atualizar(Parada entity)
        {
            return repository.Atualizar(entity);
        }

        public bool Excluir(string id)
        {
            return repository.Excluir(id);
        }

        public Parada ObterPorId(string id)
        {
            return repository.ObterPorId(id);
        }

        public List<Parada> ObterTodos()
        {
            return repository.ObterTodos();
        }
    }
}