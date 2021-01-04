using Microsoft.AspNetCore.Identity;

namespace EasyMeal.Domain
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
