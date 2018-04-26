namespace WeeklyCourseCalendar.Domain.Services
{
    public interface IWeeklyScheduleWriter
    {
        void WriteAsHtml(WeeklySchedule weeklySchedule, string outputPath);
    }
}