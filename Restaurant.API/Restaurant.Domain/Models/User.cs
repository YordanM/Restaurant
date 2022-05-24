using Microsoft.AspNetCore.Identity;

namespace Restaurant.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ResultUser ToUserModel(int? id = null)
            => new ResultUser
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };
    }
}
