using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Models
{
    public class Task
    {
        public int Id {  get; set; }
        public int ProjectId { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public string? Category {  get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime Deadline { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public int RemainingTime { get; set; }
        public Project? Project { get; set; }
        public ICollection<MemberInTask>? Members { get; set; }
        public ICollection<SubTask>? subTasks { get; set; }
        public ICollection<File>? files { get; set; }

    }
}
