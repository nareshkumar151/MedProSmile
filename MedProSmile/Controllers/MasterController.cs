using MedProSmile.Models;
using MedProSmile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedProSmile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize (Roles ="Admin")]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _service;

        public MasterController(IMasterService service)
        {
            _service = service;
        }

        [HttpGet("getAllDepartmentCategories")]
        public async Task<IActionResult> GetAllDepartmentCategories()
        {
            var result = await _service.GetAllDepartmentCategories();
            return Ok(result);
        }
        [HttpGet("getDepartmentCategoryById")]
        public async Task<IActionResult> GetDepartmentCategoryById(int id)
        {
            var emp = await _service.GetDepartmentCategoryById(id);
            return emp == null ? NotFound() : Ok(emp);
        }
        [HttpGet("getAllDepartment")]
        public async Task<IActionResult> GetAllPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllPagedAsync(pageNumber, pageSize);
            return Ok(result); 
        }

        [HttpGet("getDepartmentById")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetByIdAsync(id);
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpPost("createDepartment")]
        public async Task<IActionResult> Create(Department dep)
        {
            await _service.CreateAsync(dep);
            return Ok("Succefully Created !!");
        }

        [HttpPut("updateDepartment")]
        public async Task<IActionResult> Update(int id, Department dep)
        {
            if (id != dep.DepartmentId) return BadRequest();
            await _service.UpdateAsync(dep);
            return Ok("Succefully updated record !!");
        }

        [HttpGet("getAllStates")]
        public async Task<IActionResult> GetAllStates()
        {
            var result = await _service.GetAllStates();
            return Ok(result);
        }


        [HttpGet("getCitiesByStateId")]
        public async Task<IActionResult> GetCitiesByStateId(int StateId)
        {
            var result = await _service.GetCitiesByStateId(StateId);
            return Ok(result);
        }

    }
}
