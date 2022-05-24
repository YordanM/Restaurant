using Restaurant.Data.Repositories;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUserAsync(User user, string role, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var users = await _userRepository.GetAllAsync(null, null);
            var isUnique = users.users.Any(p => p.UserName == user.UserName);

            if (isUnique)
            {
                throw new Exception("Such user exists already.");
            }

            await _userRepository.AddUserAsync(user, role, password);

            return user;
        }

        public Task DeleteUserAsync(string id)
        {
            return _userRepository.DeleteUserAsync(id);
        }

        public async Task<(int count, IEnumerable<User> users)> GetAllAsync(PaggingParameters userParameters,
                                                                      string currentUserId)
        {
            Expression<Func<User, bool>> filter = x => x.Id != currentUserId;

            return await _userRepository.GetAllAsync(userParameters, filter);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> UpdateUserAsync(string id, User user, string password, string role)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (role != UserRoles.Waiter && role != UserRoles.Bartender && role != UserRoles.Admin && role != null)
            {
                throw new Exception("There is no such role.");
            }

            Expression<Func<User, bool>> filter = x => x.UserName == user.UserName;

            var users = await _userRepository.GetAllAsync(null, filter);

            var isUnique = users.users.Any(p => p.UserName == user.UserName);

            var existingUser = await _userRepository.GetUserByIdAsync(id);

            var uniqueEmail = users.users.Any(e => e.Email == user.Email);


            if (isUnique && existingUser.UserName != user.UserName)
            {
                throw new Exception("Such user already exists.");
            }

            if (uniqueEmail && existingUser.Email != user.Email)
            {
                throw new Exception("Email address is already in use.");
            }

            return await _userRepository.UpdateUserAsync(id, user, password, role);
        }
    }
}
