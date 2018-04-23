using System;
using System.Collections.Generic;
using System.Linq;
using WeeklyCourseCalendar.Domain.Helpers;

namespace WeeklyCourseCalendar.Domain
{
    public class WeeklySchedule
    {
        private readonly HashSet<TimeSlot> _allocatedTimeSlots;

        public string SemesterName { get; set; }

        public DateTime SemesterStartDate { get; set; }

        public DateTime SemesterEndDate { get; set; }

        public int AllocatedTimeSlotsCount => _allocatedTimeSlots.Count;

        public WeeklySchedule()
        {
            const int maximumNumberOfSlots = 157;
            _allocatedTimeSlots = new HashSet<TimeSlot>(maximumNumberOfSlots);
        }

        public void AddClassToTimeSlots(Class @class)
        {
            DateTime slotTime = @class.StartTime;
            const int slotDurationInMinutes = 5;
            while (slotTime.TimeOfDay <= @class.EndTime.TimeOfDay)
            {
                TimeSlot timeSlot = FindOrCreateTimeSlotFromDaysAndTime(@class.Days, slotTime);
                timeSlot.AddClass(@class);
                slotTime = slotTime.AddMinutes(slotDurationInMinutes);
            }
        }

        private TimeSlot FindOrCreateTimeSlotFromDaysAndTime(DaysOfWeek slotDays, DateTime slotTime)
        {
            string slotId = TimeSlotHelpers.GenerateIdFromDaysAndTime(slotDays, slotTime);
            TimeSlot timeSlot = _allocatedTimeSlots
                .SingleOrDefault(slot => slot.Id.Equals(slotId, StringComparison.InvariantCulture));
            if (timeSlot == null)
            {
                timeSlot = new TimeSlot(slotDays, slotTime);
                _allocatedTimeSlots.Add(timeSlot);
            }
            return timeSlot;
        }

        public List<TimeSlot> GetAllocatedTimeSlots()
        {
            return _allocatedTimeSlots.ToList();
        }
    }
}