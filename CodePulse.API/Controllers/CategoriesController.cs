using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoriesRepository categoriesRepository,IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequestDto request)
        {
            var category = _mapper.Map<Category>(request);
            var model=await _categoriesRepository.AddCategory(category);

            return Ok(model);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories =  await _categoriesRepository.GetAllCategories();

            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid id)
        {
          var model= await _categoriesRepository.GetCategoryById(id);
            if (model==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDto>(model));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto request)
        {
            var category = _mapper.Map<Category>(request);
            var model = await _categoriesRepository.UpdateCategory(id,category);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDto>(model));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var res = await _categoriesRepository.DeleteCategory(id);
            if (res == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
