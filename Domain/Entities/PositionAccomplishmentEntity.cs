using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("PositionAccomplishment")]
    public class PositionAccomplishmentEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int PositionId { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Accomplishment { get; set; } = string.Empty;

        public PositionEntity Position { get; set; } = null!;
    }
}
