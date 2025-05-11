using BAL.Dtos;
using BAL.Services.Implement;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : BaseController
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vouchers = await _voucherService.GetAllAsync();
            return Ok(vouchers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var voucher = await _voucherService.GetByIdAsync(id);
            if (voucher == null)
                return NotFound();
            return Ok(voucher);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoucherRequestDto voucherDto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            await _voucherService.CreateAsync(voucherDto);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] VoucherRequestDto voucherDto)
        {
            await _voucherService.UpdateAsync(id, voucherDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _voucherService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
        [HttpGet("{orderId}/related-amount")]
        public async Task<IActionResult> GetOrderRelatedAmount(Guid orderId)
        {
            try
            {
                var result = await _voucherService.GetOrderRelatedAmount(orderId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
