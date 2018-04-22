using System;
using System.Collections.Generic;
using System.Linq;

namespace WeeklyCourseCalendar.Domain
{
    public class TimeSlot
    {
        private readonly DateTime _schoolStartTime = DateTime.Parse("8:00 AM");
        private readonly DateTime _schoolEndTime = DateTime.Parse("9:00 PM");
        private readonly HashSet<Class> _classes;

        private const int _acceptedNumberOfClasses = 10;

        public DaysOfWeek Day { get; }

        public DateTime Time { get; }

        public int OccupiedSpacesCount => _classes.Count();

        public bool IsFull => _classes.Count() == _acceptedNumberOfClasses;

        public string Id => $"{Day.ToString()}_{Time.ToShortTimeString()}".Replace(" ", "");

        public int MaximumCapacity => _acceptedNumberOfClasses;

        public TimeSlot(DaysOfWeek day, DateTime time)
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
            _classes = new HashSet<Class>(_acceptedNumberOfClasses);
        }

        private bool TheGivenDayIsNotASchoolDay(DaysOfWeek day)
        {
            // At Villanova University, weekend days are not school days
            return (day.HasFlag(DaysOfWeek.Saturday) || day.HasFlag(DaysOfWeek.Sunday)) ? true : false;
        }

        private bool TheGivenTimeIsOutsideSchoolHours(DateTime time)
        {
            return (time.TimeOfDay < _schoolStartTime.TimeOfDay ||
                time.TimeOfDay > _schoolEndTime.TimeOfDay) ? true : false;
        }

        public IEnumerable<Class> GetClasses()
        {
            return _classes.ToList();
        }

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
            if (TheGivenDayIsNotASchoolDay(@class.Days))
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
                return Id.Equals(timeSlot.Id);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id;
        }
    }
}