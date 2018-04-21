using System;
using System.Collections.Generic;
using System.Text;

namespace WeeklyCourseCalendar.Domain.Models
{
    public class Class
    {
        public string Name { get; set; }

        public string Section { get; set; }

        public DayOfWeek Day { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Location { get; set; }
    }
}
