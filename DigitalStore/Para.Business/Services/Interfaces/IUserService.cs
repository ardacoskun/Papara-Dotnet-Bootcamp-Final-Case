using Para.Schema.Entities.DTOs.User;
using Para.Schema.Entities.Models;

namespace Para.Business.Services.Interfaces;

public interface IUserService
{
    Task<User> RegisterUserAsync(UserRegisterDTO userRegisterDto,string role);
    Task<string> LoginUserAsync(UserLoginDTO userLoginDto);
}
