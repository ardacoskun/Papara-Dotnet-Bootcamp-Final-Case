using Para.Data.Context;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.Models;

namespace Para.Data.Repositories;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context) { }
}
