using TesteTecnicoMarcus.Application.DTOs.JobTitles;

namespace TesteTecnicoMarcus.Application.Services.Interfaces
{
    public interface IJobTitleService
    {
        Task<JobTitleResponseDto> CreateAsync(JobTitleCreateDto dto);
        Task<JobTitleResponseDto> UpdateAsync(Guid id, JobTitleUpdateDto dto);
        Task<JobTitleResponseDto?> GetByIdAsync(Guid id);
        Task<List<JobTitleResponseDto>> GetAllAsync();
        Task<List<JobTitleResponseDto>> SearchAsync(string? search);
    }
}
