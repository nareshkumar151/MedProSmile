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

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllPagedAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetByIdAsync(id);
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
                dynamic existing = await _service.GetByIdAsync(id);
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
    }
}
