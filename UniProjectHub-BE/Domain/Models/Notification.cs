using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? UserId { get; set; } // Consider changing to int if UserId is an integer
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Type { get; set; } // Consider creating an enum for notification types
        public int SourceType { get; set; } // Consider creating an enum for source types
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Content { get; set; }
        public Users? User { get; set; } // Ensure User class exists
    }
}
