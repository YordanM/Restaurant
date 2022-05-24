using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public CategoryRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _dbContext.AddAsync(category);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _dbContext.Remove(category);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(int count, IEnumerable<Category> categories)> GetAllAsync()
        {
            var listCount = _dbContext.Users.Count();

            var all = await _dbContext.Categories.ToListAsync();

            var result = all.OrderBy(c => c.Name);

            return (listCount, result);
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            var category = await _dbContext.Categories.Include(x => x.SubCategories).FirstOrDefaultAsync(u => u.Id == id);

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(string id, Category category)
        {
            var existingCategory = await GetCategoryByIdAsync(id);
            var changedParent = await GetCategoryByIdAsync(category.ParentId);

            existingCategory.Name = category.Name;
            existingCategory.ParentId = category.ParentId;
            existingCategory.ParentCategory = changedParent;

            await _dbContext.SaveChangesAsync();

            return existingCategory;
        }
    }
}
