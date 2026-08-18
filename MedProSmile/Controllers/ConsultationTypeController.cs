using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsultationTypeController : ControllerBase
    {
        private readonly IConsultationTypeService _service;

        public ConsultationTypeController(IConsultationTypeService service)
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
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int consultationTypeId)
        {
            var result = await _service.GetByIdAsync(consultationTypeId);
            return result == null ? NotFound() : Ok(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(ConsultationTypeModel model)
        {
            await _service.CreateAsync(model);
            return Ok("Succefully Created !!");
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("update")]
        public async Task<IActionResult> Update(int consultationTypeId, ConsultationTypeModel model)
        {
            if (consultationTypeId != model.ConsultationTypeId) return BadRequest();
            await _service.UpdateAsync(model);
            return Ok("Succefully updated record !!");
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int consultationTypeId)
        {
            await _service.DeleteAsync(consultationTypeId);
            return Ok("Succefully deleted record !!");
        }
    }
}
