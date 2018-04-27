using AutoMapper;
using System;
using System.Collections.Generic;
using WeeklyCourseCalendar.Data;
using WeeklyCourseCalendar.Domain;

namespace WeeklyCourseCalendar.App
{
    public class MapperConfiguration
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<IEnumerable<Course>, IEnumerable<Class>>().ConstructUsing(MapperConfiguration.Map);
            });
        }

        private static IEnumerable<Class> Map(IEnumerable<Course> courses)
        {
            var classes = new List<Class>();
            foreach (Course course in courses)
            {
                foreach (Schedule schedule in course.Schedules)
                {
                    List<DayOfWeek> days = TransFormDaysFlagsToDays(schedule.Days);
                    foreach (DayOfWeek day in days)
                    {
                        var @class = new Class
                        {
                            Day = day,
                            EndTime = schedule.EndTime,
                            Instructors = course.Instructor ?? "TBA",
                            Location = schedule.Location ?? "TBA",
                            Name = course.Number,
                            Section = course.Section,
                            StartTime = schedule.StartTime,
                            Title = course.Name
                        };
                        classes.Add(@class);
                    }
                }
            }
            return classes;
        }

        private static List<DayOfWeek> TransFormDaysFlagsToDays(DaysOfWeek daysFlags)
        {
            const int maximumNumberOfSchoolDays = 5;
            var days = new List<DayOfWeek>(maximumNumberOfSchoolDays);
            if (daysFlags.HasFlag(DaysOfWeek.Monday))
            {
                days.Add(DayOfWeek.Monday);
            }

            if (daysFlags.HasFlag(DaysOfWeek.Tuesday))
            {
                days.Add(DayOfWeek.Tuesday);
            }

            if (daysFlags.HasFlag(DaysOfWeek.Wednesday))
            {
                days.Add(DayOfWeek.Wednesday);
            }

            if (daysFlags.HasFlag(DaysOfWeek.Thursday))
            {
                days.Add(DayOfWeek.Thursday);
            }

            if (daysFlags.HasFlag(DaysOfWeek.Friday))
            {
                days.Add(DayOfWeek.Friday);
            }
            return days;
        }
    }
}