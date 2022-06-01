using Microsoft.AspNetCore.Identity;

namespace Chat.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}