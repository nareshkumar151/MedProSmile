using MedProSmile.Models;

namespace MedProSmile.Repository
{
    public interface IAuthRepository
    {
        User GetUserDetails(string username, string password);
        Task<int> ForgotPassword(string username, string newpassword);


    }
}
