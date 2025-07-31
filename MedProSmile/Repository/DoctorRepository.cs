using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Numerics;
using System.Reflection;

namespace MedProSmile.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "Employee";


        public DoctorRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllDoctors";
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
                var query = "usp_GetDoctorById";
                var parameters = new { DoctorId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(Doctor doc)
        {
            try
            {
                var query = "usp_CreateDoctor";
                var parameters = new {
                    doc.FullName,
                    doc.HospitalId,
                    doc.DepartmentId,
                    doc.Qualification,
                    doc.Specialization,
                    doc.Phone,
                    doc.Email,
                    doc.Gender,
                    doc.ProfilePhotoUrl,
                    doc.AvailableFrom,
                    doc.AvailableTo,
                    doc.Status,
                    doc.CreatedBy
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

        public async Task<int> UpdateAsync(DoctorUpdateDto doc)
        {
            try
            {
                var query = "usp_UpdateDoctor";
                var parameters = new {
                    doc.FullName,
                    doc.HospitalId ,
                    doc.DepartmentId ,
                    doc.Qualification,
                    doc.Specialization,
                    doc.Phone,
                    doc.Email,
                    doc.Gender,
                    doc.ProfilePhotoUrl,
                    doc.AvailableFrom,
                    doc.AvailableTo,
                    doc.Status,
                    doc.UpdatedBy,
                    doc.DoctorId
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

        public async Task<int> DeleteAsync(DoctorDeleteDto doctorDeleteDto)
        {
            try
            {
                var query = "usp_DeleteEmployee";
                var parameters = new { DoctoId = doctorDeleteDto.DoctorId, UpdatedBy=doctorDeleteDto.UpdatedBy};
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
