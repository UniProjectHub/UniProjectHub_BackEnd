using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TaskViewModel
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
        public DateTime Deadline { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
    }
}
