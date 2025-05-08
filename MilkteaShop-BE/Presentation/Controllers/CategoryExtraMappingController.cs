using BAL.Dtos;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            try
            {
                var categoryExtraMappings = await _categoryExtraMappingService.GetAllCategoryExtraMappingAsync();
                return Ok(new { message = "Category extra mappings retrieved successfully.", data = categoryExtraMappings });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var categoryExtraMapping = await _categoryExtraMappingService.GetCategoryExtraMappingByIdAsync(id);
                return Ok(new { message = "Category extra mapping retrieved successfully.", data = categoryExtraMapping });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryExtraMappingDto categoryExtraMappingDto)
        {
            try
            {
                if (categoryExtraMappingDto == null)
                {
                    return BadRequest(new { message = "Invalid category extra mapping data." });
                }

                await _categoryExtraMappingService.CreateCategoryExtraMappingAsync(categoryExtraMappingDto);
                return Ok(new { message = "Category extra mapping created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryExtraMappingDto categoryExtraMappingDto)
        {
            try
            {
                await _categoryExtraMappingService.UpdateCategoryExtraMappingAsync(id, categoryExtraMappingDto);
                return Ok(new { message = "Category extra mapping updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _categoryExtraMappingService.DeleteCategoryExtraMappingAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Category extra mapping not found for deletion." });
                }
                return Ok(new { message = "Category extra mapping deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
