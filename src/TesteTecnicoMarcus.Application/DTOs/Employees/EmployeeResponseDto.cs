namespace TesteTecnicoMarcus.Application.DTOs.Employees
{
    public class EmployeeResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string DocNumber { get; set; } = default!;
        public DateTime BirthDate { get; set; }

        public string? JobTitle { get; set; }
        public string? Department { get; set; }

        public string? ManagerName { get; set; }

        public bool IsActive { get; set; }
        public string Role { get; set; } = default!;

        public EmployeeAddressDto? Address { get; set; }

        public string Phone { get; set; }
    }

}
