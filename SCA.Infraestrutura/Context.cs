﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SCA.Model.Entidades;
using System.Security.Authentication;

namespace SCA.Infraestrutura
{
    public class Context
    {
        private readonly IMongoDatabase _database;
        private readonly IConfiguration configuration;
        public Context(IConfiguration configuration)
        {
            this.configuration = configuration;
            _database = DatabaseFactory(this.configuration["connectionStringMongo"], this.configuration["userMongo"]);
            RegisterClassMap.RegisterClass();
        }

        public IMongoCollection<Area> Areas => _database.GetCollection<Area>(nameof(Areas).ToLower());
        public IMongoCollection<Equipamento> Equipamentos => _database.GetCollection<Equipamento>(nameof(Equipamentos).ToLower());
        public IMongoCollection<Manutencao> Manutencaos => _database.GetCollection<Manutencao>(nameof(Manutencaos).ToLower());
        public IMongoCollection<Parada> Paradas => _database.GetCollection<Parada>(nameof(Paradas).ToLower());
        public IMongoCollection<Operacao> Operacaos => _database.GetCollection<Operacao>(nameof(Operacaos).ToLower());
        public IMongoCollection<Empresa> Empresas => _database.GetCollection<Empresa>(nameof(Empresas).ToLower());
        public IMongoCollection<TipoEquipamento> TipoEquipamentos => _database.GetCollection<TipoEquipamento>(nameof(TipoEquipamentos).ToLower());
        public IMongoCollection<Usuario> Usuarios => _database.GetCollection<Usuario>(nameof(Usuarios).ToLower());

        private static IMongoDatabase DatabaseFactory(string connectionString, string databaseName)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };
            return new MongoClient(settings).GetDatabase(databaseName);
        }
    }
}