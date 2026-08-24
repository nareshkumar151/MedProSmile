using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private static readonly HashSet<string> AllowedStatuses = new(StringComparer.Ordinal)
        {
            "Scheduled", "Confirmed", "In Progress", "Completed", "Cancelled"
        };

        private readonly IAppointmentsService _service;

        public AppointmentsController(IAppointmentsService service)
        {
            _service = service;
        }

        private int? GetCallerDoctorId()
        {
            var claim = User.FindFirst("DoctorId")?.Value;
            return int.TryParse(claim, out var doctorId) ? doctorId : null;
        }

        private int? GetCallerHospitalId()
        {
            var claim = User.FindFirst("HospitalId")?.Value;
            return int.TryParse(claim, out var hospitalId) ? hospitalId : null;
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var hospitalId = GetCallerHospitalId();
            if (hospitalId == null) return Forbid();

            var result = await _service.GetAllPagedAsync(hospitalId.Value, pageNumber, pageSize);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var hospitalId = GetCallerHospitalId();
            if (hospitalId == null) return Forbid();

            var emp = await _service.GetByIdAsync(id, hospitalId.Value);
            return emp == null ? NotFound() : Ok(emp);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(Appointment appointment )
        {
            if (!AllowedStatuses.Contains(appointment.AppointmentStatus))
                return BadRequest("AppointmentStatus must be one of: Scheduled, Confirmed, In Progress, Completed, Cancelled.");

            await _service.CreateAsync(appointment);
            return Ok("Succefully Created !!");
        }

        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        [HttpPost("update")]
        public async Task<IActionResult> Update(int id, AppointmentUpdate appointmentUpdate)
        {
            if (id != appointmentUpdate.AppointmentId) return BadRequest();

            if (!AllowedStatuses.Contains(appointmentUpdate.AppointmentStatus))
                return BadRequest("AppointmentStatus must be one of: Scheduled, Confirmed, In Progress, Completed, Cancelled.");

            if (User.IsInRole("Doctor") && !User.IsInRole("Admin"))
            {
                var callerDoctorId = GetCallerDoctorId();
                var callerHospitalId = GetCallerHospitalId();
                if (callerHospitalId == null) return Forbid();

                dynamic existing = await _service.GetByIdAsync(id, callerHospitalId.Value);
                if (existing == null) return NotFound();
                if (callerDoctorId == null || (int)existing.DoctorId != callerDoctorId.Value)
                    return Forbid();
            }

            await _service.UpdateAsync(appointmentUpdate);
            return Ok("Succefully updated record !!");
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(AppointmentDelete appointmentDelete)
        {
            await _service.DeleteAsync(appointmentDelete);
            return Ok("Succefully deleted record !!");
        }

        [Authorize(Roles = "Doctor,Admin")]
        [HttpGet("getAllAppointmentByDoctorId")]
        public async Task<IActionResult> GetAllAppointmentByDoctorId([FromQuery] int doctorId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (User.IsInRole("Doctor") && !User.IsInRole("Admin"))
            {
                var callerDoctorId = GetCallerDoctorId();
                if (callerDoctorId == null) return Forbid();
                doctorId = callerDoctorId.Value;
            }

            var result = await _service.GetAllAppointmentByDoctorId(doctorId,pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("getConsultationFee")]
        public async Task<IActionResult> GetConsultationFeeByDoctorAndConsultationType([FromQuery] int doctorId, [FromQuery] int consultationTypeId)
        {
            var hospitalId = GetCallerHospitalId();
            if (hospitalId == null) return Forbid();

            var feeAmount = await _service.GetConsultationFeeByDoctorAndConsultationTypeAsync(doctorId, consultationTypeId, hospitalId.Value);
            if (feeAmount == null) return NotFound();
            return Ok(new { FeeAmount = feeAmount });
        }

        [Authorize(Roles = "Doctor,Admin")]
        [HttpGet("getDoctorRevenue")]
        public async Task<IActionResult> GetDoctorRevenue([FromQuery] int? doctorId)
        {
            if (User.IsInRole("Doctor") && !User.IsInRole("Admin"))
            {
                var callerDoctorId = GetCallerDoctorId();
                if (callerDoctorId == null) return Forbid();
                doctorId = callerDoctorId.Value;
            }

            var result = await _service.GetDoctorRevenueAsync(doctorId);
            return Ok(result);
        }
    }
}
