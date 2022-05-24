using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Restaurant.API.Models
{
    public class UserRequest 
    {
        private readonly string[] AllowedRoles = new[] { "Admin", "Bartender", "Waiter" };

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        public string Role { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public User ToUserModel()
            => new User
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = $"{FirstName}{LastName}",
                Email = Email
            };
    }
}
