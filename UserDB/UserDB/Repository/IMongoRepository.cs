using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using About.Models;
using FluentResults;
using UserDB.Models;

namespace Repository
{

    public interface IMongoRepository<T> where T : User
    {
        public void setCollectionName(string name);
        public Task Create(T car);
        public Task<int> Delete(string id);
        public Task<T> GetFirst();
        public Task<User> GetByLogin(string email, string password);
        public Task<Result> GenerateEmailConfirmationTokenAsync(T user);
        public Task<Result> ConfirmEmailAsync(T user);
    }
}