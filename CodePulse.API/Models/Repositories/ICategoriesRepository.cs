using CodePulse.API.Models.Domain;

namespace CodePulse.API.Models.Repositories
{
    public interface ICategoriesRepository
    {
        Task<Category> AddCategory(Category category);

        Task<Category?> GetCategoryById( Guid id);
        IEnumerable<Category> GetCategoriesByIds(IEnumerable<Guid> ids);
        Task<List<Category>> GetAllCategories();
        Task<Category?> UpdateCategory(Guid id, Category category);
        Task<Category?> DeleteCategory(Guid id);
    }
}
