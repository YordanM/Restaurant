using Restaurant.Data.Repositories;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var allCategories = await _categoryRepository.GetAllAsync();

            var isUnique = allCategories.categories.Any(c => c.Name == category.Name);

            if (isUnique)
            {
                throw new Exception("Category already exists.");
            }

            var parent = allCategories.categories.FirstOrDefault(x => x.Id == category.ParentId);

            if (parent == null && category.ParentId != null)
            {
                throw new Exception("There is not such a category for parent.");
            }

            await _categoryRepository.AddCategoryAsync(category);

            return category;
        }

        public async Task DeleteCategoryAsync(string id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                throw new Exception("Category not found!");
            }


            //check if products are in category
            var products = await _productRepository.GetAllAsync();

            var productsInCategory = products.Any(x => x.CategoryId == id);

            if (productsInCategory)
            {
                throw new Exception("There is product in this category.");
            }

            //clear all subCategories parent
            var subCategories = category.SubCategories;

            foreach (var subCategory in subCategories)
            {
                subCategory.ParentId = null;
            }

            await _categoryRepository.DeleteCategoryAsync(category);
        }

        public async Task<(int count, IEnumerable<Category> categories)> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(string id, Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var allCategories = await _categoryRepository.GetAllAsync();

            var isUnique = allCategories.categories.Any(c => c.Name == category.Name);
            var existingCategory = allCategories.categories.FirstOrDefault(c => c.Id == id);

            if (existingCategory == null)
            {
                throw new Exception("No such category exists.");
            }

            if (isUnique && category.Name != existingCategory.Name)
            {
                throw new Exception("Category name already exists.");
            }

            return await _categoryRepository.UpdateCategoryAsync(id, category);
        }
    }
}
