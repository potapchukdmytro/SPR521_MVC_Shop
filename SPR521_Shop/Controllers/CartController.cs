using Microsoft.AspNetCore.Mvc;
using SPR521_Shop.Extensions;
using SPR521_Shop.Models;
using SPR521_Shop.Repositories;
using SPR521_Shop.Services;

namespace SPR521_Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly CartRepository _cartRepository;

        public CartController(CartService cartService, CartRepository cartRepository)
        {
            _cartService = cartService;
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> Add(int productId)
        {
            var userId = this.GetUserId();
            if(userId != null)
            {
                await _cartRepository.AddAsync(userId, productId);
            }

            _cartService.Add(productId);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove(int productId)
        {
            var userId = this.GetUserId();
            if (userId != null)
            {
                await _cartRepository.RemoveAsync(userId, productId);
            }

            _cartService.Remove(productId);

            return RedirectToAction("Index", "Home");
        }
    }
}
