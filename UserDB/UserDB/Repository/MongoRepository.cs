
using MongoDB.Driver;
using MongoDB.Bson;
using About.Models;
using FluentResults;
using UserDB.Models;
using System.Security.Cryptography;
using System.Text;

namespace Repository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : User  //Возможно придется менять на entity
    {

        private IMongoCollection<T> _collection;
        private IMongoDatabase _database;
        public MongoRepository(IMongoClient client)
        {
            var database = client.GetDatabase("Mongo");
            _database = database;
            var collection = database.GetCollection<T>("Users");
            _collection = collection;
        }

        public void setCollectionName(string name)
        {
            _collection = _database.GetCollection<T>(name);
        }

        public static string HashWithSHA256(string value)
        {
            using var hash = SHA256.Create();
            var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(byteArray);
        }

        public async Task Create(T entity)
        {
            entity.Password = HashWithSHA256(entity.Password);
            await _collection.InsertOneAsync(entity);

        }

        public async Task<int> Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq(c => c._id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return (int)result.DeletedCount;
        }

        public async Task<T> GetFirst()
        {
            var entity = _collection.Find(new BsonDocument()).ToList().ElementAt(0);
            return entity;
        }
        public async Task<User> GetByLogin(string email, string password)
        {
            var filter = Builders<T>.Filter.Eq(c => c.Email, email);
            var result = _collection.Find(filter).FirstOrDefaultAsync();
            if (result.Result == null) return null;
            return result.Result;
        }
        public async Task<Result> GenerateEmailConfirmationTokenAsync(T user)
        {
            var filter = Builders<T>.Filter.Eq(c => c.Email, user.Email); // Фильтр для нахождения пользователя по электронной почте
            var update = Builders<T>.Update.Set(c => c.EmailConfirmationToken, Encoding.UTF8.GetBytes(user.Password).ToString()); // Установка нового токена для подтверждения электронной почты
            var result = await _collection.UpdateOneAsync(filter, update);
            if (result != null)
            {
                return Result.Ok();
            }
            else return Result.Fail("fail to update emailToken");
        }
        public async Task<Result> ConfirmEmailAsync(T user)
        {
            var filter = Builders<T>.Filter.Eq(c => c.Email, user.Email); // Фильтр для нахождения пользователя по электронной почте
            var update = Builders<T>.Update.Set(c => c.IsEmailConfirmed, true); // Установка нового токена для подтверждения электронной почты
            var result = await _collection.UpdateOneAsync(filter, update);
            if (result != null)
            {
                return Result.Ok();
            }
            else return Result.Fail("fail to update emailConfitmationToken");
        }






    }
}


