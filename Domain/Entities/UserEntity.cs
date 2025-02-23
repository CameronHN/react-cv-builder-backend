using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = string.Empty;

        // Navigation property
        // A user can have many positions
        public ICollection<PositionEntity> Positions { get; set; } = new List<PositionEntity>();
    }
}
