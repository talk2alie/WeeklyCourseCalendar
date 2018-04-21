using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WeeklyCourseCalendar.Domain.Tests
{
    public class TimeSlotTests
    {
        private readonly TimeSlot _timeSlot;

        public TimeSlotTests()
        {
            _timeSlot = new TimeSlot(day: DaysOfWeek.Monday, time: DateTime.Parse("10:05 AM"));
        }

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

        [Fact, Trait("Category", "TimeSlot_Tests")]
        public void AddClass_AcceptableClass_AddsClassSuccessfully()
        {
            // Arrange
            var acceptableClass = new Class
            {
                Days = DaysOfWeek.Monday | DaysOfWeek.Wednesday | DaysOfWeek.Friday,
                EndTime = DateTime.Parse("12:00 PM"),
                Instructors = "Mary Joe",
                Location = "Mendel 154",
                Name = "CSC 1210",
                Section = "001",
                StartTime = DateTime.Parse("8:05 AM")
            };
            const int addedClassCount = 1;
            int expectedClassesCount = _timeSlot.OccupiedSpacesCount + addedClassCount;

            // Act
            _timeSlot.AddClass(acceptableClass);
            int actualClassesCount = _timeSlot.OccupiedSpacesCount;

            // Assert
            Assert.Equal(expectedClassesCount, actualClassesCount);
        }

        [Fact, Trait("Category", "TimeSlot_Tests")]
        public void AddClass_ClassEndsBeforeSlotTime_ThrowsInvalidOperationException()
        {
            // Arrange
            var unacceptableClass = new Class
            {
                Days = DaysOfWeek.Monday | DaysOfWeek.Wednesday | DaysOfWeek.Friday,
                EndTime = DateTime.Parse("10:00 AM"),
                Instructors = "Mary Joe",
                Location = "Mendel 154",
                Name = "CSC 1210",
                Section = "001",
                StartTime = DateTime.Parse("9:15 AM")
            };

            // Act
            Exception expectedException = Record.Exception(() => _timeSlot.AddClass(unacceptableClass));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<InvalidOperationException>(expectedException);
        }

        [Fact, Trait("Category", "TimeSlot_Tests")]
        public void AddClass_ClassStartsAfterSlotTime_ThrowsInvalidOperationException()
        {
            // Arrange
            var unacceptableClass = new Class
            {
                Days = DaysOfWeek.Monday | DaysOfWeek.Wednesday | DaysOfWeek.Friday,
                EndTime = DateTime.Parse("9:20 AM"),
                Instructors = "Mary Joe",
                Location = "Mendel 154",
                Name = "CSC 1210",
                Section = "001",
                StartTime = DateTime.Parse("10:10 AM")
            };

            // Act
            Exception expectedException = Record.Exception(() => _timeSlot.AddClass(unacceptableClass));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<InvalidOperationException>(expectedException);
        }

        [Fact, Trait("Category", "TimeSlot_Tests")]
        public void AddClass_ClassHasNonSchoolDay_ThrowsInvalidOperationException()
        {
            // Arrange
            var unacceptableClass = new Class
            {
                Days = DaysOfWeek.Monday | DaysOfWeek.Saturday | DaysOfWeek.Friday,
                EndTime = DateTime.Parse("12:00 PM"),
                Instructors = "Mary Joe",
                Location = "Mendel 154",
                Name = "CSC 1210",
                Section = "001",
                StartTime = DateTime.Parse("10:00 AM")
            };

            // Act
            Exception expectedException = Record.Exception(() => _timeSlot.AddClass(unacceptableClass));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<InvalidOperationException>(expectedException);
        }
    }
}