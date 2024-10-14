using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace libraryWeb.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Username { get; set; }
        
    }
}
