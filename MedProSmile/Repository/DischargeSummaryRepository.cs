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
    public class DischargeSummaryRepository : IDischargeSummaryRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "DischargeSummary";


        public DischargeSummaryRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllDischargeSummaries";
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
                var query = "usp_GetDischargeSummaryById";
                var parameters = new { SummaryId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(DischargeSummary dischargeSummary)
        {
            try
            {
                var query = "usp_CreateAppointment";
                var parameters = new {
                    dischargeSummary.HospitalId,
                    dischargeSummary.PatientId,
                    dischargeSummary.DoctorId,
                    dischargeSummary.DepartmentId,
                    dischargeSummary.AdmissionDate,
                    dischargeSummary.DischargeDate,
                    dischargeSummary.Diagnosis,
                    dischargeSummary.TreatmentGiven,
                    dischargeSummary.FollowUpAdvice,                 
                    dischargeSummary.CreatedBy
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

        public async Task<int> UpdateAsync(DischargeSummaryUpdate dischargeSummaryUpdate)
        {
            try
            {
                var query = "usp_UpdateAppointment";
                var parameters = new {
                    dischargeSummaryUpdate.SummaryId,
                    dischargeSummaryUpdate.HospitalId,
                    dischargeSummaryUpdate.PatientId,
                    dischargeSummaryUpdate.DoctorId ,
                    dischargeSummaryUpdate.DepartmentId,
                    dischargeSummaryUpdate.AdmissionDate ,
                    dischargeSummaryUpdate.DischargeDate ,
                    dischargeSummaryUpdate.Diagnosis ,
                    dischargeSummaryUpdate.TreatmentGiven ,
                    dischargeSummaryUpdate.FollowUpAdvice ,
                    dischargeSummaryUpdate.UpdatedBy 

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

        public async Task<int> DeleteAsync(DischargeSummaryDelete dischargeSummaryDelete)
        {
            try
            {
                var query = "usp_DeleteDischargeSummary";
                var parameters = new { SummaryId = dischargeSummaryDelete.SummaryId, UpdatedBy = dischargeSummaryDelete.UpdatedBy};
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
