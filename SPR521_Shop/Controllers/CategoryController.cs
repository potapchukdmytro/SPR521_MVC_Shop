using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR521_Shop.Models;

namespace SPR521_Shop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var categories = _context.Categories.AsEnumerable();
            IEnumerable<Category> categories = _context.Categories;

            return View(categories);
        }

        public void Any(object obj)
        {
            DateTime? dt = obj as DateTime?;

            if(dt == null)
            { 
            }
        }
    }
}
