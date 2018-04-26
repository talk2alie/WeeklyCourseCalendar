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
        private readonly HashSet<TimeSlot> _timeSlots;

        public DateTime SemesterStartDate { get; set; }

        public DateTime SemesterEndDate { get; set; }

        public string SemesterName { get; }

        public IEnumerable<TimeSlot> TimeSlots => _timeSlots.ToList();

        public IEnumerable<DayOfWeek> SchoolDays => _schoolDays.ToList();

        public IEnumerable<DateTime> SchoolTimes => _schoolTimes.ToList();

        public WeeklySchedule(string semesterName, DateTime semesterStartDate, DateTime semesterEndDate)
        {
            SemesterName = semesterName;
            SemesterStartDate = semesterStartDate;
            SemesterEndDate = semesterEndDate;
            _timeSlots = new HashSet<TimeSlot>();

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

        public void AddClass(Class newClass)
        {
            TimeSlot timeSlot = FindOrCreateTimeSlotForClass(newClass);
            timeSlot.AddClass(newClass);
        }

        private TimeSlot FindOrCreateTimeSlotForClass(Class newClass)
        {
            TimeSpan classDuration = newClass.EndTime.TimeOfDay - newClass.StartTime.TimeOfDay;
            const int slotDurationInMinutes = 5;
            const int slotOffset = 1;
            int numberOfTimeSlotsClassSpans = ((int)classDuration.TotalMinutes / slotDurationInMinutes) + slotOffset;

            string slotId = TimeSlotHelpers.GenerateIdFromDaysAndTime(newClass.Day, newClass.StartTime);
            TimeSlot timeSlot = _timeSlots.SingleOrDefault(slot =>
                slot.Id.Equals(slotId, StringComparison.InvariantCultureIgnoreCase) &&
                slot.SlotSpan == numberOfTimeSlotsClassSpans);
            if (timeSlot == null)
            {
                timeSlot = new TimeSlot(day: newClass.Day, time: newClass.StartTime, slotSpan: numberOfTimeSlotsClassSpans);
                _timeSlots.Add(timeSlot);
            }

            return timeSlot;
        }
    }
}