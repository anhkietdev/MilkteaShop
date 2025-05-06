using BAL.Dtos;
using BAL.Services.Implement;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IStoreService storeService, ILogger<StoreController> logger)
        {
            _storeService = storeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var stores = await _storeService.GetAllStoreAsync();
                return Ok(stores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all stores");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while retrieving stores",
                    Details = ex.Message
                });
            }
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(Guid id)
        //{
        //    try
        //    {
        //        var store = await _storeService.GetStoreByIdAsync(id);
        //        return store == null ? NotFound() : Ok(store);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error getting store with id {Id}", id);
        //        return StatusCode(StatusCodes.Status500InternalServerError, new
        //        {
        //            Message = $"An error occurred while retrieving store with id {id}",
        //            Details = ex.Message
        //        });
        //    }
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);

            return Ok(store);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StoreDto storeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state: {@ModelState}", ModelState);
                    return BadRequest(new
                    {
                        Message = "Invalid store data",
                        Errors = ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage)
                    });
                }

                // Tạo ID mới trước khi gọi service
                var newId = Guid.NewGuid();
                storeDto.Id = newId; // Gán ID mới vào DTO

                // Gọi service (void method)
                await _storeService.CreateStoreAsync(storeDto);

                // Tạo response object nếu cần
                var createdStore = new
                {
                    Id = newId,
                    Name = storeDto.StoreName,
                    // Các properties khác
                };

                // Return 201 Created với location header
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = newId },
                    createdStore);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating store: {@StoreDto}", storeDto);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while creating the store",
                    Details = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StoreDto storeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _storeService.UpdateStoreAsync(id, storeDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Store with id {Id} not found", id);
                return NotFound(new { Message = $"Store with id {id} not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating store with id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = $"An error occurred while updating store with id {id}",
                    Details = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _storeService.DeleteStoreAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting store with id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = $"An error occurred while deleting store with id {id}",
                    Details = ex.Message
                });
            }
        }
    }
}