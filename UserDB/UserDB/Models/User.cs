using About.Models;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace UserDB.Models
{
    public class User : Entity //Соответственно еще поле BsonId _id
    {

        public required string UserID { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        [BsonIgnoreIfDefault]
        public DateTime? BirthDate { get; set; }

        [BsonIgnoreIfDefault]
        public string? PlaceOfLiving { get; set; }
        [BsonIgnoreIfDefault]
        public string? PhoneNumber { get; set; }
        [BsonIgnoreIfDefault]
        public List<Event>? ListOfEvents { get; set; }
        [BsonIgnoreIfDefault]
        public bool IsEmailConfirmed { get; set; }

        [BsonIgnoreIfDefault]
        public string EmailConfirmationToken { get; set; }

    }
}
