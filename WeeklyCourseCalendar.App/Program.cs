using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WeeklyCourseCalendar.Data;
using WeeklyCourseCalendar.Data.Services;
using WeeklyCourseCalendar.Domain;
using WeeklyCourseCalendar.Domain.Services;

namespace WeeklyCourseCalendar.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var startup = new Startup();
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);

            ServiceProvider serviceBuilder = services.BuildServiceProvider();
            ICourseScheduleReader reader = serviceBuilder.GetRequiredService<ICourseScheduleReader>();

            string filePath = args[0];
            IEnumerable<Course> courses = reader.ReadFromFile(filePath);

            IMapper mapper = serviceBuilder.GetRequiredService<IMapper>();
            IEnumerable<Class> classes = mapper.Map<IEnumerable<Class>>(courses);

            var weeklySchedule = new WeeklySchedule("Fall 2018", DateTime.Parse("August 16, 2018"), DateTime.Parse("December 7, 2018"));
            foreach (Class @class in classes)
            {
                weeklySchedule.AddClass(@class);
            }

            IWeeklyScheduleWriter writer = serviceBuilder.GetRequiredService<IWeeklyScheduleWriter>();
            string outputFileName = "schedule";
            outputFileName = writer.WriteAsHtml(weeklySchedule, outputFileName);
        }
    }
}