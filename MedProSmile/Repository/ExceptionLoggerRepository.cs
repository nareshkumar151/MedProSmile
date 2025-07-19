using Dapper;
using MedProSmile.Data;

namespace MedProSmile.Repository
{
    public class ExceptionLoggerRepository :IExceptionLogger
    {
        private readonly DapperContext _context;

        public ExceptionLoggerRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task LogExceptionAsync(Exception ex, string methodName)
        {
            try
            {
                var query = "usp_InsertExceptionLog";
                var parameters = new DynamicParameters();
                parameters.Add("@ExceptionMessage", ex.Message);
                parameters.Add("@StackTrace", ex.StackTrace);
                parameters.Add("@Source", ex.Source ?? "");
                parameters.Add("@MethodName", methodName);

                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(query, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch
            {
                // Swallow exceptions to prevent recursive errors
            }
        }
    }
}
