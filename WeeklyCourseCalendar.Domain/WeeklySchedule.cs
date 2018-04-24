using System;
using System.Collections.Generic;
using System.Linq;
using WeeklyCourseCalendar.Domain.Helpers;

namespace WeeklyCourseCalendar.Domain
{
    public class WeeklySchedule
    {
        private readonly HashSet<TimeSlot> _availableTimeSlots;

        public List<DayOfWeek> SchooldDays => new List<DayOfWeek>
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday
        };

        public DateTime SchoolStartTime => DateTime.Parse("8:00 AM");

        public DateTime SchoolEndTime => DateTime.Parse("9:00 PM");

        public string SemesterName { get; set; }

        public DateTime SemesterStartDate { get; set; }

        public DateTime SemesterEndDate { get; set; }

        public int AllocatedTimeSlotsCount => _availableTimeSlots.Count;

        public WeeklySchedule()
        {
            const int maximumNumberOfSlots = 157;
            _availableTimeSlots = new HashSet<TimeSlot>(maximumNumberOfSlots);
        }

        public void AddClassToTimeSlots(Class @class)
        {
            DateTime slotTime = @class.StartTime;
            const int slotDurationInMinutes = 5;
            while (slotTime.TimeOfDay <= @class.EndTime.TimeOfDay)
            {
                TimeSlot timeSlot = FindOrCreateTimeSlotFromDaysAndTime(@class.Day, slotTime);
                timeSlot.AddClass(@class);
                slotTime = slotTime.AddMinutes(slotDurationInMinutes);
            }
        }

        private TimeSlot FindOrCreateTimeSlotFromDaysAndTime(DayOfWeek slotDays, DateTime slotTime)
        {
            string slotId = TimeSlotHelpers.GenerateIdFromDaysAndTime(slotDays, slotTime);
            TimeSlot timeSlot = _availableTimeSlots
                .SingleOrDefault(slot => slot.Id.Equals(slotId, StringComparison.InvariantCulture));
            if (timeSlot == null)
            {
                timeSlot = new TimeSlot(slotDays, slotTime);
                _availableTimeSlots.Add(timeSlot);
            }
            return timeSlot;
        }

        public List<TimeSlot> GetAllocatedTimeSlots()
        {
            return _availableTimeSlots.ToList();
        }
    }
}