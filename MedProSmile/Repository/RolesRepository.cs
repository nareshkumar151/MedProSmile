using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace MedProSmile.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "Roles";


        public RolesRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllRoles";
                var parameters = new DynamicParameters();
               

                using var connection = _context.CreateConnection();

                using var multi = await connection.QueryMultipleAsync(query, parameters, commandType: CommandType.StoredProcedure);

                var employees = (await multi.ReadAsync<dynamic>()).ToList();

                return new PagedResult<dynamic>
                {
                    Items = employees
                    
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
                var query = "usp_GetRoleById";
                var parameters = new { RoleId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(string RoleName)
        {
            try
            {
                var query = "usp_CreateRole";
                var parameters = new {
                    RoleName
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

        public async Task<int> UpdateAsync(int RoleId, string RoleName)
        {
            try
            {
                var query = "usp_UpdateRole";
                var parameters = new {
                    RoleId,
                    RoleName

                };
                using var connection = _context.CreateConnection();
                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(UpdateAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> DeleteAsync(int RoleId)
        {
            try
            {
                var query = "usp_DeleteRole";
                var parameters = new { RoleId = RoleId};
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
