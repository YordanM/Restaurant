using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Domain.Entities.Initialization
{
    public static class FoodCategories
    {
        private static List<Category> Categories = new List<Category>();

        public static ICollection<Category> GetCategories()
        {
            Categories.AddRange(Main());
            Categories.AddRange(Salads());
            Categories.AddRange(Sandwiches());
            Categories.AddRange(Desserts());
            Categories.AddRange(Beverages());
            Categories.AddRange(Alcoholic());
            Categories.AddRange(NonAlcoholic());

            var result = Categories.ToList();

            return result;
        }

        public static ICollection<Category> Main() =>
            new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Salads"
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Sandwiches"
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Desserts"
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Beverages"
                },
            };

        public static ICollection<Category> Salads() =>
            new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Vegetarian Salads",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Salads").Id
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Non-Vegetarian Salads",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Salads").Id
                },
            };

        public static ICollection<Category> Sandwiches() =>
            new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Vegetarian Sandwiches",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Sandwiches").Id
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Non-Vegetarian Sandwiches",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Sandwiches").Id
                },
            };

        public static ICollection<Category> Desserts() =>
            new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Cake",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Desserts").Id,
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Pastries",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Desserts").Id,
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Frozen",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Desserts").Id,
                },
            };

        public static ICollection<Category> Beverages() =>
            new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Alcoholic",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Beverages").Id,
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Non-alcoholic",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Beverages").Id,
                },
            };

        public static ICollection<Category> Alcoholic() =>
            new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Beer",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Alcoholic").Id,
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Whiskey",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Alcoholic").Id,
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Vodka",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Alcoholic").Id,
                },
            };

        public static ICollection<Category> NonAlcoholic() =>
            new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Coffee",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Non-alcoholic").Id,
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tea",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Non-alcoholic").Id,
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Juice",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Non-alcoholic").Id,
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Carbonated drink",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Non-alcoholic").Id,
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Water",
                    ParentId = Categories.FirstOrDefault(x => x.Name == "Non-alcoholic").Id,
                },
            };

    }
}
