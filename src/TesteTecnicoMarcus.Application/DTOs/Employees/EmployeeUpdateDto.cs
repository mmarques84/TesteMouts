using TesteTecnicoMarcus.Application.DTOs.Employees;

namespace TesteTecnicoMarcus.Application.DTOs.Employees
{
    public class EmployeeUpdateDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string DocNumber { get; set; } = default!;
        public DateTime BirthDate { get; set; }

        public Guid? ManagerId { get; set; }
        public Guid? JobTitleId { get; set; }

        public EmployeeAddressDto? Address { get; set; }
        public string Phone { get; set; }
    }

}
