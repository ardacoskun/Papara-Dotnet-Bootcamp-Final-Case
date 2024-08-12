using Para.Schema.Entities.Models;

namespace Para.Business.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
