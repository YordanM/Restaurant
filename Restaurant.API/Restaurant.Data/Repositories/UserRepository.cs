using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserRepository(RestaurantDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task AddUserAsync(User user, string role, string password)
        {
            await _userManager.CreateAsync(user, password);

            await _dbContext.SaveChangesAsync();

            await _userManager.AddToRoleAsync(user, role);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {   
            _dbContext.Remove(user);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(int count, IEnumerable<User> users)> GetAllAsync(PaggingParameters paggingParameters,
                                                                            Expression<Func<User, bool>> filter = null)
        {

            var query = _dbContext.Users
                    .AsQueryable();

            var listCount = _dbContext.Users.Count();

            var result = new List<User>();

            if (paggingParameters != null)
            {
                result = await query
               .Where(filter)
               .Skip((paggingParameters.PageNumber - 1) * paggingParameters.PageSize)
               .Take(paggingParameters.PageSize)
               .ToListAsync();
            }
            else
            {
                result = await query
                    .Where(filter)
                    .ToListAsync();
            }

            return (listCount, result);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> UpdateUserAsync(string id, User user, string password, string role)
        {
            var existingUser = await GetUserByIdAsync(id);

            if (existingUser == null)
            {
                return null;
            }

            string code = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            await _userManager.ResetPasswordAsync(existingUser, code, password);

            var currentRole = await _userManager.GetRolesAsync(existingUser);

            if (role != null)
            {
                await _userManager.RemoveFromRoleAsync(existingUser, currentRole[0]);
                await _userManager.AddToRoleAsync(existingUser, role);
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.UserName = user.UserName;

            _dbContext.SaveChanges();

            return existingUser;
        }
    }
}
