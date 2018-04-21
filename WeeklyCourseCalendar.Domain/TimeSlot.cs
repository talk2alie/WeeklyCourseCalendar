using System;
using System.Collections.Generic;

namespace WeeklyCourseCalendar.Domain
{
    public class TimeSlot
    {
        private readonly List<Class> _classes;

        public DaysOfWeek Day { get; }

        public DateTime Time { get; }

        public string Id => $"{Day.ToString()}:{Time.ToShortTimeString()}".Replace(" ", "");

        public TimeSlot(DaysOfWeek day, DateTime time) : this(day, time, new List<Class>())
        {
        }

        public TimeSlot(DaysOfWeek day, DateTime time, List<Class> classes)
        {
            Day = day;
            Time = time;
            _classes = classes;
        }

        public bool AddClass(Class spanningClass)
        {
            _classes.Add(spanningClass);

            return false;
        }

        public void AddClasses(IEnumerable<Class> spanningClasses)
        {
            _classes.AddRange(spanningClasses);
        }

        private static bool CanOccupyTimeSlot(this Class @class, TimeSlot timeSlot)
        {
            if (!@class.Days.HasFlag(timeSlot.Day))
            {
                return false;
            }

            if (timeSlot.Time < @class.StartTime || timeSlot.Time > @class.EndTime)
            {
                return false;
            }
            return true;
        }
    }
}