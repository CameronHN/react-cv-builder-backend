using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PositionEntity
    {
        public int Id { get; set; }

        [Required]
        public string Role {  get; set; } = string.Empty;

        [Required]
        public string StartDate { get; set; } = string.Empty;

        public string EndDate { get; set; } = "Present";

        // Foreign key
        [Required]
        public int UserId { get; set; }

        // Navigation property
        public UserEntity User { get; set; } = null!;

    }
}
