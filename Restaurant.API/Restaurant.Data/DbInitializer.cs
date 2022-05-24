using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities.Initialization;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public class DbInitializer
    {

        private readonly RestaurantDbContext _context;
        private UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(RestaurantDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedTablesAsync();
            await SeedCategoriesAsync();
            await SeedProductsAsync();
        }

        private async Task SeedTablesAsync()
        {
            if (await _context.Tables.AnyAsync())
            {
                return;
            }

            var table = new Table();
            var rand = new Random();

            for (int i = 1; i <= 14; i++)
            {
                table.Id = Guid.NewGuid().ToString();
                table.TableNumber = i;
                table.Capacity = rand.Next(2, 13);

                await _context.Tables.AddAsync(table);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedProductsAsync()
        {
            if (await _context.Products.AnyAsync())
            {
                return;
            }

            var categories = await _context.Categories.ToListAsync();

            Dictionary<string, string> categoryDictionary = categories.ToDictionary(x => x.Name, x => x.Id);


            var products = FoodProducts.GetProducts(categoryDictionary);

            foreach (var product in products)
            {
                product.Id = Guid.NewGuid().ToString();
            }

            await _context.Products.AddRangeAsync(products);

            await _context.SaveChangesAsync();
        }

        private async Task SeedCategoriesAsync()
        {
            if (await _context.Categories.AnyAsync())
            {
                return;
            }

            var categories = FoodCategories.GetCategories();

            await _context.Categories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (await _context.Roles.AnyAsync())
            {
                return;
            }

            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Bartender));
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Waiter));

        }

        private async Task SeedUsersAsync()
        {
            if (await _context.Users.AnyAsync())
            {
                return;
            }

            var adminUser = new User
            {
                Email = "admin@mentormate.com",
                UserName = "admin@mentormate.com",
            };

            await _userManager.CreateAsync(adminUser, "Admin123!");
            await _userManager.AddToRoleAsync(adminUser, UserRoles.Admin);

            var regularUser = new User
            {
                Email = "user@mentormate.com",
                UserName = "user@mentormate.com",
            };

            await _userManager.CreateAsync(regularUser, "User123!");

            var user = new User();

            for (int i = 1; i < 21; i++)
            {
                user.Email = $"{i}@abv.bg";
                user.UserName = i.ToString();

                await _userManager.CreateAsync(user, "1234567890");
                await _userManager.AddToRoleAsync(user, UserRoles.Waiter);
            }
        }
    }
}
