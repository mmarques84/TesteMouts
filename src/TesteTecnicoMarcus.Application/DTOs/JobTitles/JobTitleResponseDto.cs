using TesteTecnicoMarcus.Application.DTOs.JobTitles;

namespace TesteTecnicoMarcus.Application.DTOs.JobTitles
{
    public class JobTitleResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        public string Department { get; set; } = default!;

        public bool IsActive { get; set; }
    }
}
