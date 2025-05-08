using BAL.Dtos;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboItemController : BaseController
    {
        private readonly IComboItemService _comboItemService;

        public ComboItemController(IComboItemService comboItemService)
        {
            _comboItemService = comboItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comboItems = await _comboItemService.GetAllComboItemAsync();
                return Ok(comboItems);
            }
            catch (Exception ex)
            {
                // Log the error (you can replace this with your logger if you have one)
                return StatusCode(500, new { message = "An error occurred while fetching combo items.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var comboItem = await _comboItemService.GetComboItemByIdAsync(id);
                return Ok(comboItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the combo item.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComboItemDto comboItemDto)
        {
            try
            {
                await _comboItemService.CreateComboItemAsync(comboItemDto);
                return CreatedAtAction(nameof(GetById), new { id = comboItemDto.Id }, comboItemDto); // Return 201 status with the created item
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = "Invalid input: " + ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the combo item.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ComboItemDto comboItemDto)
        {
            try
            {
                await _comboItemService.UpdateComboItemAsync(id, comboItemDto);
                return Ok(new { message = "ComboItem updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "Invalid data: " + ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the combo item.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _comboItemService.DeleteComboItemAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Combo item with ID {id} not found." });
                }
                return Ok(new { message = "Combo item deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the combo item.", error = ex.Message });
            }
        }
    }
}
