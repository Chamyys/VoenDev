using FluentResults;
using UserDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using About.Models;
using FluentResults;
using UserDB.Models;

namespace UserDB.Repository
{
    public interface IEventRepository<T> where T : Event
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
    


