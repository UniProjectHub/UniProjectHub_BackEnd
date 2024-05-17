using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? OwnerId { get; set; }
        public int BlogId { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Blog? Blog { get; set; }
        public Users? Users { get; set; }

    }
}
