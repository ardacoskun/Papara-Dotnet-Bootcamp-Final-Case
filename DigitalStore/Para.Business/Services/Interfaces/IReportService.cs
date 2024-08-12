using Para.Schema.Entities.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Business.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<OrderSummaryDTO>> GetOrderSummariesAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<OrderDetailReportDTO>> GetOrderDetailsAsync(int orderId);
        Task<IEnumerable<UserOrderSummaryDTO>> GetUserOrderSummariesAsync(int userId);
        Task<IEnumerable<TopSellingProductsDTO>> GetTopSellingProductsAsync(DateTime startDate, DateTime endDate);
    }
}
