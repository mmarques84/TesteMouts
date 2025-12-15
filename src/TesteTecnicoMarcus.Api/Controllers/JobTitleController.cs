using Microsoft.AspNetCore.Mvc;
using TesteTecnicoMarcus.Application.DTOs.JobTitles;
using TesteTecnicoMarcus.Application.Services;

namespace TesteTecnicoMarcus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobTitleController : ControllerBase
    {
        private readonly JobTitleService _service;

        public JobTitleController(JobTitleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobTitleCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, JobTitleUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            if (!string.IsNullOrWhiteSpace(search))
                return Ok(await _service.SearchAsync(search));

            return Ok(await _service.GetAllAsync());
        }
    }
}
