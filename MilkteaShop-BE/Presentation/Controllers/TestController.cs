using DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    // Tạo một controller mới hoặc thêm vào controller hiện có
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TestController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                // Test 1: Kiểm tra kết nối cơ bản
                bool canConnect = await _dbContext.Database.CanConnectAsync();

                // Test 2: Thử đọc dữ liệu đơn giản
                // Thay "Users" bằng tên bảng thực tế trong DB của bạn
                var testQuery = await _dbContext.Database
                    .ExecuteSqlRawAsync("SELECT 1");

                return Ok(new
                {
                    ConnectionSuccessful = canConnect,
                    QueryExecuted = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }
    }
}
