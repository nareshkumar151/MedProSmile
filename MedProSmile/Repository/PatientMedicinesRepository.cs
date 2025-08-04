using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Net;
using System.Numerics;
using System.Reflection;
using static Azure.Core.HttpHeader;

namespace MedProSmile.Repository 
{
    public class PatientMedicinesRepository : IPatientMedicinesRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "PatientMedicines";


        public PatientMedicinesRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllPatientMedicines";
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
                var query = "usp_GetPatientMedicineById";
                var parameters = new { MedicineId = id };
                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _exceptionLogger.LogExceptionAsync(ex, nameof(GetByIdAsync) + " " + _controllerName);
                throw;
            }
        }

        public async Task<int> CreateAsync(PatientMedicine patientMedicine)
        {
            try
            {
                var query = "usp_CreatePatientMedicine";
                var parameters = new {
                    patientMedicine.HospitalId,
                    patientMedicine.PatientId,
                    patientMedicine.DoctorId,
                    patientMedicine.MedicineName,
                    patientMedicine.Dosage,
                    patientMedicine.Frequency,
                    patientMedicine.Duration,
                    patientMedicine.Notes,
                    patientMedicine.CreatedBy
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

        public async Task<int> UpdateAsync(PatientMedicineUpdate patientMedicineUpdate)
        {
            try
            {
                var query = "usp_UpdatePatientMedicine";
                var parameters = new {
                    patientMedicineUpdate.MedicineId,
                    patientMedicineUpdate.HospitalId,
                    patientMedicineUpdate.PatientId,
                    patientMedicineUpdate.DoctorId,
                    patientMedicineUpdate.MedicineName ,
                    patientMedicineUpdate.Dosage ,
                    patientMedicineUpdate.Frequency ,
                    patientMedicineUpdate.Duration ,
                    patientMedicineUpdate.Notes ,
                    patientMedicineUpdate.UpdatedBy 

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

        public async Task<int> DeleteAsync(PatientMedicineDelete patientMedicineDelete)
        {
            try
            {
                var query = "usp_DeleteAppointment";
                var parameters = new { MedicineId = patientMedicineDelete.MedicineId, UpdatedBy = patientMedicineDelete.UpdatedBy};
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
