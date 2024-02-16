using gRPC.Models;

namespace gRPC.Repositories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetCategoryListAsync();


        public Task<Category> GetCategoryByIdAsync(int Id);


        public Task<Category> AddCategoryAsync(Category category);


        public Task<Category> UpdateCategoryAsync(Category category);


        public Task<bool> DeleteCategoryAsync(int Id);
    }
}