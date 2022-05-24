using Restaurant.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.API.Models
{
    public class CategoryRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public string? ParentId { get; set; }

        public Category ToCategoryModel()
            => new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                ParentId = ParentId
            };
    }
}
