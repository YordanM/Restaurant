using Restaurant.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<(int count, IEnumerable<Category> categories)> GetAllAsync();

        Task AddCategoryAsync(Category category);

        Task<Category> GetCategoryByIdAsync(string id);

        Task<Category> UpdateCategoryAsync(string id, Category category);

        Task DeleteCategoryAsync(Category category);
    }
}
