using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WeeklyCourseCalendar.Data.Services;
using WeeklyCourseCalendar.Domain.Services;

namespace WeeklyCourseCalendar.App
{
    public class Startup
    {
        public Startup()
        {
            MapperConfiguration.ConfigureAutoMapper();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(factory => Mapper.Instance);
            services.AddSingleton<ICourseScheduleReader, CourseScheduleReader>();
            services.AddTransient<IWeeklyScheduleWriter, WeeklyScheduleWriter>();
        }
    }
}