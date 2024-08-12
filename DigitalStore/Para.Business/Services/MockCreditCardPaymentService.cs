using Para.Business.Services.Interfaces;

namespace Para.Business.Services;

public class MockCreditCardPaymentService : ICreditCardPaymentService
{
    public Task<bool> ProcessPayment(string cardNumber, string cardHolderName, string expirationDate, string cvv, decimal amount)
    {
        return Task.FromResult(true);
    }
}