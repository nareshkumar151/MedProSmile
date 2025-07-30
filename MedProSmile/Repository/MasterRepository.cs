using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using System.Data;

namespace MedProSmile.Repository
{
    public class MasterRepository : IMasterRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "Master";


        public MasterRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }


        public async Task<PagedResult<dynamic>> GetAllDepartmentCategories()
        {
            try
            {
                var query = "usp_GetAllDepartmentCategories";
                var parameters = new DynamicParameters();
               
                using var connection = _context.CreateConnection();

                using var multi = await connection.QueryMultipleAsync(query,parameters, commandType: CommandType.StoredProcedure);

                var totalCount = await multi.ReadFirstAsync<int>();
                var departmentCategories = (await multi.ReadAsync<dynamic>()).ToList();

                return new PagedResult<dynamic>
                {
                    Items = departmentCategories
                   
                };
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetAllPagedAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<dynamic> GetDepartmentCategoryById(int id)
        {
            try
            {
                var query = "usp_GetDepartmentCategoryById";
                var parameters = new { CategoryId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetDepartmentCategoryById) + " " + _controllerName);
                throw;
            }
        }


        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllDepartments";
                var parameters = new DynamicParameters();
                //parameters.Add("PageNumber", pageNumber);
                //parameters.Add("PageSize", pageSize);

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
                var query = "usp_GetDepartmentById";
                var parameters = new { DepartmentId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(Department dep)
        {
            try
            {
                var query = "usp_CreateDepartment";
                var parameters = new { dep.CategoryId, dep.DepartmentName, dep.Description,dep.Status };
                using var connection = _context.CreateConnection();
                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(CreateAsync) + " " + _controllerName);
                throw;
            }
        }
        public async Task<int> UpdateAsync(Department dep)
        {
            try
            {
                var query = "usp_UpdateDepartment";
                var parameters = new { dep.DepartmentId, dep.DepartmentName, dep.CategoryId, dep.Status,dep.Description };
                using var connection = _context.CreateConnection();
                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(UpdateAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<PagedResult<dynamic>> GetAllStates()
        {
            try
            {
                var query = "usp_GetAllStates";
                var parameters = new DynamicParameters();

                using var connection = _context.CreateConnection();

                using var multi = await connection.QueryMultipleAsync(query, parameters, commandType: CommandType.StoredProcedure);

                var totalCount = await multi.ReadFirstAsync<int>();
                var states = (await multi.ReadAsync<dynamic>()).ToList();

                return new PagedResult<dynamic>
                {
                    Items = states,
                                        TotalCount = totalCount

                };
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetAllStates) + " " + _controllerName);
                throw;
            }
        }

        public async Task<PagedResult<dynamic>> GetCitiesByStateId(int StateId)
        {
            try
            {
                var query = "usp_GetCitiesByStateId";
                var parameters = new { StateId = StateId };

                using var connection = _context.CreateConnection();

                using var multi = await connection.QueryMultipleAsync(query, parameters, commandType: CommandType.StoredProcedure);

                var totalCount = await multi.ReadFirstAsync<int>();

                var cities = (await multi.ReadAsync<dynamic>()).ToList();

                return new PagedResult<dynamic>
                {
                    Items = cities,
                    TotalCount = totalCount

                };
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetCitiesByStateId) + " " + _controllerName);
                throw;
            }
        }

    }
}
