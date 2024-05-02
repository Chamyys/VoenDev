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
namespace About.Controllers
{
    //[Route("/api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IMongoRepository<User> _mongoRepository;
        public HomeController(IMongoRepository<User> mongoRepository)
        {
            _mongoRepository = mongoRepository;

        }

        public Result AddNewUser([FromBody] User newUser)
        {

            //   User user = new User { Surname = "1", BirthDate = new DateTime(), Email = "TEst", Password = "Password",
            //      PlaceOfLiving = "das", UserID = "sdome", _id = "some id"//BsonObjectId.GenerateNewId()
            // };

            try
            {
                _mongoRepository.Create(newUser);
                return Result.Ok();
            }
            catch (Exception ex) { return Result.Fail("New User Error"); }
        }
        public async Task<Result> DeleteUser(string id)
        {
            //  BsonObjectId id  = BsonObjectId.Parse("660f2643c40be2f75931fcbe");
            try
            {
                var result = await _mongoRepository.Delete(id);
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

        [HttpPost]
        public IResult Login([FromBody] User? loginData)
        {

            loginData.Password = HashWithSHA256(loginData.Password);
            User user = _mongoRepository.GetByLogin(loginData.Email, loginData.Password).Result;
            if (user is null || !user.IsEmailConfirmed) return Results.Unauthorized();


            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new
            {
                access_token = encodedJwt,
                username = user.Email
            };

            return Results.Json(response);
        }
        [HttpGet]
        [Authorize]
        public string Data()
        {
            return ("Hello World!");
        }
        [HttpGet]
        public /*IActionResult*/ void Register()
        {
            //return View();
        }
        public static string HashWithSHA256(string value)
        {
            using var hash = SHA256.Create();
            var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(byteArray);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User loginData)
        {
            loginData.Password = HashWithSHA256(loginData.Password);
            var result = AddNewUser(loginData);
            if (result.IsSuccess)
            {

                var code = Encoding.UTF8.GetBytes(loginData.Email).ToString();
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Home",
                    new { email = loginData.Email, code = code, password = loginData.Password },
                    protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(loginData.Email, "Confirm your account",
                    $"ѕодтвердите регистрацию, перейд€ по ссылке: <a href='{callbackUrl}'>¬аша уникальна€ ссылка</a>");
                return View();

            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string code, string password)
        {
            if (email == null || code == null)
            {
                return View("Error");
            }
            var user = await _mongoRepository.GetByLogin(email, password);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _mongoRepository.ConfirmEmailAsync(user);
            if (result.IsSuccess)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }


    }





}




