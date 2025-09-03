using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;

        public ICategoriesRepository _categoriesRepository { get; }

        public BlogPostController(IBlogPostRepository blogPostRepository,IMapper mapper,ICategoriesRepository categoriesRepository)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] AddBlogPostRequestDTO  addBlogPostRequestDTO)
        {
            var model = await _blogPostRepository.CreateBlogPost(_mapper.Map<BlogPost>(addBlogPostRequestDTO));

            return Ok(_mapper.Map<BlogPostDTO>(model));
        }



        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var models = await _blogPostRepository.GetAllBlogPosts();
           
            return Ok(_mapper.Map<List<BlogPostDTO>>(models));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var model = await _blogPostRepository.GetBlogPostById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BlogPostDTO>(model));
        }



        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            var model = await _blogPostRepository.GetBlogPostByUrlHandle(urlHandle);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BlogPostDTO>(model));
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, [FromBody] UpdateBlogPostRequestDTO request)
        {
            // var category = _mapper.Map<BlogPost>(request);
            var blogPost = new BlogPost()
            {
                Title = request.Title,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };
            foreach (var categoryGuid   in request.Categories)
            {
                var existingCategory = await _categoriesRepository.GetCategoryById(categoryGuid);

                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }

            }
            var model = await _blogPostRepository.UpdateBlogPost(id, blogPost);
            if (model == null)
            {
                return NotFound();
            }
            var response = new BlogPostDTO()
            {
                Id=id,
                Title = model.Title,
                Author = model.Author,
                Content = model.Content,
                FeaturedImageUrl = model.FeaturedImageUrl,
                IsVisible = model.IsVisible,
                PublishedDate = model.PublishedDate,
                ShortDescription = model.ShortDescription,
                UrlHandle = model.UrlHandle,
                Categories = model.Categories.Select(x => new CategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
           // return Ok(_mapper.Map<BlogPostDTO>(model));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var res = await _blogPostRepository.DeleteBlogPost(id);
            if (res == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
