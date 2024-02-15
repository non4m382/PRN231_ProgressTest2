using AutoMapper;
using gRPC.Models;
using gRPC.Protos;

namespace gRPC.AutoMapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDetail>().ReverseMap();
        }
    }
}