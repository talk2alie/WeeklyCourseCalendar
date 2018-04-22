using System;

namespace WeeklyCourseCalendar.Domain
{
    public class WeeklySchedule
    {
        public string SemesterName { get; set; }

        public DateTime SemesterStartDate { get; set; }

        public DateTime SemesterEndDate { get; set; }
    }
}