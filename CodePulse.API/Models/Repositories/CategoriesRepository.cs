using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CodePulse.API.Models.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoriesRepository(ApplicationDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public async Task<Category> AddCategory(Category category)
        {
            var res= await _dbContext.Categories.AddAsync(category);

            await _dbContext.SaveChangesAsync();

            return res.Entity;
        }

        public async Task<List<Category>> GetAllCategories()
        {
           var categories= await _dbContext.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            var model = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (model==null)
            {
                return null;
            }
            return model;
        }

        public IEnumerable<Category> GetCategoriesByIds(IEnumerable<Guid> ids)
        {
            return _dbContext.Categories.Where(c => ids.Contains(c.Id)).ToList();
        }

        public async Task<Category?> UpdateCategory(Guid id, Category category)
        {
            var model = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return null;
            }

            model.Name = category.Name;
            model.UrlHandle = category.UrlHandle;
           await _dbContext.SaveChangesAsync();
            return model;
        }
        public async Task<Category?> DeleteCategory(Guid id)
        {
            var model = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return null;
            }

           var category= _dbContext.Categories.Remove(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }
    }
}
