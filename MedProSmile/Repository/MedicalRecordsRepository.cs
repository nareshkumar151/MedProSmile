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
    public class MedicalRecordsRepository : IMedicalRecordsRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "MedicalRecords";


        public MedicalRecordsRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllMedicalRecords";
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
                var query = "usp_GetMedicalRecordById";
                var parameters = new { AppointmentId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(MedicalRecords medicalRecords)
        {
            try
            {
                var query = "usp_CreateMedicalRecord";
                var parameters = new {
                    medicalRecords.HospitalId,
                    medicalRecords.PatientId,
                    medicalRecords.DepartmentId,
                    medicalRecords.Diagnosis,
                    medicalRecords.Prescription,
                    medicalRecords.Status,
                    medicalRecords.CreatedBy
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

        public async Task<int> UpdateAsync(MedicalRecordUpdate medicalRecordUpdate)
        {
            try
            {
                var query = "usp_UpdateMedicalRecord";
                var parameters = new {
                    medicalRecordUpdate.RecordId,
                    medicalRecordUpdate.HospitalId,
                    medicalRecordUpdate.PatientId,
                    medicalRecordUpdate.DepartmentId,
                    medicalRecordUpdate.Diagnosis,
                    medicalRecordUpdate.Prescription,
                    medicalRecordUpdate.Status,
                    medicalRecordUpdate.UpdatedBy

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

        public async Task<int> DeleteAsync(MedicalRecordDelete medicalRecordDelete)
        {
            try
            {
                var query = "usp_DeleteMedicalRecord";
                var parameters = new { RecordId = medicalRecordDelete.RecordId, UpdatedBy = medicalRecordDelete.UpdatedBy};
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
