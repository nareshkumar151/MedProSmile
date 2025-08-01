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
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "Appointments";


        public AppointmentsRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllAppointments";
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
                var query = "usp_GetAppointmentById";
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

        public async Task<int> CreateAsync(Appointment appointment)
        {
            try
            {
                var query = "usp_CreateAppointment";
                var parameters = new {
                    appointment.HospitalId,
                    appointment.PatientId,
                    appointment.DoctorId,
                    appointment.AppointmentDate,
                    appointment.AppointmentTime,
                    appointment.Reason,
                    appointment.AppointmentStatus,
                    appointment.Status,
                    appointment.CreatedBy
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

        public async Task<int> UpdateAsync(AppointmentUpdate appointmentUpdate)
        {
            try
            {
                var query = "usp_UpdateAppointment";
                var parameters = new {
                    appointmentUpdate.AppointmentId,
                    appointmentUpdate.HospitalId,
                    appointmentUpdate.PatientId ,
                    appointmentUpdate.DoctorId,
                    appointmentUpdate.AppointmentDate,
                    appointmentUpdate.AppointmentTime,
                    appointmentUpdate.Reason,
                    appointmentUpdate.AppointmentStatus,
                    appointmentUpdate.Status,
                    appointmentUpdate.UpdatedBy

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

        public async Task<int> DeleteAsync(AppointmentDelete appointmentDelete)
        {
            try
            {
                var query = "usp_DeleteAppointment";
                var parameters = new { AppointmentId = appointmentDelete.AppointmentId, UpdatedBy = appointmentDelete.UpdatedBy};
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
