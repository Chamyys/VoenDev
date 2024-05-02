using About.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Repository;
using FluentResults;
using System.Collections;
using System.Diagnostics;
using UserDB.Models;
using MongoDB.Bson;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using UserDB.EmailService;
using System.Text;
using System.Security.Cryptography;
using UserDB.Repository;
namespace About.Controllers
{
    //[Route("/api/[controller]/[action]")]
    public class EventController : Controller
    {
        private readonly IEventRepository<Event> _eventRepository;
        public EventController(IEventRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;

        }

        public Result AddNewEvent([FromBody] Event newEvent)
        {

            try
            {
                _eventRepository.Create(newEvent);
                return Result.Ok();
            }
            catch (Exception ex) { return Result.Fail("New User Error"); }
        }
        public async Task<Result> DeleteEvent(string id)
        {

            try
            {
                var result = await _eventRepository.Delete(id);
                if (result == 1) return Result.Ok();
                else { return Result.Fail("Delete user Error"); }
            }
            catch (Exception ex) { return Result.Fail("Delete user Error"); }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void Index()
        {
            //return View();
        }


        public static string HashWithSHA256(string value)
        {
            using var hash = SHA256.Create();
            var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(byteArray);
        }

        public Task<List<Event>> GetEventsByDate(DateTime date)
        {
            return _eventRepository.GetEventsByDate(date);
        }
        public async Task<Result> GetEventById(string id)
        {
            //  BsonObjectId id  = BsonObjectId.Parse("660f2643c40be2f75931fcbe");
            try
            {
                var result = await _eventRepository.GetById(id);
                if (result == null) return Result.Ok();
                else { return Result.Fail("Delete event Error"); }
            }
            catch (Exception ex) { return Result.Fail("Delete event Error"); }
        }


    }





}




