using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using SCA.Service.Interfaces;
using System.Collections.Generic;

namespace SCA.Service.Implementation
{
    public class ManutencaoService : IManutencaoService
    {
        private readonly IManutencaoRepository repository;

        public ManutencaoService(IManutencaoRepository repository)
        {
            this.repository = repository;
        }

        public bool Adicionar(Manutencao entity)
        {
            return repository.Adicionar(entity);
        }

        public bool Atualizar(Manutencao entity)
        {
            return repository.Atualizar(entity);
        }

        public bool Excluir(string id)
        {
            return repository.Excluir(id);
        }

        public bool Liberar(string id)
        {
            return repository.Liberar(id);
        }

        public Manutencao ObterPorId(string id)
        {
            return repository.ObterPorId(id);
        }

        public List<Manutencao> ObterTodos()
        {
            return repository.ObterTodos();
        }

        public List<Manutencao> ObterConcluidas()
        {
            return repository.ObterConcluidas();
        }

        public List<Manutencao> ObterCadastradas()
        {
            return repository.ObterCadastradas();
        }
    }
}