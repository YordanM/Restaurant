using Microsoft.AspNetCore.Identity;

namespace Restaurant.Domain.Models
{
    public class ResultUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
