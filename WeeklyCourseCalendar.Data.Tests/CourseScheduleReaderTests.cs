using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeeklyCourseCalendar.Data.Services;
using Xunit;

namespace WeeklyCourseCalendar.Data.Tests
{
    public class CourseScheduleReaderTests
    {
        private readonly ICourseScheduleReader _courseScheduleReader;
        private const string _filePath = "schedule.data";

        public CourseScheduleReaderTests()
        {
            _courseScheduleReader = new CourseScheduleReader();
        }

        [Fact, Trait("Category", "CourseScheduleReader")]
        public void CourseScheduleReader_CanCreateInstance()
        {
            // Arrange

            // Act
            var courseScheduleReader = new CourseScheduleReader();

            // Assert
            Assert.NotNull(courseScheduleReader);
        }

        [Fact, Trait("Category", "CourseScheduleReader")]
        public void ReadFromFile_ValidFilePath_Returns5Courses()
        {
            // Arrange
            const int expectedCourseCount = 5;

            // Act
            IEnumerable<Course> courses = _courseScheduleReader.ReadFromFile(_filePath);

            // Assert
            Assert.NotNull(courses);

            int actualCoursesCount = courses.ToList().Count;
            Assert.Equal(expectedCourseCount, actualCoursesCount);
        }

        [Fact, Trait("Category", "CourseScheduleReader")]
        public void ReadFromFile_InvalidFilePath_ThrowsFileNotFoundException()
        {
            // Arrange

            // Act
            string filePath = "somefile.data";
            Exception expectedException = Record.Exception(() => _courseScheduleReader.ReadFromFile(filePath));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<FileNotFoundException>(expectedException);
        }
    }
}