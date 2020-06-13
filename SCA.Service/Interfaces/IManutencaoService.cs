﻿using SCA.Model.Entidades;
using System.Collections.Generic;

namespace SCA.Service.Interfaces
{
    public interface IManutencaoService
    {
        bool Adicionar(Manutencao entity);
        bool Atualizar(Manutencao entity);
        Manutencao ObterPorId(string id);
        bool Excluir(string id);
        bool Liberar(string id);
        Manutencao Filtrar(string id);
        List<Manutencao> ObterTodos();
        List<Manutencao> ObterConcluidas();
        List<Manutencao> ObterCadastradas();
    }
}
