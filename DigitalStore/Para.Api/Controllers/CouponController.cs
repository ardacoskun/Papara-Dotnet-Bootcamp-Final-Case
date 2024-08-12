using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Business.Services.Interfaces;
using Para.Schema.Entities.DTOs.Coupon;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] 
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponDTO createCouponDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coupon = await _couponService.CreateCouponAsync(createCouponDto);
            return Ok(coupon);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCoupons()
        {
            var coupons = await _couponService.GetAllCouponsAsync();
            return Ok(coupons);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            try
            {
                await _couponService.DeleteCouponAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Coupon not found" });
            }
        }
    }
}
