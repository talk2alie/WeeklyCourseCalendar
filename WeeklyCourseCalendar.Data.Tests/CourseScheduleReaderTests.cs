using System;
using WeeklyCourseCalendar.Data.Services;
using Xunit;

namespace WeeklyCourseCalendar.Data.Tests
{
    public class CourseScheduleReaderTests
    {
        private readonly ICourseScheduleReader _courseScheduleReader;
        private const string filePath = "schedule.data";

        public CourseScheduleReaderTests()
        {
            _courseScheduleReader = new CourseScheduleReader();
        }
    }
}