using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }
    }
}
