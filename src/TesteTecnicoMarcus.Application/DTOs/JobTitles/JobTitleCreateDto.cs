using TesteTecnicoMarcus.Domain.Enums;

namespace TesteTecnicoMarcus.Application.DTOs.JobTitles
{
    public class JobTitleCreateDto
    {
        public string Name { get; set; } = default!;
        public EDepartment Department { get; set; }
    }
}
