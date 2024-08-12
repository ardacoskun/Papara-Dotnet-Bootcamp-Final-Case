using Para.Schema.Entities.Models;

namespace Para.Data.Repositories.Interfaces;

public interface ICouponRepository : IRepository<Coupon>
{
    Task<Coupon> GetByCodeAsync(string code);
}
