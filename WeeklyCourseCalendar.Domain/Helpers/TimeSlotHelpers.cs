﻿using System;

namespace WeeklyCourseCalendar.Domain.Helpers
{
    public static class TimeSlotHelpers
    {
        public static string GenerateIdFromDaysAndTime(DayOfWeek day, DateTime time)
        {
            return $"{day.ToString()}_{time.ToShortTimeString()}".Replace(" ", "");
        }
    }
}