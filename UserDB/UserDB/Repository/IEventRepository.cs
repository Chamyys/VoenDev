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

        public Task Create(T newEvent);


        public Task<int> Delete(string id);


        public Task<Event> GetById(string id);


        public Task<List<T>> GetEventsByDate(DateTime date);

    }
}



