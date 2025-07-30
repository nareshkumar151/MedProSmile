using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using System.Data;

namespace MedProSmile.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "Auth";


        public AuthRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<User> GetByIdAsync(string username, string password)
        {
            try
            {
                var query = "usp_getUserDetails";
                var parameters = new { UserName = username, Password = password };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<User>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public User GetUserDetails(string username, string password)
        {
            try
            {
                var query = "usp_getUserDetails";
                var parameters = new { UserName = username, Password = password };
                using var connection = _context.CreateConnection();
                return connection.QueryFirstOrDefault<User>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

        public async Task<int> ForgotPassword(string username, string newpassword)
        {
            try
            {
                var query = "usp_forgotPassword";
                var parameters = new { UserName=username, NewPassword= newpassword };
                using var connection = _context.CreateConnection();
                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(ForgotPassword) + " " + _controllerName);
                throw;
            }
        }


    }
}
