using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    [Authorize (Roles ="Admin,Doctor")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordsService _service;

        public MedicalRecordsController(IMedicalRecordsService service)
        {
            _service = service;
        }

        private int? GetCallerDoctorId()
        {
            var claim = User.FindFirst("DoctorId")?.Value;
            return int.TryParse(claim, out var doctorId) ? doctorId : null;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPaged([FromQuery] int? doctorId,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (User.IsInRole("Doctor") && !User.IsInRole("Admin"))
            {
                var callerDoctorId = GetCallerDoctorId();
                if (callerDoctorId == null) return Forbid();
                doctorId = callerDoctorId.Value;
            }

            var result = await _service.GetAllPagedAsync(doctorId,pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetByIdAsync(id);
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(MedicalRecords medicalRecords )
        {
            if (User.IsInRole("Doctor") && !User.IsInRole("Admin"))
            {
                var callerDoctorId = GetCallerDoctorId();
                if (callerDoctorId == null) return Forbid();
                medicalRecords.DoctorId = callerDoctorId.Value;
            }

            await _service.CreateAsync(medicalRecords);
            return Ok("Succefully Created !!");
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(int id, MedicalRecordUpdate medicalRecordUpdate)
        {
            if (id != medicalRecordUpdate.RecordId) return BadRequest();

            if (User.IsInRole("Doctor") && !User.IsInRole("Admin"))
            {
                var callerDoctorId = GetCallerDoctorId();
                dynamic existing = await _service.GetByIdAsync(id);
                if (existing == null) return NotFound();
                if (callerDoctorId == null || (int)existing.DoctorId != callerDoctorId.Value)
                    return Forbid();
            }

            await _service.UpdateAsync(medicalRecordUpdate);
            return Ok("Succefully updated record !!");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(MedicalRecordDelete medicalRecordDelete)
        {
            if (User.IsInRole("Doctor") && !User.IsInRole("Admin"))
            {
                var callerDoctorId = GetCallerDoctorId();
                dynamic existing = await _service.GetByIdAsync(medicalRecordDelete.RecordId);
                if (existing == null) return NotFound();
                if (callerDoctorId == null || (int)existing.DoctorId != callerDoctorId.Value)
                    return Forbid();
            }

            await _service.DeleteAsync(medicalRecordDelete);
            return Ok("Succefully deleted record !!");
        }
    }
}
