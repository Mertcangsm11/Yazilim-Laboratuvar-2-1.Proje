using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarProje.Models;
namespace CarProje.Controllers
{
    public class MongoDbController
    {
        private static MongoDbController _client;
        private static object lockobject = new object();

        private MongoClient mongoClient;
        private IMongoCollection<ProjectModel> ProjectModelCollection;

        public static MongoDbController ProjectModelClientAsSingleton() 
        {
            lock(lockobject)
            {
                return _client ?? (_client = new MongoDbController());
            }
        }
        public MongoDbController()
        {
            mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("cardb");
            ProjectModelCollection = database.GetCollection<ProjectModel>("gpscar");
        }

    }
}