using System;
using System.Collections.Generic;
using System.Linq;
using WeeklyCourseCalendar.Domain.Helpers;

namespace WeeklyCourseCalendar.Domain
{
    public class TimeSlot
    {
        private readonly DateTime _schoolStartTime = DateTime.Parse("8:00 AM");
        private readonly DateTime _schoolEndTime = DateTime.Parse("9:00 PM");
        private readonly HashSet<Class> _classes;

        private const int _acceptedNumberOfClasses = 10;

        public DayOfWeek Day { get; }

        public DateTime Time { get; }

        public int OccupiedSpacesCount => _classes.Count();

        public bool IsFull => _classes.Count() == _acceptedNumberOfClasses;

        public string Id { get; }

        public int MaximumCapacity => _acceptedNumberOfClasses;

        public int SlotSpan { get; }

        public TimeSlot(DayOfWeek day, DateTime time, int slotSpan)
        {
            if (TheGivenDayIsNotASchoolDay(day))
            {
                throw new ArgumentOutOfRangeException(nameof(day), "The provided day is not a valid school day");
            }

            if (TheGivenTimeIsOutsideSchoolHours(time))
            {
                throw new ArgumentOutOfRangeException(nameof(time), "The provided time is outside normal school hours");
            }

            Day = day;
            Time = time;
            SlotSpan = slotSpan;
            Id = TimeSlotHelpers.GenerateIdFromDaysAndTime(day, time);
            _classes = new HashSet<Class>(_acceptedNumberOfClasses);
        }

        private bool TheGivenDayIsNotASchoolDay(DayOfWeek day)
        {
            // At Villanova University, weekend days are not school days
            return (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday) ? true : false;
        }

        private bool TheGivenTimeIsOutsideSchoolHours(DateTime time)
        {
            return (time.TimeOfDay < _schoolStartTime.TimeOfDay ||
                time.TimeOfDay > _schoolEndTime.TimeOfDay) ? true : false;
        }

        public IEnumerable<Class> Classes => _classes.OrderByDescending(@class => @class.StartTime);

        public void AddClass(Class @class)
        {
            if (TheGivenClassCannnotOccupyThisSlot(@class))
            {
                throw new InvalidOperationException("The given class cannot be placed in this slot. " +
                    "Please check that this slot falls within its day and time");
            }

            if (IsFull)
            {
                throw new InvalidOperationException("The given class cannot be placed in this slot. " +
                    $"The slot has reached its maximum capacity of {_acceptedNumberOfClasses}");
            }

            if (TheGivenClassAlreadyExists(@class))
            {
                throw new InvalidOperationException("The given class already exists in the slot");
            }
        }

        private bool TheGivenClassCannnotOccupyThisSlot(Class @class)
        {
            if (TheGivenDayIsNotASchoolDay(@class.Day))
            {
                return true;
            }

            if (Time < @class.StartTime || Time > @class.EndTime)
            {
                return true;
            }
            return false;
        }

        private bool TheGivenClassAlreadyExists(Class @class)
        {
            return !_classes.Add(@class);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is TimeSlot timeSlot)
            {
                return Id.Equals(timeSlot.Id) && SlotSpan == timeSlot.SlotSpan;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ SlotSpan.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Id}; Spans {SlotSpan}" + (SlotSpan > 1 ? "s" : String.Empty);
        }
    }
}