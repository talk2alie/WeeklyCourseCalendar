using System.Collections.Generic;

namespace WeeklyCourseCalendar.Data.Services
{
    public interface ICourseScheduleReader
    {
        IEnumerable<Course> ReadFromFile(string filePath);
    }
}