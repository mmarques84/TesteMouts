using Microsoft.EntityFrameworkCore;
using TesteTecnicoMarcus.Domain.Entities;
using TesteTecnicoMarcus.Domain.Interfaces;
using TesteTecnicoMarcus.Infrastructure.Persistence;

namespace TesteTecnicoMarcus.Infrastructure.Repositories
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly AppDbContext _context;

        public JobTitleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<JobTitle?> GetByIdAsync(Guid id)
        {
            return await _context.JobTitles
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<List<JobTitle>> GetAllAsync()
        {
            return await _context.JobTitles.ToListAsync();
        }

        public async Task<List<JobTitle>> SearchAsync(string? search)
        {
            var query = _context.JobTitles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query.Where(j =>
                    j.Name.ToLower().Contains(search) ||
                    j.Department.ToString().ToLower().Contains(search)
                );
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(JobTitle jobTitle)
        {
            await _context.JobTitles.AddAsync(jobTitle);
        }

        public async Task UpdateAsync(JobTitle jobTitle)
        {
            _context.JobTitles.Update(jobTitle);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
