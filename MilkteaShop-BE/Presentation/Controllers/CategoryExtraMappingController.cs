using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryExtraMappingController : ControllerBase
    {   
        private readonly ICategoryExtraMappingService _categoryExtraMappingService;

        public CategoryExtraMappingController(ICategoryExtraMappingService categoryExtraMappingService)
        {
            _categoryExtraMappingService = categoryExtraMappingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryExtraMappings = await _categoryExtraMappingService.GetAllCategoryExtraMappingAsync();
            return Ok(categoryExtraMappings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var categoryExtraMappings = await _categoryExtraMappingService.GetCategoryExtraMappingByIdAsync(id);
            return Ok(categoryExtraMappings);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryExtraMappingDto categoryExtraMappingDto)
        {
            await _categoryExtraMappingService.CreateCategoryExtraMappingAsync(categoryExtraMappingDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryExtraMappingDto categoryExtraMappingDto)
        {
            await _categoryExtraMappingService.UpdateCategoryExtraMappingAsync(id, categoryExtraMappingDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryExtraMappingService.DeleteCategoryExtraMappingAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

