using System;
using ProjetoAPI.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ProjetoAPI.Data.Collections
{
    public class MongoDB
    {
        public IMongoDatabase DataBase { get; }
        public MongoDB(IConfiguration configuration)
        {
            try 
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DataBase = client.GetDatabase("DbName");
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoException("it was not possible to connect to MongoDB", ex);
            }
        }
        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infected)))
            {
                BsonClassMap.RegisterClassMap<Infected>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }

        }
    }
}