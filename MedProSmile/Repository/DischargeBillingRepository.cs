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
    public class DischargeBillingRepository : IDischargeBillingRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "DischargeBilling";


        public DischargeBillingRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllDischargeBillings";
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
                var query = "usp_GetDischargeBillingById";
                var parameters = new { dischargeBillingId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(DischargeBilling dischargeBilling)
        {
            try
            {
                var query = "usp_CreateDischargeBilling";
                var parameters = new {
                    dischargeBilling.HospitalId,
                    dischargeBilling.PatientId,
                    dischargeBilling.SummaryId,
                    dischargeBilling.RoomCharges,
                    dischargeBilling.ConsultationFees,
                    dischargeBilling.MedicineCharges,
                    dischargeBilling.LabCharges,
                    dischargeBilling.OtherCharges,
                    dischargeBilling.Discount,
                    dischargeBilling.PaymentStatus,
                    dischargeBilling.CreatedBy

                    
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

        public async Task<int> UpdateAsync(DischargeBillingUpdate dischargeBillingUpdate)
        {
            try
            {
                var query = "usp_UpdateDischargeBilling";
                var parameters = new {
                    dischargeBillingUpdate.DischargeBillingId,
                    dischargeBillingUpdate.HospitalId,
                    dischargeBillingUpdate.PatientId,
                    dischargeBillingUpdate.SummaryId,
                    dischargeBillingUpdate.RoomCharges,
                    dischargeBillingUpdate.ConsultationFees,
                    dischargeBillingUpdate.MedicineCharges,
                    dischargeBillingUpdate.LabCharges,
                    dischargeBillingUpdate.OtherCharges,
                    dischargeBillingUpdate.Discount,
                    dischargeBillingUpdate.PaymentStatus,
                    dischargeBillingUpdate.UpdatedBy


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

        public async Task<int> DeleteAsync(DischargeBillingDelete dischargeBillingDelete)
        {
            try
            {
                var query = "usp_DeleteDischargeBilling";
                var parameters = new { dischargeBillingId = dischargeBillingDelete.DischargeBillingId, UpdatedBy = dischargeBillingDelete.UpdatedBy};
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
