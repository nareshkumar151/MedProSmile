using MedProSmile.Models;

namespace MedProSmile.Services
{
    public interface IAuthService
    {
        User GetUserDetails(string username, string password);
        Task<int> ForgotPassword(string username, string newpassword);
    }
}
