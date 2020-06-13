using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using SCA.Service.Interfaces;

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

        public Manutencao Filtrar(string id)
        {
            return repository.Filtrar(id);
        }

        public Manutencao ObterPorId(string id)
        {
            return repository.ObterPorId(id);
        }
    }
}