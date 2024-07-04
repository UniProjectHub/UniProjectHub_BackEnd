using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ScheduleViewModel
{
    public class ScheduleViewModel
    {
        public string UserId { get; set; }
        public string DateOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
       
        public string CourseName { get; set; }
    }
}