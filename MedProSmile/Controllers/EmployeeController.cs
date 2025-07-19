using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
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
        public async Task<IActionResult> Create(Employee emp)
        {
            await _service.CreateAsync(emp);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Employee emp)
        {
            if (id != emp.EmployeeId) return BadRequest();
            await _service.UpdateAsync(emp);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Succefully deleted record !!");
        }
    }
}
