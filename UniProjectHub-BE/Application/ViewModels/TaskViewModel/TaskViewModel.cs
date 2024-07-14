using Application.ViewModels.SubTaskViewModel;
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
        public int ProjectId { get; set; }
        public string? TaskName { get; set; }
        public int Status { get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime Deadline { get; set; }
        public int Rate { get; set; }
        public int RemainingTime { get; set; }
    }
    public class ShowTask
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? TaskName { get; set; }
        public int Status { get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime Deadline { get; set; }
        public int Rate { get; set; }
        public int RemainingTime { get; set; }
        public ICollection<ShowSubTask>? SubTasks { get; set;}
        public ICollection<ShowMember>? members { get; set; }
    }
    public class ShowSubTask
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
        public DateTime? Deadline { get; set; }
    }
    public class ShowMember
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
