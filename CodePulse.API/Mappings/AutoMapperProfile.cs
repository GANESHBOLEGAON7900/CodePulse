using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Models.Repositories;

namespace CodePulse.API.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCategoryRequestDto,Category>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryRequestDto, Category>().ReverseMap();



            CreateMap<AddBlogPostRequestDTO, BlogPost>()
             .ForMember(dest => dest.Categories, opt => opt.Ignore()) // Ignore mapping for now
             .AfterMap<MapCategoriesInBlogPost>();  // Cus
            //CreateMap<AddBlogPostRequestDTO, BlogPost>().ReverseMap();
            CreateMap<BlogPost,BlogPostDTO>().ReverseMap();
            CreateMap<UpdateBlogPostRequestDTO,BlogPost>().ReverseMap();
        }
    }
}
