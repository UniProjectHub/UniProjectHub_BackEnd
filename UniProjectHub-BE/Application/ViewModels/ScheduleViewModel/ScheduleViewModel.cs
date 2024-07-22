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

        public Guid? UserId { get; set; }
        public string DateOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public DateTime SlotStartTime { get; set; }
        public DateTime SlotEndTime { get; set; }
        public string CourseName { get; set; }
    }
    public class CreateScheduleViewModel
    {
 

        public Guid? UserId { get; set; }
        public string DateOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public DateTime SlotStartTime { get; set; }
        public DateTime SlotEndTime { get; set; }
        public string CourseName { get; set; }
    }
    public class UpdateScheduleViewModel
    {
        public Guid? UserId { get; set; }
        public string DateOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime SlotStartTime { get; set; }
        public DateTime SlotEndTime { get; set; }
        public string CourseName { get; set; }
    }
}