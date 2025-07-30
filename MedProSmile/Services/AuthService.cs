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

       

        

        public User GetUserDetails(string username, string password)
           => _repository.GetUserDetails(username, password);

        public Task<int> ForgotPassword(string username, string newpassword)
          => _repository.ForgotPassword(username, newpassword);
    }
}
