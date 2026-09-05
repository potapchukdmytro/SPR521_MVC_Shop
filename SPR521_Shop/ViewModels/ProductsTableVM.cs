using SPR521_Shop.Models;

namespace SPR521_Shop.ViewModels
{
    public class ProductsTableVM
    {
        public IEnumerable<Product> Products { get; set; } = [];
        public IEnumerable<Category> Categories { get; set; } = [];
    }
}
