namespace Application.DTOs
{
    public class PositionAndResponsibilitiesDto
    {
        public string Role { get; set; }
        public string StartDate { get; set; }
        public string? EndDate { get; set; }
        public List<string>? PositionResponsibilities { get; set; } = new();
    }
}
