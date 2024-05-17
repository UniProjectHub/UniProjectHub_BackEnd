using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string? OwnerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public int CategoryID { get; set; }
        public DateTime CreatedAt { get; set; }
        public Users? User { get; set; }
        public Category? Category { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
