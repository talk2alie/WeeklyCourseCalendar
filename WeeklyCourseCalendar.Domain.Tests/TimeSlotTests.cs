using System;
using System.Collections.Generic;
using Xunit;

namespace WeeklyCourseCalendar.Domain.Tests
{
    public class TimeSlotTests
    {
        [Fact, Trait("Category", "TimeSlot_Tests")]
        public void TimeSlotCreation_SchoolDayAndTime_CreatesInstance()
        {
            // Arrange
            DaysOfWeek expectedDay = DaysOfWeek.Monday;
            var expectedTime = DateTime.Parse("8:00 AM");
            string expectedTimeSlotId = $"{expectedDay.ToString()}_{expectedTime.ToShortTimeString()}".Replace(" ", "");

            // Act
            var timeSlot = new TimeSlot(day: DaysOfWeek.Monday, time: DateTime.Parse("8:00 AM"));
            DaysOfWeek actualDay = timeSlot.Day;
            DateTime actualTime = timeSlot.Time;
            string actualTimeSlotId = timeSlot.Id;

            // Assert
            Assert.NotNull(timeSlot);
            Assert.Equal(expectedDay, actualDay);
            Assert.Equal(expectedTime, actualTime);
            Assert.Equal(expectedTimeSlotId, actualTimeSlotId);
        }

        [Fact, Trait("Category", "TimeSlot_Tests")]
        public void TimeSlotCreation_NonSchoolDay_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            DaysOfWeek invalidDay = DaysOfWeek.Monday | DaysOfWeek.Saturday;
            var time = DateTime.Parse("8:00 AM");

            // Act
            Exception expectedException = Record.Exception(() => new TimeSlot(invalidDay, time));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<ArgumentOutOfRangeException>(expectedException);
        }

        [Fact, Trait("Category", "TimeSlot_Tests")]
        public void TimeSlotCreation_NonSchoolHours_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            DaysOfWeek day = DaysOfWeek.Monday;
            var invalidTime = DateTime.Parse("7:00 AM");

            // Act
            Exception expectedException = Record.Exception(() => new TimeSlot(day, invalidTime));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<ArgumentOutOfRangeException>(expectedException);
        }
    }
}