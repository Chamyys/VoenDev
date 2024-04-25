
using MongoDB.Driver;
using MongoDB.Bson;
using About.Models;

namespace Repository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : Entity  //Возможно придется менять на entity
    {

        private IMongoCollection<T> _collection;
        private IMongoDatabase _database;
        public MongoRepository(IMongoClient client)
        {
            var database = client.GetDatabase("Mongo");
            _database = database;
            var collection = database.GetCollection<T>("About");
            _collection = collection;
        }

        public void setCollectionName(string name)
        {
            _collection = _database.GetCollection<T>(name);
        }



        public async Task<string> Create(T entity)
        {

            await _collection.InsertOneAsync(entity);
            return entity.ToString();
        }
          /*
        public void Delete(T entity)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(c => c.id, entity.id);
                var result = _weathercollection.DeleteOneAsync(filter);
                return;
                // return result.DeletedCount == 1;
            }
            catch (Exception e)
            {
               
            }

        }
          */
        public async Task<T> Get()
        {
             var entity = _collection.Find(new BsonDocument()).ToList().ElementAt(0);
             return entity;
         }



        public async Task Update(int additionalUsers)
        {
            var entity = await Get();
            var filter = Builders<T>.Filter.Eq("numberOfUsers", entity.numberOfUsers);
            var update = Builders<T>.Update
            .Set("numberOfUsers", entity.numberOfUsers + additionalUsers);
            await _collection.UpdateOneAsync(filter, update);
        }



          /*
        public async Task<List<T>> GetAll()
        {
            var allEntities = await _weathercollection.Find(_ => true).ToListAsync();
            return allEntities;
        }
          */
    }
}





/*
using MongoDB.Driver;
using MongoDB.Bson;
using About.Models;

namespace Repository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : Entity  //Возможно придется менять на entity
    {

        private IMongoCollection<T> _weathercollection;
        private IMongoDatabase _database;
        public MongoRepository(IMongoClient client)
        {
            var database = client.GetDatabase("Mongo");
            _database = database;
            var collection = database.GetCollection<T>(nameof(T));
            _weathercollection = collection;
        }

        /*
        public MongoRepository(IMongoClient client, string databaseName, string collectionName) конфигурация для нескольких таблиц
        {
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<Entity>(collectionName);
            _weathercollection = collection;
        }
        */


      //  public void setCollectionName(string name)
       // {
     //       _weathercollection = _database.GetCollection<T>(name);
     //   }



     //   public async Task<string> Create(T car)
   //     {

  //          await _weathercollection.InsertOneAsync(car);
  //          return car.id;
  //      }
        /*
      public void Delete(T entity)
      {
          try
          {
              var filter = Builders<T>.Filter.Eq(c => c.id, entity.id);
              var result = _weathercollection.DeleteOneAsync(filter);
              return;
              // return result.DeletedCount == 1;
          }
          catch (Exception e)
          {

          }

      }
        */
    //    public async Task<T> Get(T entity)
   //     {

  //          var filter = Builders<T>.Filter.Eq(c => c.id, entity.id);
   //         var car = _weathercollection.Find(filter).FirstOrDefaultAsync();
  //          if (car.Result.id == null) car.Result.id = "Not Found";
  //          return car.Result;
  //      }



    //    public async Task Update(dynamic entity)
   //     {
   //         var filter = Builders<dynamic>.Filter.Eq("id", entity.id);
  //          var update = Builders<dynamic>.Update
   //         .Set("firstName", entity.firstName)
  //          .Set("secondName", entity.secondName)
    //        .Set("login", entity.login)
   //         .Set("password", entity.password)
   //         .Set("role", entity.role);
  //          await _weathercollection.UpdateOneAsync(filter, update);
  //      }



        /*
      public async Task<List<T>> GetAll()
      {
          var allEntities = await _weathercollection.Find(_ => true).ToListAsync();
          return allEntities;
      }
        */
  //  }
//}