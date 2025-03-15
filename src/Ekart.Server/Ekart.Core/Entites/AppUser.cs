using Microsoft.AspNetCore.Identity;

namespace Ekart.Core.Entites
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Address? Address { get; set; }
    }
}
