using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("PositionResponsibility")]
    public class PositionResponsibilityEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int PositionId { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Responsibility { get; set; } = string.Empty;

        public PositionEntity Position { get; set; } = null!;

    }
}
