using CodePulse.API.Models.Domain;

namespace CodePulse.API.Models.Repositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateBlogPost(BlogPost model);
        Task<BlogPost?> GetBlogPostById(Guid id);
        Task<BlogPost?> GetBlogPostByUrlHandle(string urlHandle);

        Task<List<BlogPost>> GetAllBlogPosts();
        Task<BlogPost?> UpdateBlogPost(Guid id, BlogPost model);
        Task<BlogPost?> DeleteBlogPost(Guid id);
    }
}
