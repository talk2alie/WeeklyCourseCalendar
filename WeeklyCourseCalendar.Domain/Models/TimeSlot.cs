using System;
using System.Collections.Generic;
using System.Text;

namespace WeeklyCourseCalendar.Domain.Models
{
    public class TimeSlot
    {
        private List<Class> _classes { get; set; }

        public DayOfWeek? Day { get; }
        public DateTime? Time { get; }


        public TimeSlot()
        {
        }

        public TimeSlot(DayOfWeek day, DateTime time)
        {
            Day = day;
            Time = time;
            _classes = new List<Class>();
        }
    }
}
