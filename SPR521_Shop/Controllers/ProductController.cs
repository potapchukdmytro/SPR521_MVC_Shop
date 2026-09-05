using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR521_Shop.Models;
using SPR521_Shop.Repositories;
using SPR521_Shop.ViewModels;

namespace SPR521_Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public ProductController(ProductRepository productRepository, CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(string? category)
        {
            IQueryable<Product> products = _productRepository.Products
                .Include(p => p.Category);

            if(!string.IsNullOrEmpty(category))
            {
                products = products
                    .Where(p => p.Category!.Name.ToLower() == category.ToLower());
            }

            var viewModel = new ProductsTableVM
            {
                Products = products,
                Categories = await _categoryRepository.Categories.ToListAsync()
            };

            return View(viewModel);
        }
    }
}
