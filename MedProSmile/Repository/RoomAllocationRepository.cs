using Dapper;
using MedProSmile.Data;
using MedProSmile.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;

namespace MedProSmile.Repository
{
    public class RoomAllocationRepository : IRoomAllocationRepository
    {
        private readonly DapperContext _context;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly string _controllerName = "RoomAllocation";


        public RoomAllocationRepository(DapperContext context, IExceptionLogger exceptionLogger)
        {
            _context = context;
            _exceptionLogger = exceptionLogger;

        }

        public async Task<PagedResult<dynamic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = "usp_GetAllRoomAllocations";
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
                var query = "usp_GetRoomAllocationById";
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

        public async Task<int> CreateAsync(RoomAllocation roomAllocation)
        {
            try
            {
                var query = "usp_CreateRoomAllocation";
                var parameters = new {
                    roomAllocation.HospitalId,
                    roomAllocation.PatientId,
                    roomAllocation.DepartmentId,
                    roomAllocation.RoomNumber,
                    roomAllocation.BedNumber,
                    roomAllocation.AdmissionDate,
                    roomAllocation.DischargeDate,
                    roomAllocation.Status,
                    roomAllocation.CreatedBy
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

        public async Task<int> UpdateAsync(RoomAllocationUpdate roomAllocation)
        {
            try
            {
                var query = "usp_UpdateRoomAllocation";
                var parameters = new {
                    roomAllocation.RoomId,
                    roomAllocation.HospitalId,
                    roomAllocation.PatientId ,
                    roomAllocation.DepartmentId ,
                    roomAllocation.RoomNumber ,
                    roomAllocation.BedNumber  ,
                    roomAllocation.AdmissionDate ,
                    roomAllocation.DischargeDate ,
                    roomAllocation.Status ,
                    roomAllocation.UpdatedBy

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

        public async Task<int> DeleteAsync(RoomAllocationDelete roomAllocationDelete)
        {
            try
            {
                var query = "usp_DeleteRoomAllocation";
                var parameters = new { RoomId = roomAllocationDelete.RoomId, UpdatedBy = roomAllocationDelete.UpdatedBy};
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
