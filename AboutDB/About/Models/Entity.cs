using MongoDB.Bson;

namespace About.Models
{
    public class Entity
    {
        public BsonObjectId _id { get; set; }
        public int numberOfUsers {  get; set; }
    }
}
