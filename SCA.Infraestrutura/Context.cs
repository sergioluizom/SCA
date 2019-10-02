﻿using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SCA.Model.Entities;
using System.Security.Authentication;

namespace SCA.Infraestrutura
{
    public class Context
    {
        private readonly IMongoDatabase _database;

        //public Context(IConfiguration configuration)
        public Context()
        {
            _database = DatabaseFactory("mongodb://application:sergio9205@ds337985.mlab.com:37985/heroku_386b1s1k", "heroku_386b1s1k");
            RegisterClassMap();
        }

        public IMongoCollection<Area> Areas => _database.GetCollection<Area>(nameof(Areas).ToLower());
        public IMongoCollection<User> Users => _database.GetCollection<User>(nameof(Users).ToLower());

        private static IMongoDatabase DatabaseFactory(string connectionString, string databaseName)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };
            return new MongoClient(settings).GetDatabase(databaseName);
        }

        private static void RegisterClassMap()
        {
            BsonClassMap.RegisterClassMap<Area>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}