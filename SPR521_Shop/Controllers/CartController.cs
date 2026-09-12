using Microsoft.AspNetCore.Mvc;
using SPR521_Shop.Services;

namespace SPR521_Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Add(int productId)
        {
            _cartService.Add(productId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int productId)
        {
            _cartService.Remove(productId);

            return RedirectToAction("Index", "Home");
        }
    }
}
