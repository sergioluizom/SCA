using SCA.Model.Entidades;
using SCA.Repository.Interfaces;
using SCA.Service.Interfaces;

namespace SCA.Service.Implementation
{
    public class EquipamentoService : IEquipamentoService
    {
        private readonly IEquipamentoRepository repository;

        public EquipamentoService(IEquipamentoRepository repository)
        {
            this.repository = repository;
        }

        public bool Adicionar(Equipamento entity)
        {
            return repository.Adicionar(entity);
        }

        public bool Atualizar(Equipamento entity)
        {
            return repository.Atualizar(entity);
        }

        public bool Excluir(string id)
        {
            return repository.Excluir(id);
        }

        public Equipamento Filtrar(string id)
        {
            return repository.Filtrar(id);
        }

        public Equipamento ObterPorId(string id)
        {
            return repository.ObterPorId(id);
        }
    }
}