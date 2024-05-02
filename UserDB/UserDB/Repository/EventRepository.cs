using About.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Repository;
using UserDB.Models;

namespace UserDB.Repository
{
    public class EventRepository<T> : IEventRepository<T> where T : Event
    {
        private IMongoCollection<T> _collection;
        private IMongoDatabase _database;
        public EventRepository(IMongoClient client)
        {
            var database = client.GetDatabase("Mongo");
            _database = database;
            var collection = database.GetCollection<T>("Events");
            _collection = collection;
        }

        public void setCollectionName(string name)
        {
            _collection = _database.GetCollection<T>(name);
        }
        public async Task Create(T newEvent)
        {
            await _collection.InsertOneAsync(newEvent);
        }
        public async Task<int> Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq(c => c._id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return (int)result.DeletedCount;
        }
        public async Task<Event> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq(c => c._id, id);
            var result = _collection.Find(filter).FirstOrDefaultAsync();
            if (result.Result == null) return null;
            return result.Result;
        }
        public async Task<List<T>> GetEventsByDate(DateTime date)
        {
            var filter = Builders<T>.Filter.Lt(c => c.EventTime, date);
            var result = await _collection.Find(filter).ToListAsync();
            if (result.Count == 0)
                return null;
            return result;
        }

    }
}
