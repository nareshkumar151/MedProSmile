using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _service;

        public AppointmentsController(IAppointmentsService service)
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
        public async Task<IActionResult> Create(Appointment appointment )
        {
            await _service.CreateAsync(appointment);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, AppointmentUpdate appointmentUpdate)
        {
            if (id != appointmentUpdate.AppointmentId) return BadRequest();
            await _service.UpdateAsync(appointmentUpdate);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(AppointmentDelete appointmentDelete)
        {
            await _service.DeleteAsync(appointmentDelete);
            return Ok("Succefully deleted record !!");
        }
    }
}
