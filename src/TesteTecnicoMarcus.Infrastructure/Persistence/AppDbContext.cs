using Microsoft.EntityFrameworkCore;
using TesteTecnicoMarcus.Domain.Entities;

namespace TesteTecnicoMarcus.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tabelas
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }

      
    }
}
