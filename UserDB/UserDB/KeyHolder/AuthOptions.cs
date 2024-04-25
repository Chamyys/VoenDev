using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserDB.KeyHolder;

public class AuthOptions
{
    static string KEY;
    IKeyHolder _keyHolder;
    public AuthOptions(IKeyHolder keyHolder)
    {
        _keyHolder = keyHolder;
        KEY = keyHolder.GetKey();
    }
    public const string ISSUER = "MyIssuerClient"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}