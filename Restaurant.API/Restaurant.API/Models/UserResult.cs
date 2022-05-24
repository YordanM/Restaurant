namespace Restaurant.API.Models
{
    public class UserResult
    {
        public string Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string Role { get; set; }

        public UserResult(UserRequest user)
        {
            UserName = $"{user.FirstName} {user.LastName}";
            Email = user.Email;
            Role = user.Role;
        }
    }
}
