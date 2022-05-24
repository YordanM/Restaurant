using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain.Models
{
    public class Category
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Category ParentCategory { get; set; }

        public virtual List<Category> SubCategories { get; set; }
    }
}
