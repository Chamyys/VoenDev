using About.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Repository;
using System.Collections;
using System.Diagnostics;

namespace About.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IMongoRepository<Entity> _mongoRepository;
        public HomeController(IMongoRepository<Entity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
                  
        }
     
        public int AddNewUserToCounter()
        {
            _mongoRepository.Update(1);
            return  _mongoRepository.Get().Result.numberOfUsers;
        }
        public int Test()
        {
            return 1;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
