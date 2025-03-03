using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("HobbyType")]
    public class HobbyTypeEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Required]
        public string HobbyTypeName { get; set; } = string.Empty;

        public ICollection<HobbyEntity> Hobbies { get; set; } = new List<HobbyEntity>();
    }
}
