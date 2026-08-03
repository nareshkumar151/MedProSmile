using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace MedProSmile.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "Users";


        public UsersRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllUsers";
                var parameters = new DynamicParameters();
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("PageSize", pageSize);

                using var connection = _context.CreateConnection();

                using var multi = await connection.QueryMultipleAsync(query, parameters, commandType: CommandType.StoredProcedure);

                var totalCount = await multi.ReadFirstAsync<int>();
                var employees = (await multi.ReadAsync<dynamic>()).ToList();

                return new PagedResult<dynamic>
                {
                    Items = employees,
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetAllPagedAsync)+" "+ _controllerName);
                throw;
            }
        }

        public async Task<dynamic> GetByIdAsync(int id)
        {
            try
            {
                var query = "usp_GetUserById";
                var parameters = new { UserId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(Users users)
        {
            try
            {
                var query = "usp_CreateUser";
                var parameters = new {
                    users.HospitalId,
                    users.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(users.PasswordHash),
                    users.RoleId,
                    users.Status,
                    users.CreatedBy
                };
                using var connection = _context.CreateConnection();
                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(CreateAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> UpdateAsync(UsersUpdate usersUpdate)
        {
            try
            {
                var query = "usp_UpdateUser";
                using var connection = _context.CreateConnection();

                var passwordHash = usersUpdate.PasswordHash;
                if (string.IsNullOrWhiteSpace(passwordHash))
                {
                    passwordHash = await connection.QueryFirstOrDefaultAsync<string>(
                        "SELECT PasswordHash FROM Users WHERE UserId = @UserId",
                        new { usersUpdate.UserId });
                }
                else
                {
                    passwordHash = BCrypt.Net.BCrypt.HashPassword(passwordHash);
                }

                var parameters = new {
                    usersUpdate.UserId,
                    usersUpdate.HospitalId,
                    usersUpdate.Username ,
                    PasswordHash = passwordHash,
                    usersUpdate.RoleId,
                    usersUpdate.Status,
                    usersUpdate.UpdatedBy

                };
                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(UpdateAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> DeleteAsync(UsersDelete usersDelete)
        {
            try
            {
                var query = "usp_DeleteUser";
                var parameters = new { UserId = usersDelete.UserId, UpdatedBy = usersDelete.UpdatedBy};
                using var connection = _context.CreateConnection();
                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(DeleteAsync) + " " + _controllerName);
                throw;
            }
        }
    }
}
