using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Hobby")]
    public class HobbyEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string HobbyName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "int")]
        public int HobbyTypeId { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        public HobbyTypeEntity HobbyType { get; set; } = null!;

        public UserEntity User { get; set; } = null!;
    }
}
