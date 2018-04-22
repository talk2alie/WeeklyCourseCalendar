using System;
using System.Collections.Generic;

namespace WeeklyCourseCalendar.Domain
{
    public class WeeklySchedule
    {
        private readonly Dictionary<DaysOfWeek, HashSet<TimeSlot>> _availableSlotsPerDay;
        private readonly List<DaysOfWeek> _schoolDays;

        public string SemesterName { get; set; }

        public DateTime SemesterStartDate { get; set; }

        public DateTime SemesterEndDate { get; set; }

        public WeeklySchedule()
        {
            _schoolDays = new List<DaysOfWeek>
            {
                DaysOfWeek.Monday,
                DaysOfWeek.Tuesday,
                DaysOfWeek.Wednesday,
                DaysOfWeek.Thursday,
                DaysOfWeek.Friday
            };
            _availableSlotsPerDay = new Dictionary<DaysOfWeek, HashSet<TimeSlot>>();
            CreateSlotsForSchoolDays();
        }

        private void CreateSlotsForSchoolDays()
        {
            var schoolStartTime = DateTime.Parse("8:00 AM");
            var schoolEndTime = DateTime.Parse("9:00 PM");
            const int slotDurationInMinutes = 5;

            _schoolDays.ForEach(schoolDay =>
            {
                var timeSlotsForDay = new HashSet<TimeSlot>();
                DateTime currentTime = schoolStartTime;
                while (currentTime.TimeOfDay <= schoolEndTime.TimeOfDay)
                {
                    timeSlotsForDay.Add(new TimeSlot(day: schoolDay, time: currentTime));
                    currentTime = currentTime.AddMinutes(slotDurationInMinutes);
                }
                _availableSlotsPerDay.Add(schoolDay, timeSlotsForDay);
            });
        }
    }
}