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

        public bool CanAcceptClass => _classes.Count() < _acceptedNumberOfClasses;

        public string Id => $"{Day.ToString()}_{Time.ToShortTimeString()}".Replace(" ", "");

        public TimeSlot(DaysOfWeek day, DateTime time)
        {
            if (!IsSchoolDay(day))
            {
                throw new ArgumentOutOfRangeException(nameof(day), "The provided day is not a valid school day");
            }

            if (!IsDuringSchoolHours(time))
            {
                throw new ArgumentOutOfRangeException(nameof(time), "The provided time is outside normal school hours");
            }

            Day = day;
            Time = time;
            _classes = new HashSet<Class>(_acceptedNumberOfClasses);
        }

        private bool IsSchoolDay(DaysOfWeek day)
        {
            if (day == DaysOfWeek.Saturday || day == DaysOfWeek.Sunday)
            {
                return false;
            }

            if (day.HasFlag(DaysOfWeek.Saturday) || day.HasFlag(DaysOfWeek.Sunday))
            {
                return false;
            }

            return true;
        }

        private bool IsDuringSchoolHours(DateTime time)
        {
            return (time.TimeOfDay < _schoolStartTime.TimeOfDay ||
                time.TimeOfDay > _schoolEndTime.TimeOfDay) ? false : true;
        }

        public IEnumerable<Class> GetClasses()
        {
            return _classes.ToList();
        }

        public void AddClass(Class @class)
        {
            if (!CanOccupyThisSlot(@class))
            {
                throw new InvalidOperationException("The given class cannot be placed in this slot. " +
                    "Please check that this slot falls within its day and time");
            }

            if (!CanAcceptClass)
            {
                throw new InvalidOperationException("The given class cannot be placed in this slot. " +
                    $"The slot has reached its maximum capacity of {_acceptedNumberOfClasses}");
            }

            if (!_classes.Add(@class))
            {
                throw new InvalidOperationException("The given class already exists in the slot");
            }
        }

        private bool CanOccupyThisSlot(Class @class)
        {
            if (!IsSchoolDay(@class.Days))
            {
                return false;
            }

            if (Time < @class.StartTime || Time > @class.EndTime)
            {
                return false;
            }
            return true;
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