using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Models.Repositories;

namespace CodePulse.API.Mappings
{
    public class MapCategoriesInBlogPost : IMappingAction<AddBlogPostRequestDTO, BlogPost>
    {
        private readonly ICategoriesRepository _categoryRepository;

        public MapCategoriesInBlogPost(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Process(AddBlogPostRequestDTO source, BlogPost destination, ResolutionContext context)
        {
            var categories = _categoryRepository
                .GetCategoriesByIds(source.Categories)
                .ToList();

            destination.Categories = categories;
        }
    }

}
