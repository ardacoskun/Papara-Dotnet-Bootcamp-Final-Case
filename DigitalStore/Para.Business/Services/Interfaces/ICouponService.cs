using Para.Schema.Entities.DTOs.Coupon;

namespace Para.Business.Services.Interfaces;

public interface ICouponService
{
    Task<CouponDTO> CreateCouponAsync(CreateCouponDTO createCouponDTO);
    Task<IEnumerable<CouponDTO>> GetAllCouponsAsync();
    Task DeleteCouponAsync(int id);
    Task DeactivateExpiredCouponsAsync();
}
