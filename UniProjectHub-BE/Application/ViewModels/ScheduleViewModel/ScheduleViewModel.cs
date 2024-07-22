using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ScheduleViewModel
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string DateOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SlotStartTime { get; set; }
        public string SlotEndTime { get; set; }
        public string CourseName { get; set; }
    }

    public class CreateScheduleViewModel
    {
        public string? UserId { get; set; }
        public string DateOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SlotStartTime { get; set; }
        public string SlotEndTime { get; set; }
        public string CourseName { get; set; }
    }

    public class UpdateScheduleViewModel
    {
        public string? UserId { get; set; }
        public string DateOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SlotStartTime { get; set; }
        public string SlotEndTime { get; set; }
        public string CourseName { get; set; }
    }
}