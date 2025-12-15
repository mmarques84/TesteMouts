using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoMarcus.Domain.Entities;

namespace TesteTecnicoMarcus.Domain.Interfaces
{
    public interface IJobTitleRepository
    {
        Task<JobTitle?> GetByIdAsync(Guid id);
        Task<List<JobTitle>> GetAllAsync();
        Task AddAsync(JobTitle jobTitle);
        Task UpdateAsync(JobTitle jobTitle);
        Task<List<JobTitle>> SearchAsync(string? search);
        Task SaveChangesAsync();
    }

}
