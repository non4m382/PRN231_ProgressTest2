using gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MySaleDBContext _dbContext;


        public CategoryRepository(MySaleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetCategoryListAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int Id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == Id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var result = _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            Category result = await GetCategoryByIdAsync(category.CategoryId);

            _dbContext.Entry(result).CurrentValues.SetValues(category);
            // _dbContext.SaveChanges();

            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int Id)
        {
            var filteredData = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == Id);
            var result = _dbContext.Remove(filteredData);
            await _dbContext.SaveChangesAsync();
            return result != null;
        }
    }
}