using SPR521_Shop.Extensions;
using SPR521_Shop.ViewModels;

namespace SPR521_Shop.Services
{
    public class CartService
    {
        private const string _key = "d993c15400936e316a27606dd3dd99469e21cf9cb6865db4309ed1b6d1351210";
        private readonly HttpContext _context;

        public CartService(IHttpContextAccessor accessor)
        {
            if (accessor.HttpContext == null)
            {
                throw new ArgumentNullException("Http context is null");
            }

            _context = accessor.HttpContext;
        }

        public List<CartItemVM> GetItems()
        {
            var items = _context.Session.Get<List<CartItemVM>>(_key);

            return items ?? [];
        }

        public bool IsInCart(int productId)
        {
            var items = GetItems();
            return items.Any(i => i.ProductId == productId);
        }

        public int Count()
        {
            var items = GetItems();
            return items.Sum(i => i.Count);
        }

        public void Add(int productId)
        {
            if(!IsInCart(productId))
            {
                var items = GetItems();
                items.Add(new CartItemVM { ProductId = productId });
                _context.Session.Set(_key, items);
            }
        }

        public void Remove(int productId)
        {
            if (IsInCart(productId))
            {
                var items = GetItems();
                items = items.Where(i => i.ProductId != productId).ToList();
                _context.Session.Set(_key, items);
            }
        }
    }
}
