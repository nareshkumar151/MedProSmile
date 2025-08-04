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
    public class BillingMasterRepository : IBillingMasterRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "BillingMaster";


        public BillingMasterRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllBillingRecords";
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
                var query = "usp_GetBillingById";
                var parameters = new { BillingId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(BillingMaster billingMaster)
        {
            try
            {
                var query = "usp_CreateBilling";
                var parameters = new {
                    billingMaster.HospitalId,
                    billingMaster.PatientId,
                    billingMaster.TotalAmount,
                    billingMaster.DiscountAmount,
                    billingMaster.PaidAmount,
                    billingMaster.PaymentMode,
                    billingMaster.PaymentStatus,
                    billingMaster.CreatedBy
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

        public async Task<int> UpdateAsync(BillingMasterUpdate billingMasterUpdate)
        {
            try
            {
                var query = "usp_UpdateBilling";
                var parameters = new {
                    billingMasterUpdate.BillingId,
                    billingMasterUpdate.HospitalId,
                    billingMasterUpdate.PatientId,
                    billingMasterUpdate.TotalAmount,
                    billingMasterUpdate.DiscountAmount,
                    billingMasterUpdate.PaidAmount ,
                    billingMasterUpdate.PaymentMode ,
                    billingMasterUpdate.PaymentStatus ,
                    billingMasterUpdate.UpdatedBy 

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

        public async Task<int> DeleteAsync(BillingMasterDelete billingMasterDelete)
        {
            try
            {
                var query = "usp_DeleteBilling";
                var parameters = new { BillingId = billingMasterDelete.BillingId, UpdatedBy = billingMasterDelete.UpdatedBy};
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
