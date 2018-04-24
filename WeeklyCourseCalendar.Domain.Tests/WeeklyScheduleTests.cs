using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WeeklyCourseCalendar.Domain.Tests
{
    public class WeeklyScheduleTests
    {
        private readonly WeeklySchedule _weeklySchedule;

        public WeeklyScheduleTests()
        {
            string semesterName = "Fall 2018";
            var semesterStartDate = DateTime.Parse("August 16, 2018");
            var semesterEndDate = DateTime.Parse("December 7, 2018");
            _weeklySchedule = new WeeklySchedule(semesterName, semesterStartDate, semesterEndDate);
        }

        [Fact, Trait("Category", "WeeklySchedule")]
        public void WeeklySchedule_CanCreateInstance()
        {
            // Arrange
            const int expectedSchoolDaysCount = 5;
            const int expectedSchoolTimesCount = 157;
            const string expectedSemesterName = "Fall 2019";
            var expectedSemesterStartDate = DateTime.Parse("August 16, 2018");
            var expectedSemesterEndDate = DateTime.Parse("December 7, 2018");

            // Act
            var weeklySchedule = new WeeklySchedule(expectedSemesterName, expectedSemesterStartDate, expectedSemesterEndDate);

            // Assert
            Assert.NotNull(weeklySchedule);
            int actualSchoolDaysCount = weeklySchedule.GetSchoolDays().ToList().Count;
            int actualSchoolTimesCount = weeklySchedule.GetSchoolTimes().ToList().Count;

            string actualSemesterName = weeklySchedule.SemesterName;
            DateTime actualSemesterStartDate = weeklySchedule.SemesterStartDate;
            DateTime actualSemesterEndDate = weeklySchedule.SemesterEndDate;
            IEnumerable<TimeSlot> timeSlots = weeklySchedule.GetTimeSlots();

            Assert.Equal(expectedSchoolDaysCount, actualSchoolDaysCount);
            Assert.Equal(expectedSchoolTimesCount, actualSchoolTimesCount);

            Assert.Equal(expectedSemesterName, actualSemesterName);
            Assert.Equal(expectedSemesterStartDate, actualSemesterStartDate);
            Assert.Equal(expectedSemesterEndDate, actualSemesterEndDate);
        }
    }
}