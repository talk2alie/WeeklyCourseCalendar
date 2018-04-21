using System;
using System.Collections.Generic;
using System.Linq;

namespace WeeklyCourseCalendar.Domain
{
    public class TimeSlot
    {
        private readonly DateTime _schoolStartTime = DateTime.Parse("8:00 AM");
        private readonly DateTime _schoolEndTime = DateTime.Parse("9:00 PM");
        private readonly List<Class> _classes;
        private const int _acceptedNumberOfClasses = 10;

        public DaysOfWeek Day { get; }

        public DateTime Time { get; }

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
            _classes = new List<Class>(_acceptedNumberOfClasses);
        }

        public bool TryAddClass(Class spanningClass)
        {
            if (!CanOccupyThisSlot(spanningClass))
            {
                return false;
            }

            _classes.Add(spanningClass);
            return true;
        }

        public void AddClasses(IEnumerable<Class> spanningClasses)
        {
            _classes.AddRange(spanningClasses);
        }

        private bool CanOccupyThisSlot(Class spanningClass)
        {
            if (!spanningClass.Days.HasFlag(Day))
            {
                return false;
            }

            if (Time < spanningClass.StartTime || Time > spanningClass.EndTime)
            {
                return false;
            }
            return true;
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

        private bool CanAccomodateNMoreClasses(int n)
        {
            return ((_classes.Count + n) <= _classes.Capacity) ? true : false;
        }
    }
}