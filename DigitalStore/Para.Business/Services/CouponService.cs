using AutoMapper;
using Para.Business.Services.Interfaces;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.DTOs.Coupon;
using Para.Schema.Entities.Models;

namespace Para.Business.Services;

public class CouponService : ICouponService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CouponService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CouponDTO> CreateCouponAsync(CreateCouponDTO createCouponDTO)
    {
        var coupon = _mapper.Map<Coupon>(createCouponDTO);
        coupon.IsActive = true;
        coupon.IsUsed = false;

        await _unitOfWork.Coupons.AddAsync(coupon);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<CouponDTO>(coupon);
    }

    public async Task<IEnumerable<CouponDTO>> GetAllCouponsAsync()
    {
        var coupons = await _unitOfWork.Coupons.GetAllAsync();
        return _mapper.Map<IEnumerable<CouponDTO>>(coupons);
    }

    public async Task DeleteCouponAsync(int id)
    {
        var coupon = await _unitOfWork.Coupons.GetByIdAsync(id);
        if (coupon == null)
            throw new KeyNotFoundException("Coupon not found");

        _unitOfWork.Coupons.Remove(coupon);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeactivateExpiredCouponsAsync()
    {
        var expiredCoupons = await _unitOfWork.Coupons
            .GetAllAsync(c => c.ExpiryDate <= DateTime.UtcNow && c.IsActive);

        foreach (var coupon in expiredCoupons)
        {
            coupon.IsActive = false;
            _unitOfWork.Coupons.Update(coupon);
        }

        await _unitOfWork.CommitAsync();
    }
}
