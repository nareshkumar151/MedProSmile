using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class DischargeSummaryController : ControllerBase
    {
        private readonly IDischargeSummaryService _service;

        public DischargeSummaryController(IDischargeSummaryService service)
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
        public async Task<IActionResult> Create(DischargeSummary dischargeSummary )
        {
            await _service.CreateAsync(dischargeSummary);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, DischargeSummaryUpdate dischargeSummaryUpdate)
        {
            if (id != dischargeSummaryUpdate.SummaryId) return BadRequest();
            await _service.UpdateAsync(dischargeSummaryUpdate);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DischargeSummaryDelete dischargeSummaryDelete)
        {
            await _service.DeleteAsync(dischargeSummaryDelete);
            return Ok("Succefully deleted record !!");
        }
    }
}
