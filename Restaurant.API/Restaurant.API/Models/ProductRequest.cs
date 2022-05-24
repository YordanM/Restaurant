using Restaurant.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.API.Models
{
    public class ProductRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public Product ToProductModel()
            => new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                CategoryId = CategoryId,
                Description = Description,
                Price = Price
            };
    }
}
