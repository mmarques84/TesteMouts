namespace TesteTecnicoMarcus.Application.DTOs.JobTitles
{
    public class JobTitleListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Department { get; set; } = default!;
    }
}
