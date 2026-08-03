using MedProSmile.Models;
using MedProSmile.Repository;

namespace MedProSmile.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;

        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

       

        

        public User? GetUserDetails(string username, string password)
        {
            var user = _repository.GetUserDetails(username);
            if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public Task<int> ForgotPassword(string username, string newpassword)
          => _repository.ForgotPassword(username, newpassword);
    }
}
