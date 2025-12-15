using TesteTecnicoMarcus.Domain.Enums;

namespace TesteTecnicoMarcus.Application.DTOs.JobTitles
{
    public class JobTitleUpdateDto
    {
        public string Name { get; set; } = default!;
        public EDepartment Department { get; set; }
    }
}
