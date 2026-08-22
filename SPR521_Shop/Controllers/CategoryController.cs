using Microsoft.AspNetCore.Mvc;

namespace SPR521_Shop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
