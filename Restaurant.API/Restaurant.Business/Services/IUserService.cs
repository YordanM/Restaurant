using Restaurant.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant.Business.Services
{
    public interface IUserService
    {
        Task<(int count, IEnumerable<User> users)> GetAllAsync(PaggingParameters userParameters, string currentUserId);

        Task<User> AddUserAsync(User user, string role, string password);

        Task<User> GetUserByIdAsync(string id);

        Task<User> UpdateUserAsync(string id, User user, string password, string role);

        Task DeleteUserAsync(string id);
    }
}
