using BAL.Dtos;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : BaseController
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null)
                return NotFound();
            return Ok(orderItem);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderItemRequestDto orderItemDto)
        {
            if (orderItemDto == null)
            {
                return BadRequest("Invalid data.");
            }

            // Kiểm tra dữ liệu trong orderItemDto
            if (!orderItemDto.ToppingItems.Any())
            {
                return BadRequest("ToppingItems cannot be empty.");
            }

            try
            {
                await _orderItemService.CreateOrderItemAsync(orderItemDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderItemRequestDto orderItemDto)
        {
            await _orderItemService.UpdateOrderItemAsync(id, orderItemDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _orderItemService.DeleteOrderItemAsync(id);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}
