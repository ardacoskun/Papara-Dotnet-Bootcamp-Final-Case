using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Business.Services.Interfaces;
using Para.Schema.Entities.DTOs.User;

namespace Para.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDTO userRegisterDto)
    {
        try
        {
            var user = await _userService.RegisterUserAsync(userRegisterDto, "User");
            var userDto = _mapper.Map<UserDTO>(user);

            var response = ApiResponse<UserDTO>.SuccessResponse(userDto, "User registered successfully. Please log in.");
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<UserDTO>.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<UserDTO>.ErrorResponse("An unexpected error occurred. " + ex.Message));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDTO userLoginDto)
    {
        try
        {
            var token = await _userService.LoginUserAsync(userLoginDto);
            var response = ApiResponse<string>.SuccessResponse(token, "Login successful.");
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ApiResponse<string>.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.ErrorResponse("An unexpected error occurred. " + ex.Message));
        }
    }
}
