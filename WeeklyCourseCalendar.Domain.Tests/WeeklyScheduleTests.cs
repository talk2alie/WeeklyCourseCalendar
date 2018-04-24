using System;
using System.Linq;
using Xunit;

namespace WeeklyCourseCalendar.Domain.Tests
{
    public class WeeklyScheduleTests
    {
        private readonly WeeklySchedule _weeklySchedule;

        public WeeklyScheduleTests()
        {
            _weeklySchedule = new WeeklySchedule();
        }

        [Fact, Trait("Category", "WeeklySchedule")]
        public void WeeklySchedule_CanCreateInstance()
        {
            // Arrange
            const int expectedSchoolDaysCount = 5;
            const int expectedSchoolTimesCount = 157;

            // Act
            var weeklySchedule = new WeeklySchedule();

            // Assert
            Assert.NotNull(weeklySchedule);
            int actualSchoolDaysCount = weeklySchedule.GetSchoolDays().ToList().Count;
            int actualSchoolTimesCount = weeklySchedule.GetSchoolTimes().ToList().Count;

            Assert.Equal(expectedSchoolDaysCount, actualSchoolDaysCount);
            Assert.Equal(expectedSchoolTimesCount, actualSchoolTimesCount);
        }
    }
}