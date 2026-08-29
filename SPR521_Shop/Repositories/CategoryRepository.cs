using Microsoft.EntityFrameworkCore;
using SPR521_Shop.Models;
using SPR521_Shop.ViewModels;

namespace SPR521_Shop.Repositories
{
    public class CategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories.AsNoTracking();

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<string?> CreateAsync(CategoryCreateVM vm)
        {
            bool res = await IsExistsAsync(vm.Name!);

            if (res)
            {
                return $"Категорія '{vm.Name}' вже існує";
            }

            var model = new Category
            {
                Name = vm.Name!,
                Description = vm.Description,
                Image = vm.Image
            };

            await _context.Categories.AddAsync(model);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<bool> IsExistsAsync(string name, int id = 0)
        {
            return await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.Id != id);
        }

        public async Task<string?> UpdateAsync(CategoryUpdateVM vm)
        {
            bool res = await IsExistsAsync(vm.Name!, vm.Id);

            if (res)
            {
                return $"Категорія '{vm.Name}' вже існує";
            }

            var category = await GetByIdAsync(vm.Id);

            if(category == null)
            {
                return $"Категорія з id '{vm.Id}' не існує";
            }

            category.Description = vm.Description;
            category.Name = vm.Name!;
            category.Image = vm.Image;

            await _context.SaveChangesAsync();

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);

            if(category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
