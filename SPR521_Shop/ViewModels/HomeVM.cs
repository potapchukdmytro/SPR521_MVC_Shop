using SPR521_Shop.Models;

namespace SPR521_Shop.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; } = [];
    }
}
