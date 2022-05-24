using Restaurant.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Business.Services
{
    public interface ICategoryService
    {
        Task<(int count, IEnumerable<Category> categories)> GetAllAsync();

        Task<Category> AddCategoryAsync(Category category);

        Task<Category> GetCategoryByIdAsync(string id);

        Task<Category> UpdateCategoryAsync(string id, Category category);

        Task DeleteCategoryAsync(string id);
    }
}
