namespace Para.Data.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }
    ICartRepository Carts { get; }
    IPaymentRepository Payments { get; }
    ICouponRepository Coupons { get; }
    IOrderRepository Orders { get; }
    IOrderDetailRepository OrderDetails { get; }
    Task<int> CommitAsync();
}
