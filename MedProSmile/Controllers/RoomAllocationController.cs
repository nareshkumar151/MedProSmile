using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class RoomAllocationController : ControllerBase
    {
        private readonly IRoomAllocationService _service;

        public RoomAllocationController(IRoomAllocationService service)
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
        public async Task<IActionResult> Create(RoomAllocation roomAllocation )
        {
            await _service.CreateAsync(roomAllocation);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, RoomAllocationUpdate roomAllocationUpdate)
        {
            if (id != roomAllocationUpdate.RoomId) return BadRequest();
            await _service.UpdateAsync(roomAllocationUpdate);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(RoomAllocationDelete roomAllocationDelete)
        {
            await _service.DeleteAsync(roomAllocationDelete);
            return Ok("Succefully deleted record !!");
        }
    }
}
