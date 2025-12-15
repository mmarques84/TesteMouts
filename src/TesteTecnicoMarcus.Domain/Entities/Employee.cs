using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoMarcus.Domain.Enums;

namespace TesteTecnicoMarcus.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string DocNumber { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public Guid? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public string Phone {get; set; }
        public EmployeeAddress? Address { get; set; }
        public Guid? JobTitleId { get; set; }
        public JobTitle? JobTitle { get; set; } 
        public DateTime? HireDate { get; set; }
        public bool IsActive { get; set; } = true;
        public ERole Role { get; set; } = ERole.Employee;
        public string PasswordHash { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

}
