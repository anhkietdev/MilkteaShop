using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizeController : BaseController
    {
        private readonly IProductSizeService _productSizeService;
        private readonly IMapper _mapper;
        public ProductSizeController(IProductSizeService productSizeService, IMapper mapper)
        {
            _productSizeService = productSizeService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productSizes = await _productSizeService.GetAllProductSizesAsync();
            return Ok(productSizes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productSize = await _productSizeService.GetProductSizeByIdAsync(id);
            if (productSize == null)
            {
                return NotFound();
            }
            return Ok(productSize);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductSizeRequestDto productSizeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _productSizeService.CreateProductSizeAsync(productSizeDto);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductSizeRequestDto productSizeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _productSizeService.UpdateProductSizeAsync(id, productSizeDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productSizeService.DeleteProductSizeAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
