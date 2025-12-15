using TesteTecnicoMarcus.Domain.Entities;

namespace TesteTecnicoMarcus.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(Guid id);
        Task<List<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> ExistsByDocNumberAsync(string docNumber);
        Task<bool> ExistsByEmailAsync(string email);
        Task<Employee?> GetByEmailAsync(string email);
        Task SaveChangesAsync();
        Task<List<Employee>> SearchAsync(string? search);
        Task<List<Employee>> GetManagersAsync();
    }
}
