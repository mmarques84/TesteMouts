using TesteTecnicoMarcus.Application.DTOs.JobTitles;
using TesteTecnicoMarcus.Application.Services.Interfaces;
using TesteTecnicoMarcus.Domain.Entities;
using TesteTecnicoMarcus.Domain.Interfaces;

namespace TesteTecnicoMarcus.Application.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository _repository;

        public JobTitleService(IJobTitleRepository repository)
        {
            _repository = repository;
        }

        public async Task<JobTitleResponseDto> CreateAsync(JobTitleCreateDto dto)
        {
            var job = new JobTitle
            {
                Name = dto.Name,
                Department = dto.Department,
                IsActive = true
            };

            await _repository.AddAsync(job);
            await _repository.SaveChangesAsync();

            return ToMapper(job);
        }

        public async Task<JobTitleResponseDto> UpdateAsync(Guid id, JobTitleUpdateDto dto)
        {
            var job = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Job Title not found.");

            job.Name = dto.Name;
            job.Department = dto.Department;

            await _repository.UpdateAsync(job);
            await _repository.SaveChangesAsync();

            return ToMapper(job);
        }

        public async Task<JobTitleResponseDto?> GetByIdAsync(Guid id)
        {
            var job = await _repository.GetByIdAsync(id);
            return job is null ? null : ToMapper(job);
        }

        public async Task<List<JobTitleResponseDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(ToMapper).ToList();
        }

        public async Task<List<JobTitleResponseDto>> SearchAsync(string? search)
        {
            var list = await _repository.SearchAsync(search);
            return list.Select(ToMapper).ToList();
        }

        private JobTitleResponseDto ToMapper(JobTitle job)
        {
            return new JobTitleResponseDto
            {
                Id = job.Id,
                Name = job.Name,
                Department = job.Department.ToString(),
                IsActive = job.IsActive
            };
        }
    }
}
