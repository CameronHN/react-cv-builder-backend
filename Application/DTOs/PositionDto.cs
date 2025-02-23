using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    // DTO to display user positions in the following format
    public class PositionDto
    {

        // public int Id { get; set; }

        public string Role { get; set; }

        public string StartDate { get; set; }

        public string? EndDate { get; set; }
    }
}
