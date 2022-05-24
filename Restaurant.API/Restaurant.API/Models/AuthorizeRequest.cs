using System.ComponentModel.DataAnnotations;

namespace Restaurant.API.Models
{
    public class AuthorizeRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
