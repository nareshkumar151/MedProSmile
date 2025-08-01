using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordsService _service;

        public MedicalRecordsController(IMedicalRecordsService service)
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
        public async Task<IActionResult> Create(MedicalRecords medicalRecords )
        {
            await _service.CreateAsync(medicalRecords);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, MedicalRecordUpdate medicalRecordUpdate)
        {
            if (id != medicalRecordUpdate.RecordId) return BadRequest();
            await _service.UpdateAsync(medicalRecordUpdate);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(MedicalRecordDelete medicalRecordDelete)
        {
            await _service.DeleteAsync(medicalRecordDelete);
            return Ok("Succefully deleted record !!");
        }
    }
}
