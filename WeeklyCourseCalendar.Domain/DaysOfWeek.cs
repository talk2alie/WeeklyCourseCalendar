using System;
using System.Collections.Generic;
using System.Text;

namespace WeeklyCourseCalendar.Domain
{
    [Flags]
    public enum DaysOfWeek
    {
        None = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64,
        MondayWednesdayFriday = Monday | Wednesday | Friday,
        TuesdayThursday = Tuesday | Thursday
    }
}