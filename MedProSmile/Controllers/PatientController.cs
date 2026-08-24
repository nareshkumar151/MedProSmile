using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Admin,Receptionist")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        private int? GetCallerHospitalId()
        {
            var claim = User.FindFirst("HospitalId")?.Value;
            return int.TryParse(claim, out var hospitalId) ? hospitalId : null;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var hospitalId = GetCallerHospitalId();
            if (hospitalId == null) return Forbid();

            var result = await _service.GetAllPagedAsync(hospitalId.Value, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var hospitalId = GetCallerHospitalId();
            if (hospitalId == null) return Forbid();

            var emp = await _service.GetByIdAsync(id, hospitalId.Value);
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Patient patient )
        {
            await _service.CreateAsync(patient);
            return Ok("Succefully Created !!");
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(int id, PatientUpdateDto patientUpdateDto)
        {
            if (id != patientUpdateDto.PatientId) return BadRequest();
            await _service.UpdateAsync(patientUpdateDto);
            return Ok("Succefully updated record !!");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(PatientDeleteDto patientDeleteDto)
        {
            await _service.DeleteAsync(patientDeleteDto);
            return Ok("Succefully deleted record !!");
        }
    }
}
