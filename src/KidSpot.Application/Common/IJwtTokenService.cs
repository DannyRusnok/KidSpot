using KidSpot.Domain.Entities;

namespace KidSpot.Application.Common;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
