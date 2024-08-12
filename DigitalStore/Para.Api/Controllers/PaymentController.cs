using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Business.Services.Interfaces;
using Para.Schema.Entities.DTOs.Payment;

namespace Para.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;

    public PaymentController(IPaymentService paymentService, IMapper mapper)
    {
        _paymentService = paymentService;
        _mapper = mapper;
    }

    [HttpPost("makePayment")]
    public async Task<IActionResult> MakePayment(PaymentRequestDTO paymentRequest)
    {
        try
        {
            var paymentDto = await _paymentService.MakePaymentAsync(
                paymentRequest.UserId,
                paymentRequest.CardNumber,
                paymentRequest.CardHolderName,
                paymentRequest.ExpirationDate,
                paymentRequest.CVV,
                paymentRequest.CouponCode
            );

            return Ok(new
            {
                Message = "Payment successful.",
                Data = paymentDto
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Detailed = ex.Message });
        }
    }

    [HttpGet("getPaymentHistory/{userId}")]
    public async Task<IActionResult> GetPaymentHistory(int userId)
    {
        var paymentHistory = await _paymentService.GetPaymentHistoryByUserIdAsync(userId);
        return Ok(paymentHistory);
    }
}
