using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using System.Data;

namespace MedProSmile.Repository
{
    public class DoctorConsultantFeeRepository : IDoctorConsultantFeeRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "DoctorConsultantFee";

        public DoctorConsultantFeeRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;
        }

        public async Task<int> CreateAsync(DoctorConsultantFeeModel model)
        {
            try
            {
                var query = "usp_AddDoctorConsultantFee";
                var parameters = new
                {
                    model.HospitalId,
                    model.DoctorId,
                    model.ConsultationTypeId,
                    model.FeeAmount,
                    model.CreatedBy
                };
                using var connection = _context.CreateConnection();
                return await connection.QuerySingleAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(CreateAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> UpdateAsync(DoctorConsultantFeeModel model)
        {
            try
            {
                var query = "usp_UpdateDoctorConsultantFee";
                var parameters = new
                {
                    model.ConsultantFeeId,
                    model.HospitalId,
                    model.DoctorId,
                    model.ConsultationTypeId,
                    model.FeeAmount,
                    model.IsActive,
                    model.UpdatedBy
                };
                using var connection = _context.CreateConnection();
                return await connection.QuerySingleAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(UpdateAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> DeleteAsync(int consultantFeeId, int? updatedBy)
        {
            try
            {
                var query = "usp_DeleteDoctorConsultantFee";
                var parameters = new { ConsultantFeeId = consultantFeeId, UpdatedBy = updatedBy };
                using var connection = _context.CreateConnection();
                return await connection.QuerySingleAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(DeleteAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<IEnumerable<DoctorConsultantFeeModel>> GetByDoctorIdAsync(int doctorId)
        {
            try
            {
                var query = "usp_GetDoctorConsultantFeeByDoctorId";
                var parameters = new { DoctorId = doctorId };
                using var connection = _context.CreateConnection();
                return await connection.QueryAsync<DoctorConsultantFeeModel>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByDoctorIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<IEnumerable<DoctorConsultantFeeModel>> GetAllAsync()
        {
            try
            {
                var query = "usp_GetAllDoctorConsultantFee";
                using var connection = _context.CreateConnection();
                return await connection.QueryAsync<DoctorConsultantFeeModel>(query, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetAllAsync) + " " + _controllerName);
                throw;
            }
        }
    }
}
