using Restaurant.Domain.Models;

namespace Restaurant.API.Models
{
    public class CategoryResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Category ParentCategory { get; set; }
    }
}
