using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.API.Models;
using Restaurant.Business.Services;
using Restaurant.Domain.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaggingParameters userParameters)
        {
            ClaimsPrincipal currentUser = User;

            var currentUserId = ClaimsPrincipalExtensions.GetUserId(currentUser);

            var users = await _userService.GetAllAsync(userParameters, currentUserId);

            return Ok(users.users);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserRequest request)
        {

            var user = request.ToUserModel();

            UserResult result = new UserResult(request);

            await _userService.AddUserAsync(user, request.Role, request.Password);


            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string id, [FromBody] UserRequest request)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var password = request.Password;

            var role = request.Role;

            var result = await _userService.UpdateUserAsync(id, request.ToUserModel(), password, role);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDeviceAsync([FromRoute] string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            await _userService.DeleteUserAsync(id);

            return NoContent();
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMeAsync([FromBody] UserRequest request)
        {
            ClaimsPrincipal currentUser1 = User;

            var userId = currentUser1.FindFirstValue(ClaimTypes.NameIdentifier);

            var role = request.Role;

            var password = request.Password;

            var result = await _userService.UpdateUserAsync(userId, request.ToUserModel(), password, role);

            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMeAsync()
        {
            ClaimsPrincipal currentUser = this.User;

            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await _userService.GetUserByIdAsync(currentUserId);

            if (user == null)
            {
                return NotFound();
            }

            var currentUserRole = ClaimsPrincipalExtensions.GetRole(currentUser);

            var userResult = new UserResult(new UserRequest()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = currentUserRole
            });


            return Ok(userResult);
        }
    }
}
