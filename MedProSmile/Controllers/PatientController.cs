using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllPagedAsync(pageNumber, pageSize);
            return Ok(result); 
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetByIdAsync(id);
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Patient patient )
        {
            await _service.CreateAsync(patient);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, PatientUpdateDto patientUpdateDto)
        {
            if (id != patientUpdateDto.PatientId) return BadRequest();
            await _service.UpdateAsync(patientUpdateDto);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(PatientDeleteDto patientDeleteDto)
        {
            await _service.DeleteAsync(patientDeleteDto);
            return Ok("Succefully deleted record !!");
        }
    }
}
