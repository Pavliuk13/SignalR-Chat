using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Chat
{
    public class AuthOptions
    {
        public const string ISSUER = "SignalRChat";
        
        public const string AUDIENCE = "ChatClients";
        
        const string KEY = "this is my custom Secret key for authentication";
        
        public const int LIFETIME = 60;
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}