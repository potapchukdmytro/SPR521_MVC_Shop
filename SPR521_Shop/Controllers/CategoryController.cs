using Microsoft.AspNetCore.Mvc;
using SPR521_Shop.Models;
using SPR521_Shop.Repositories;
using SPR521_Shop.ViewModels;

namespace SPR521_Shop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            //var categories = _context.Categories.AsEnumerable();
            IEnumerable<Category> categories = _categoryRepository.Categories;

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateVM vm)
        {
            return View();
        }
    }
}
