using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Para.Business.Services.Interfaces;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Business.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderSummaryDTO>> GetOrderSummariesAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync(
                o => o.OrderDate >= startDate && o.OrderDate <= endDate);

            return _mapper.Map<IEnumerable<OrderSummaryDTO>>(orders);
        }

        public async Task<IEnumerable<OrderDetailReportDTO>> GetOrderDetailsAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetOrderWithDetailsAsync(orderId);

            return _mapper.Map<IEnumerable<OrderDetailReportDTO>>(order.OrderDetails);
        }

        public async Task<IEnumerable<UserOrderSummaryDTO>> GetUserOrderSummariesAsync(int userId)
        {
            var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId);

            return _mapper.Map<IEnumerable<UserOrderSummaryDTO>>(orders);
        }

        public async Task<IEnumerable<TopSellingProductsDTO>> GetTopSellingProductsAsync(DateTime startDate, DateTime endDate)
        {
            var orderDetails = await _unitOfWork.OrderDetails
                .GetAllAsync(
                    od => od.Order != null && od.Order.OrderDate >= startDate && od.Order.OrderDate <= endDate,
                    include: od => od.Include(od => od.Order).Include(od => od.Product)
                );

            if (orderDetails == null || !orderDetails.Any())
            {
                throw new InvalidOperationException("No order details found for the given date range.");
            }

            return orderDetails
                .GroupBy(od => od.Product.Name)  // Product'ın adını grupluyoruz
                .Select(group => new TopSellingProductsDTO
                {
                    ProductName = group.Key,
                    QuantitySold = group.Sum(od => od.Quantity)
                })
                .OrderByDescending(p => p.QuantitySold)
                .ToList();
        }


    }
}
