using AutoMapper;
using Para.Business.Services.Interfaces;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.DTOs.User;
using Para.Schema.Entities.Models;

namespace Para.Business.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<User> RegisterUserAsync(UserRegisterDTO userRegisterDto, string role)
    {
        if (await _unitOfWork.Users.GetByEmailAsync(userRegisterDto.Email) != null)
            throw new InvalidOperationException("A user with this email already exists.");

        var user = _mapper.Map<User>(userRegisterDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);
        user.Role = role == "admin" ? "Admin" : "User";

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CommitAsync();
        return user;
    }

    public async Task<string> LoginUserAsync(UserLoginDTO userLoginDto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(userLoginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return _tokenService.GenerateToken(user);
    }
}
