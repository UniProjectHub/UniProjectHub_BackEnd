using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SubTask
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int TaskId { get; set; }
        public string? Tags { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Deadline { get; set;}
        public Task? Task { get; set; }
    }
}
