using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using About.Models;

namespace Repository
{

    public interface IMongoRepository<T> where T : Entity
    {
        public void setCollectionName(string name);
        public Task<string> Create(T car);
        //public void Delete(T entity);
        public  Task<T> Get();
        //public Task<List<T>> GetAll();
        public Task Update(int additionalUsers);
    }
}