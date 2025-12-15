using Microsoft.EntityFrameworkCore;
using TesteTecnicoMarcus.Domain.Entities;
using TesteTecnicoMarcus.Domain.Enums;
using TesteTecnicoMarcus.Domain.Interfaces;
using TesteTecnicoMarcus.Infrastructure.Persistence;

namespace TesteTecnicoMarcus.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _context.Employees
                                 .Include(e => e.Address)
                                 .Include(e => e.JobTitle)
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                                 .Include(e => e.Address)
                                 .Include(e => e.JobTitle)
                                 .ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
        }



        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByDocNumberAsync(string docNumber)
        {
            return await _context.Employees
                .AnyAsync(e => e.DocNumber == docNumber);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Employees
                .AnyAsync(e => e.Email == email);
        }

        public async Task<List<Employee>> SearchAsync(string? search)
        {
            var query = _context.Employees
                .Include(e => e.Manager)
                .Include(e => e.JobTitle)
                       .Include(e => e.Address)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query.Where(e =>
                    e.FirstName.ToLower().Contains(search) ||
                    e.LastName.ToLower().Contains(search) ||
                    e.Email.ToLower().Contains(search) ||
                    e.DocNumber.ToLower().Contains(search) ||
                    e.JobTitle!.Name.ToLower().Contains(search)
                );
            }

            return await query.ToListAsync();
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _context.Employees
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<Employee>> GetManagersAsync()
        {
            return await _context.Employees
                .Where(e => e.Role == ERole.Leader )
                .ToListAsync();
        }

    }
}
