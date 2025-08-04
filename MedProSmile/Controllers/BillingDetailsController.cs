using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class BillingDetailsController : ControllerBase
    {
        private readonly IBillingDetailsService _service;

        public BillingDetailsController(IBillingDetailsService service)
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
        public async Task<IActionResult> Create(BillingDetails billingDetails )
        {
            await _service.CreateAsync(billingDetails);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, BillingDetailsUpdate billingDetailsUpdate)
        {
            if (id != billingDetailsUpdate.BillingDetailId) return BadRequest();
            await _service.UpdateAsync(billingDetailsUpdate);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(BillingDetailsDelete billingDetailsDelete)
        {
            await _service.DeleteAsync(billingDetailsDelete);
            return Ok("Succefully deleted record !!");
        }
    }
}
