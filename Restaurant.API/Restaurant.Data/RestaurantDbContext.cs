using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models;

namespace Restaurant.Data
{
    public class RestaurantDbContext : IdentityDbContext<User>
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>();
            builder.Entity<Product>();
            builder.Entity<Table>();

            base.OnModelCreating(builder);
        }
    }
}
