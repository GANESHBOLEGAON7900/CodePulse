using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          var images= await _imageRepository.GetAll();

            var  response=new List<BlogImageDto>();
            foreach (var image in images)
            {
                response.Add(new BlogImageDto()
                {
                    Id = image.Id,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    Url = image.Url
                });
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file
            , [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);
            if (ModelState.IsValid)
            {
                var blogImage = new BlogImage()
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                blogImage =await _imageRepository.Upload(file,blogImage);

                var dto = new BlogImageDto()
                {
                    Id = blogImage.Id,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    Url = blogImage.Url
                };

                return Ok(dto);
            }
            return BadRequest(ModelState);
        }
        [NonAction]
        public void ValidateFileUpload(IFormFile file)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("File", "Unsupported file format"); 
            }
            if (file.Length>10485760)
            {
                ModelState.AddModelError("File","File size should not exceed 10MB");
            }
        }
    }
}
