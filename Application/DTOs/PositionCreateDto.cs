namespace Application.DTOs
{
    public class PositionCreateDto
    {
        public string Role { get; set; }
        public string StartDate { get; set; }

        // Default value is "Present"
        public string? EndDate { get; set; } = "Present";
        public int UserId { get; set; }

    }
}
