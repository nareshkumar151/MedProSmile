using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class DischargeBillingController : ControllerBase
    {
        private readonly IDischargeBillingService _service;

        public DischargeBillingController(IDischargeBillingService service)
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
        public async Task<IActionResult> Create(DischargeBilling dischargeBilling )
        {
            await _service.CreateAsync(dischargeBilling);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, DischargeBillingUpdate dischargeBillingUpdate)
        {
            if (id != dischargeBillingUpdate.DischargeBillingId) return BadRequest();
            await _service.UpdateAsync(dischargeBillingUpdate);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DischargeBillingDelete dischargeBillingDelete)
        {
            await _service.DeleteAsync(dischargeBillingDelete);
            return Ok("Succefully deleted record !!");
        }
    }
}
