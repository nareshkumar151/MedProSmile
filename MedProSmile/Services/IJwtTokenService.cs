using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
