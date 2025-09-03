using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Models.Repositories
{
    public class BlogPostRepository:IBlogPostRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
      

        public async Task<BlogPost> CreateBlogPost(BlogPost model)
        {
            var res = await _dbContext.BlogPosts.AddAsync(model);

            await _dbContext.SaveChangesAsync();

            return res.Entity;
        }

        public async Task<BlogPost?> GetBlogPostById(Guid id)
        {
            var model = await _dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return null;
            }
            return model;
        }
        public async Task<BlogPost?> GetBlogPostByUrlHandle(string urlHandle)
        {
            var model = await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task<List<BlogPost>> GetAllBlogPosts()
        {
            var blogPost = await _dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();

            return blogPost;
        }

        public async Task<BlogPost?> UpdateBlogPost(Guid id, BlogPost model)
        {
            var existingModel = await _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return null;
            }

            existingModel.Title = model.Title;
            existingModel.UrlHandle = model.UrlHandle;
            existingModel.FeaturedImageUrl = model.FeaturedImageUrl;
            existingModel.PublishedDate = model.PublishedDate;
            existingModel.IsVisible = model.IsVisible;
            existingModel.Content = model.Content;
            existingModel.Author = model.Author;
            existingModel.ShortDescription = model.ShortDescription;
            //_dbContext.Entry(existingModel).CurrentValues.SetValues(model);
            existingModel.Categories = model.Categories;

            await _dbContext.SaveChangesAsync();
            return existingModel;
        }

        public async Task<BlogPost?> DeleteBlogPost(Guid id)
        {
            var model = await _dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return null;
            }

            var category = _dbContext.BlogPosts.Remove(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

      
    }
}
