using SPR521_Shop.Models;

namespace SPR521_Shop.ViewModels
{
    public class ProductsTableVM
    {
        public IEnumerable<Product> Products { get; set; } = [];
        public IEnumerable<Category> Categories { get; set; } = [];
        public int Page { get; set; } = 1;
        public int PageCount { get; set; } = 1;
        public string? Category { get; set; }
    }
}
