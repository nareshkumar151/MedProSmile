using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class BillingMasterController : ControllerBase
    {
        private readonly IBillingMasterService _service;

        public BillingMasterController(IBillingMasterService service)
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
        public async Task<IActionResult> Create(BillingMaster billingMaster )
        {
            await _service.CreateAsync(billingMaster);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, BillingMasterUpdate billingMasterUpdate)
        {
            if (id != billingMasterUpdate.BillingId) return BadRequest();
            await _service.UpdateAsync(billingMasterUpdate);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(BillingMasterDelete billingMasterDelete)
        {
            await _service.DeleteAsync(billingMasterDelete);
            return Ok("Succefully deleted record !!");
        }
    }
}
