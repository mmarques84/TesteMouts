using TesteTecnicoMarcus.Application.DTOs.Employees;
using TesteTecnicoMarcus.Domain.Entities;

namespace TesteTecnicoMarcus.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto dto);
        Task<EmployeeResponseDto> UpdateAsync(Guid id, EmployeeUpdateDto dto);
        Task<EmployeeResponseDto?> GetByIdAsync(Guid id);
        Task<List<EmployeeResponseDto>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id); // inativar
        Task<List<EmployeeResponseDto>> SearchAsync(string? search);
        Task<List<Employee>> GetManagersAsync();
    }
}
