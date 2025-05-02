using BAL.Dtos;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto orderDto)
        {
            var result = await _orderService.CreateOrderAsync(orderDto);
            if (result is null)
            {
                return BadRequest(new { message = "Order creation failed" });
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderService = await _orderService.GetAllAsync();
            return Ok(orderService);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var orderService = await _orderService.GetOrderByIdAsync(id);

            return Ok(orderService);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] OrderRequestDto orderDto)
        {
            var result = await _orderService.UpdateOrderAsync(id, orderDto);
            if (result is null)
            {
                return BadRequest(new { message = "Order update failed" });
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result)
            {
                return BadRequest(new { message = "Order deletion failed" });
            }
            return Ok(new { message = "Order deleted successfully" });
        }
    }
}
