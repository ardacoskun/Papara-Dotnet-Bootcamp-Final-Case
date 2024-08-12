using Para.Schema.Entities.DTOs.Payment;

namespace Para.Business.Services.Interfaces;

public interface IPaymentService
{
    Task<PaymentDTO> MakePaymentAsync(int userId, string cardNumber, string cardHolderName, string expirationDate, string cvv, string? coupnCode);
    Task<IEnumerable<PaymentDTO>> GetPaymentHistoryByUserIdAsync(int userId);
}
