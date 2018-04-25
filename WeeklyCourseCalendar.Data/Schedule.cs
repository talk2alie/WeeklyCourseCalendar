using System;

namespace WeeklyCourseCalendar.Data
{
    public class Schedule
    {
        public DaysOfWeek Days { get; set; }

        public string Location { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public override string ToString()
        {
            return $"{Days} from {StartTime.ToShortTimeString()} to {EndTime.ToShortTimeString()}";
        }
    }
}