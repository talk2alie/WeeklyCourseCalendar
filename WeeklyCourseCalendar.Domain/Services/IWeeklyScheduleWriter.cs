namespace WeeklyCourseCalendar.Domain.Services
{
    public interface IWeeklyScheduleWriter
    {
        string WriteAsHtml(WeeklySchedule weeklySchedule, string outputPath);
    }
}