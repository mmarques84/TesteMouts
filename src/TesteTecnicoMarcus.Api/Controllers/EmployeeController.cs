using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TesteTecnicoMarcus.Application.DTOs.Employees;
using TesteTecnicoMarcus.Application.Services;
using TesteTecnicoMarcus.Domain.Enums;

namespace TesteTecnicoMarcus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateDto dto)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var roleEnum = Enum.Parse<ERole>(role);

                dto.RoleToken = roleEnum;

                var result = await _service.CreateAsync(dto);

                return Ok( new
                {
                    success = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, EmployeeUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

 
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                var filtered = await _service.SearchAsync(search);
                return Ok(filtered);
            }

            var all = await _service.GetAllAsync();
            return Ok(all);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        [HttpGet("managers")]
        [AllowAnonymous]
        public async Task<IActionResult> GetManagers()
        {
            var managers = await _service.GetManagersAsync();
            return Ok(managers);
        }
    }
}
