using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ProfessionalStatement")]
    public class ProfessionalStatementEntity
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string ProfessionalSummary { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string ProfessionalObjective { get; set; } = string.Empty;

        public UserEntity User { get; set; } = null!;
    }
}
