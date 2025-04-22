
using BAL.Dtos;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
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
            var categories = await _comboItemService.GetAllComboItemAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var category = await _comboItemService.GetComboItemByIdAsync(id);

            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComboItemDto comboItemDto)
        {
            await _comboItemService.CreateComboItemAsync(comboItemDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ComboItemDto comboItemDto)
        {
            await _comboItemService.UpdateComboItemAsync(id, comboItemDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _comboItemService.DeleteComboItemAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

