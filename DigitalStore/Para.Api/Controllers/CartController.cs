using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Business.Services.Interfaces;
using Para.Schema.Entities.DTOs.Cart;

namespace Para.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(int userId)
    {
        var cart = await _cartService.GetCartByUserIdAsync(userId);
        return Ok(cart);
    }

    [HttpPost("{userId}/add")]
    public async Task<IActionResult> AddToCart(int userId, CreateCartDTO createCartDto)
    {
        try
        {
            var cart = await _cartService.AddToCartAsync(userId, createCartDto);
            return Ok(cart);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Detailed = ex.Message });
        }
    }

    [HttpPut("{userId}/update")]
    public async Task<IActionResult> UpdateCartItem(int userId, CreateCartDTO createCartDto)
    {
        var cart = await _cartService.UpdateCartItemAsync(userId, createCartDto);
        return Ok(cart);
    }

    [HttpDelete("{userId}/remove/{productId}")]
    public async Task<IActionResult> RemoveFromCart(int userId, int productId)
    {
        await _cartService.RemoveFromCartAsync(userId, productId);
        return NoContent();
    }

    [HttpDelete("{userId}/clear")]
    public async Task<IActionResult> ClearCart(int userId)
    {
        await _cartService.ClearCartAsync(userId);
        return NoContent();
    }
}
