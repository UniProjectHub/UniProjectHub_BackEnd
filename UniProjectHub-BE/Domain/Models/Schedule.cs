﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? DateOfWeek { get; set; } // Assuming this is a string for the day of the week, if it's not a DateTime
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public DateTime SlotStartTime { get; set; }
        public DateTime SlotEndTime { get; set; }
       
        public Users? User { get; set; }
        public string CourseName { get; set; }
    }

}
