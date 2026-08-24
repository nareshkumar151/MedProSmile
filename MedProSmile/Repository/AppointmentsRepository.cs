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
        

             public async Task<PagedResult<dynamic>> GetAllAppointmentByDoctorId(int doctorId,int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllAppointmentsByDoctorId";
                var parameters = new DynamicParameters();
                parameters.Add("DoctorId", doctorId);
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
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetAllPagedAsync) + " " + _controllerName);
                throw;
            }
        }
        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int hospitalId, int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllAppointments";
                var parameters = new DynamicParameters();
                parameters.Add("HospitalId", hospitalId);
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

        public async Task<dynamic> GetByIdAsync(int id, int hospitalId)
        {
            try
            {
                var query = "usp_GetAppointmentById";
                var parameters = new { AppointmentId = id, HospitalId = hospitalId };
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
                    appointment.ConsultationTypeId,
                    appointment.ConsultationFee,
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
                    appointmentUpdate.ConsultationTypeId,
                    appointmentUpdate.ConsultationFee,
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

        public async Task<decimal?> GetConsultationFeeByDoctorAndConsultationTypeAsync(int doctorId, int consultationTypeId, int hospitalId)
        {
            try
            {
                var query = "usp_GetConsultationFeeByDoctorandConsultation";
                var parameters = new { DoctorID = doctorId, ConsultationTypeID = consultationTypeId, HospitalId = hospitalId };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<decimal?>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetConsultationFeeByDoctorAndConsultationTypeAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<IEnumerable<dynamic>> GetDoctorRevenueAsync(int? doctorId)
        {
            try
            {
                var query = "usp_GetDoctorRevenue";
                var parameters = new { DoctorId = doctorId };
                using var connection = _context.CreateConnection();
                return await connection.QueryAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetDoctorRevenueAsync) + " " + _controllerName);
                throw;
            }
        }
    }
}
