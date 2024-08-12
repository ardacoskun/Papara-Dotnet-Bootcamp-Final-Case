using Microsoft.EntityFrameworkCore;
using Para.Data.Context;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.Models;

namespace Para.Data.Repositories;

public class CouponRepository : Repository<Coupon>, ICouponRepository
{
    public CouponRepository(AppDbContext context) : base(context) { }

    public async Task<Coupon> GetByCodeAsync(string code)
    {
        return await _context.Set<Coupon>().FirstOrDefaultAsync(c => c.Code == code);
    }
}
