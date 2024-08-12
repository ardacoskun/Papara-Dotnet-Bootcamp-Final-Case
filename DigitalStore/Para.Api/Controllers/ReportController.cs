using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Business.Services.Interfaces;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("order-summary")]
        public async Task<IActionResult> GetOrderSummaries([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var summaries = await _reportService.GetOrderSummariesAsync(startDate, endDate);
            return Ok(summaries);
        }

        [HttpGet("order-details/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var details = await _reportService.GetOrderDetailsAsync(orderId);
            return Ok(details);
        }

        [HttpGet("user-order-summary/{userId}")]
        public async Task<IActionResult> GetUserOrderSummaries(int userId)
        {
            var summaries = await _reportService.GetUserOrderSummariesAsync(userId);
            return Ok(summaries);
        }

        [HttpGet("top-selling-products")]
        public async Task<IActionResult> GetTopSellingProducts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var topProducts = await _reportService.GetTopSellingProductsAsync(startDate, endDate);
            return Ok(topProducts);
        }
    }
}
