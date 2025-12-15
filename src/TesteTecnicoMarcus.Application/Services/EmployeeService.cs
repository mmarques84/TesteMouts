using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TesteTecnicoMarcus.Application.DTOs.Employees;
using TesteTecnicoMarcus.Application.Services.Interfaces;
using TesteTecnicoMarcus.Domain.Entities;
using TesteTecnicoMarcus.Domain.Enums;
using TesteTecnicoMarcus.Domain.Interfaces;

namespace TesteTecnicoMarcus.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IJobTitleRepository _jobTitleRepository;

        public EmployeeService(IEmployeeRepository repository, IJobTitleRepository jobTitleRepository)
        {
            _repository = repository;
            _jobTitleRepository = jobTitleRepository;
        }

        public async Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto dto)
        {
            await ValidateCreateAsync(dto);



     
            if (dto.Role == ERole.Admin && dto.RoleToken != ERole.Admin)
            {
                throw new Exception("Apenas um administrador pode criar outro administrador.");
            }

            if (dto.RoleToken != ERole.Admin && dto.Role > dto.RoleToken)
            {
                throw new Exception("Você não tem permissão para criar um funcionário com nível superior ao seu.");
            }


            JobTitle? jobTitle = null;
            if (dto.JobTitleId.HasValue)
                jobTitle = await _jobTitleRepository.GetByIdAsync(dto.JobTitleId.Value)
                    ?? throw new Exception("Cargo (Job Title) não encontrado.");

            Employee? manager = null;
            if (dto.ManagerId.HasValue)
                manager = await _repository.GetByIdAsync(dto.ManagerId.Value)
                    ?? throw new Exception("Gestor informado não foi encontrado.");

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DocNumber = dto.DocNumber,
                BirthDate = dto.BirthDate,
                Manager = manager,
                JobTitle = jobTitle,
                HireDate = DateTime.UtcNow,
                Role = dto.Role,
                Phone = dto.Phone
            };

   
            var hasher = new PasswordHasher<Employee>();
            employee.PasswordHash = hasher.HashPassword(employee, dto.Password);

            // Endereço
            if (dto.Address is not null)
            {
                employee.Address = new EmployeeAddress
                {
                    Street = dto.Address.Street,
                    Number = dto.Address.Number,
                    City = dto.Address.City,
                    State = dto.Address.State,
                    ZipCode = dto.Address.ZipCode
                };
            }

        
            await _repository.AddAsync(employee);
            await _repository.SaveChangesAsync();

            return ToResponse(employee);
        }


        public async Task<EmployeeResponseDto> UpdateAsync(Guid id, EmployeeUpdateDto dto)
        {
            var employee = await _repository.GetByIdAsync(id)
                ?? throw new Exception("Employee not found.");

            await ValidateUpdateAsync(dto, id);

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.BirthDate = dto.BirthDate;
            employee.UpdatedAt = DateTime.UtcNow;
            employee.Phone = dto.Phone;

            if (dto.JobTitleId.HasValue)
                employee.JobTitle = await _jobTitleRepository.GetByIdAsync(dto.JobTitleId.Value)
                    ?? throw new Exception("Job Title not found.");

            if (dto.ManagerId.HasValue)
                employee.Manager = await _repository.GetByIdAsync(dto.ManagerId.Value)
                    ?? throw new Exception("Manager not found.");

            if (dto.Address != null)
            {
                if (employee.Address == null)
                    employee.Address = new EmployeeAddress();

                employee.Address.Street = dto.Address.Street;
                employee.Address.Number = dto.Address.Number;
                employee.Address.City = dto.Address.City;
                employee.Address.State = dto.Address.State;
                employee.Address.ZipCode = dto.Address.ZipCode;
            }

          

            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();

            return ToResponse(employee);
        }

        public async Task<EmployeeResponseDto?> GetByIdAsync(Guid id)
        {
            var employee = await _repository.GetByIdAsync(id);
            return employee is null ? null : ToResponse(employee);
        }

        public async Task<List<EmployeeResponseDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(ToResponse).ToList();
        }

        public async Task<List<EmployeeResponseDto>> SearchAsync(string? search)
        {
            var list = await _repository.SearchAsync(search);
            return list.Select(ToResponse).ToList();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee is null)
                return false;

            employee.IsActive = false;
            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();

            return true;
        }

        private async Task ValidateCreateAsync(EmployeeCreateDto dto)
        {
            if (dto.BirthDate > DateTime.UtcNow.AddYears(-18))
                throw new Exception("O funcionário deve ter pelo menos 18 anos.");

            if (await _repository.ExistsByDocNumberAsync(dto.DocNumber))
                throw new Exception("O número de documento informado já está cadastrado.");
            if (await _repository.ExistsByEmailAsync(dto.Email))
                throw new Exception("O e-mail informado já está cadastrado.");
          
         
        }


        private async Task ValidateUpdateAsync(EmployeeUpdateDto dto, Guid id)
        {
            if (dto.BirthDate > DateTime.UtcNow.AddYears(-18))

                throw new Exception("O funcionário deve ter pelo menos 18 anos.");
            var employees = await _repository.GetAllAsync();
            if (employees.Any(x => x.DocNumber == dto.DocNumber && x.Id != id))
                throw new Exception("O número de documento informado já está sendo utilizado por outro funcionário.");
            if (employees.Any(x => x.Email == dto.Email && x.Id != id))
                throw new Exception("O e-mail informado já está sendo utilizado por outro funcionário.");
        }


        private EmployeeResponseDto ToResponse(Employee e)
        {
            return new EmployeeResponseDto
            {
                Id = e.Id,
                FullName = $"{e.FirstName} {e.LastName}",
                Email = e.Email,
                DocNumber = e.DocNumber,
                BirthDate = e.BirthDate,
                IsActive = e.IsActive,
                ManagerName = e.Manager != null ? $"{e.Manager.FirstName} {e.Manager.LastName}" : null,
                JobTitle = e.JobTitle?.Name,
                Department = e.JobTitle?.Department.ToString(),
                Address = e.Address != null ? new EmployeeAddressDto
                {
                    Street = e.Address.Street,
                    Number = e.Address.Number,
                    City = e.Address.City,
                    State = e.Address.State,
                    ZipCode = e.Address.ZipCode
                } : null,
                Phone = e.Phone
            };
        }

        public async  Task<List<Employee>> GetManagersAsync()
        {
            return await _repository.GetManagersAsync();
        }
    }
}
