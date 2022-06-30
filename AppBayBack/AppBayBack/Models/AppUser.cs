using Microsoft.AspNetCore.Identity;

namespace AppBayBack.Models
{
    public class AppUser:IdentityUser
    {
        public bool IsAdmin { get; set; }
        public bool IsGranted { get; set; }
    }
}
