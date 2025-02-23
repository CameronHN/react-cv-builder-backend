using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PositionResponsibilityEntity
    {
        public int Id { get; set; }

        [Required]
        public int PositionId { get; set; }

        public string Responsibility { get; set; } = string.Empty;

    }
}
