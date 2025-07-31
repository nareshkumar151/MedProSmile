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
    public class PatientRepository : IPatientRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "Patient";


        public PatientRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllPatients";
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
                var query = "usp_GetPatientById";
                var parameters = new { PatientId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(Patient patient)
        {
            try
            {
                var query = "usp_CreatePatient";
                var parameters = new {
                    patient.HospitalId,
                    patient.FullName,
                    patient.DOB,
                    patient.Gender,
                    patient.Phone,
                    patient.Email,
                    patient.Address,
                    patient.DepartmentId,
                    patient.Status,
                    patient.CreatedBy
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

        public async Task<int> UpdateAsync(PatientUpdateDto patientUpdateDto)
        {
            try
            {
                var query = "usp_UpdatePatient";
                var parameters = new {
                    patientUpdateDto.HospitalId,
                    patientUpdateDto.FullName ,
                    patientUpdateDto.DOB ,
                    patientUpdateDto.Gender ,
                    patientUpdateDto.Phone ,
                    patientUpdateDto.Email ,
                    patientUpdateDto.Address ,
                    patientUpdateDto.DepartmentId ,
                    patientUpdateDto.Status,
                    patientUpdateDto.UpdatedBy 

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

        public async Task<int> DeleteAsync(PatientDeleteDto patientDeleteDto)
        {
            try
            {
                var query = "usp_DeleteEmployee";
                var parameters = new { PatientId = patientDeleteDto.PatientId, UpdatedBy = patientDeleteDto.UpdatedBy};
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
