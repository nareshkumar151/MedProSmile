using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using System.Data;

namespace MedProSmile.Repository
{
    public class ConsultationTypeRepository : IConsultationTypeRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "ConsultationType";

        public ConsultationTypeRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;
        }

        public async Task<IEnumerable<ConsultationTypeModel>> GetAllAsync()
        {
            try
            {
                var query = "usp_Get_ConsultationType";
                using var connection = _context.CreateConnection();
                return await connection.QueryAsync<ConsultationTypeModel>(query, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetAllAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<ConsultationTypeModel> GetByIdAsync(int consultationTypeId)
        {
            try
            {
                var query = "usp_Get_ConsultationType_By_Id";
                var parameters = new { ConsultationTypeId = consultationTypeId };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<ConsultationTypeModel>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(ConsultationTypeModel model)
        {
            try
            {
                var query = "usp_Add_ConsultationType";
                var parameters = new { model.ConsultationType };
                using var connection = _context.CreateConnection();
                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(CreateAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> UpdateAsync(ConsultationTypeModel model)
        {
            try
            {
                var query = "usp_Update_ConsultationType";
                var parameters = new { model.ConsultationTypeId, model.ConsultationType };
                using var connection = _context.CreateConnection();
                return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(UpdateAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> DeleteAsync(int consultationTypeId)
        {
            try
            {
                var query = "usp_Delete_ConsultationType";
                var parameters = new { ConsultationTypeId = consultationTypeId };
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
