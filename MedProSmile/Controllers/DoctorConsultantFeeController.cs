using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorConsultantFeeController : ControllerBase
    {
        private readonly IDoctorConsultantFeeService _service;

        public DoctorConsultantFeeController(IDoctorConsultantFeeService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("getByDoctorId")]
        public async Task<IActionResult> GetByDoctorId(int doctorId)
        {
            var result = await _service.GetByDoctorIdAsync(doctorId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(DoctorConsultantFeeModel model)
        {
            var consultantFeeId = await _service.CreateAsync(model);
            return Ok(new { ConsultantFeeId = consultantFeeId, Message = "Succefully Created !!" });
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("update")]
        public async Task<IActionResult> Update(int consultantFeeId, DoctorConsultantFeeModel model)
        {
            if (consultantFeeId != model.ConsultantFeeId) return BadRequest();
            var rowsAffected = await _service.UpdateAsync(model);
            if (rowsAffected == 0) return NotFound();
            return Ok("Succefully updated record !!");
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int consultantFeeId, int? updatedBy)
        {
            var rowsAffected = await _service.DeleteAsync(consultantFeeId, updatedBy);
            if (rowsAffected == 0) return NotFound();
            return Ok("Succefully deleted record !!");
        }
    }
}
