using System;

namespace Restaurant.API.Models
{
    public class AuthorizeResult
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
