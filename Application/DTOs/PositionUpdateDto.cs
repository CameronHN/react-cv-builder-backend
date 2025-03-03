namespace Application.DTOs
{
    public class PositionUpdateDto
    {
        public int Id { get; set; }

        public string? Role { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }
        public int? UserId { get; set; }
    }
}
