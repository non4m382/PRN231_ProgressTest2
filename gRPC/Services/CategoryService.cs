using AutoMapper;
using gRPC.Models;
using gRPC.Protos;
using gRPC.Repositories;
using Grpc.Core;

namespace gRPC.Services
{
    public class CategoryService : gRPC.Protos.CategoryService.CategoryServiceBase
    {
        private readonly ICategoryRepository _repository;

        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Categories> GetCategoryList(Empty request, ServerCallContext context)
        {
            var offersData = await _repository.GetCategoryListAsync();
            Categories response = new Categories();
            foreach (Category offer in offersData)
            {
                response.Items.Add(_mapper.Map<CategoryDetail>(offer));
            }

            return response;
        }

        public override async Task<CategoryDetail> GetCategory(GetCategoryDetailRequest request,
            ServerCallContext context)
        {
            var categoryResult = await _repository.GetCategoryByIdAsync(request.CategoryId);
            return _mapper.Map<CategoryDetail>(categoryResult);
        }

        public override async Task<CategoryDetail> CreateCategory(CreateCategoryDetailRequest request,
            ServerCallContext context)
        {
            Category newCategory = new Category()
            {
                CategoryName = request.Category.CategoryName
            };
            var categoryResult = await _repository.AddCategoryAsync(newCategory);
            return _mapper.Map<CategoryDetail>(categoryResult);
        }

        public override async Task<CategoryDetail> UpdateCategory(UpdateCategoryDetailRequest request,
            ServerCallContext context)
        {
            Category updateCategory = new Category()
            {
                CategoryId = request.Category.CategoryId,
                CategoryName = request.Category.CategoryName
            };
            var categoryResult = await _repository.UpdateCategoryAsync(updateCategory);
            return _mapper.Map<CategoryDetail>(categoryResult);
        }
    }
}