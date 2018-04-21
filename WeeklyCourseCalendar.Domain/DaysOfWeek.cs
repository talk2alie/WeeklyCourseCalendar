using System;
using System.Collections.Generic;
using System.Text;

namespace WeeklyCourseCalendar.Domain
{
    [Flags]
    public enum DaysOfWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64
    }
}