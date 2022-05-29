using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Data.Repositories
{
    public interface IUserRepository
    {
        Task<(int count, IEnumerable<User> users)> GetAllAsync(PaggingParameters userParameters,
                                                               Expression<Func<User, bool>> filter = null);

        Task AddUserAsync(User user, string role, string password);

        Task<User> GetUserByIdAsync(string id);

        Task<User> UpdateUserAsync(string id, User user, string password, string role);

        Task DeleteUserAsync(User user);
    }
}
