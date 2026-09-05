using Microsoft.EntityFrameworkCore;
using SPR521_Shop.Models;

namespace SPR521_Shop.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products.AsNoTracking();
    }
}
