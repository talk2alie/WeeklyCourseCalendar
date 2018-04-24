using System;
using System.Collections.Generic;
using System.Linq;
using WeeklyCourseCalendar.Domain.Helpers;

namespace WeeklyCourseCalendar.Domain
{
    public class WeeklySchedule
    {
        private readonly HashSet<DayOfWeek> _schoolDays;
        private readonly HashSet<DateTime> _schoolTimes;

        public DateTime SemesterStartDate { get; set; }

        public DateTime SemesterEndDate { get; set; }

        public string SemesterName { get; set; }

        public WeeklySchedule()
        {
            _schoolDays = new HashSet<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            };
            _schoolTimes = new HashSet<DateTime>();
            LoadSchoolTimes();
        }

        public IEnumerable<DayOfWeek> GetSchoolDays()
        {
            return _schoolDays;
        }

        public IEnumerable<DateTime> GetSchoolTimes()
        {
            return _schoolTimes;
        }

        private void LoadSchoolTimes()
        {
            var schoolStartTime = DateTime.Parse("8:00 AM");
            var schoolEndTime = DateTime.Parse("9:00 PM");

            const int slotDurationInMinutes = 5;
            DateTime time = schoolStartTime;
            while (time.TimeOfDay <= schoolEndTime.TimeOfDay)
            {
                _schoolTimes.Add(time);
                time = time.AddMinutes(slotDurationInMinutes);
            }
        }
    }
}