using Para.Data.Context;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.Models;

namespace Para.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context,
                      IUserRepository users,
                      ICategoryRepository categories,
                      IProductRepository products,
                      ICartRepository carts,
                      IPaymentRepository payments, 
                      ICouponRepository coupons,
                      IOrderRepository orders
                      ) 
    {
        _context = context;
        Users = users;
        Categories = categories;
        Products = products;
        Carts = carts;
        Payments = payments; 
        Coupons = coupons;
        Orders = orders;
    }

    public IUserRepository Users { get; }
    public ICategoryRepository Categories { get; }
    public IProductRepository Products { get; }
    public ICartRepository Carts { get; }
    public IPaymentRepository Payments { get; } 
    public ICouponRepository Coupons { get; }
    public IOrderRepository Orders { get; }
    public IOrderDetailRepository OrderDetails { get; private set; }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
