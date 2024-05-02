using About.Models;

namespace UserDB.Models
{
    public class Event : Entity//Соответственно еще поле BsonId _id
    {

        public required string Title { get; set; }
        public required string Category { get; set; }
        public required string Link { get; set; }
        public required string Adress { get; set; }
        public required DateTime EventTime { get; set; }
        public required string DiscriptionLink { get; set; }
        public required int Members { get; set; }
        public required int MembersRequired { get; set; }
        public required List<string> LinksForPhotoes { get; set; }
        public List<string>? LinksForShorts { get; set; }
        public List<string>? LinksForVideos { get; set; }
        public string? DonationLink { get; set; }
    }
}
