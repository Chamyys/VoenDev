using Microsoft.Extensions.Configuration;

namespace UserDB.KeyHolder
{
    public class KeyHolder : IKeyHolder
    {
         IConfiguration _configuration;
        public static string KEY;
        public KeyHolder(IConfiguration configuration)
        {
            _configuration = configuration;
            KEY = _configuration["SECRETKEY"];
        }
        
        public  string GetKey()
        {
           return KEY;
        }
    }
}
