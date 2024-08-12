namespace Para.Business.Services.Interfaces;

public interface ICreditCardPaymentService
{
    Task<bool> ProcessPayment(string cardNumber, string cardHolderName, string expirationDate, string cvv, decimal amount);
}
