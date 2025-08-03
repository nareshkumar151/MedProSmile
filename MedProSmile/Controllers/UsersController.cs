using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service)
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
        public async Task<IActionResult> Create(Users users )
        {
            await _service.CreateAsync(users);
            return Ok("Succefully Created !!");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, UsersUpdate usersUpdate)
        {
            if (id != usersUpdate.UserId) return BadRequest();
            await _service.UpdateAsync(usersUpdate);
            return Ok("Succefully updated record !!");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(UsersDelete usersDelete)
        {
            await _service.DeleteAsync(usersDelete);
            return Ok("Succefully deleted record !!");
        }
    }
}
