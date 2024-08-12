using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Business.Services.Interfaces;
using Para.Schema.Entities.DTOs.User;

namespace Para.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")] 
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AdminController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAdmin(UserRegisterDTO userRegisterDto)
    {
        try
        {
            // Kullanıcı kaydını gerçekleştir
            var user = await _userService.RegisterUserAsync(userRegisterDto, "admin");
            var userDto = _mapper.Map<UserDTO>(user);

            // Başarılı response
            var response = ApiResponse<UserDTO>.SuccessResponse(userDto, "Admin user registered successfully.");
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            // Çakışma (Conflict) durumunda response
            return Conflict(ApiResponse<UserDTO>.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            // Beklenmeyen hata durumunda response
            return StatusCode(500, ApiResponse<UserDTO>.ErrorResponse("An unexpected error occurred. " + ex.Message));
        }
    }
}