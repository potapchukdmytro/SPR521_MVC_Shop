using Microsoft.EntityFrameworkCore;

namespace SPR521_Shop
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
