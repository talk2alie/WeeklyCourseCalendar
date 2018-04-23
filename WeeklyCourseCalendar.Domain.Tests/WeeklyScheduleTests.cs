using System;
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

            // Act
            var weeklySchedule = new WeeklySchedule();

            // Assert
            Assert.NotNull(weeklySchedule);
        }

        [Fact, Trait("Category", "WeeklySchedule")]
        public void AddClassToTimeSlots_1Hr15MinutesClassInEmptySchedule_Returns16AllocatedTimeSlots()
        {
            // Arrange
            var validClass = new Class
            {
                Days = DaysOfWeek.Tuesday | DaysOfWeek.Thursday,
                EndTime = DateTime.Parse("9:45 AM"),
                Instructors = "Mary Angela Papalaskari",
                Location = "TBA",
                Name = "CSC 1051",
                Section = "001",
                StartTime = DateTime.Parse("8:30 AM"),
                Title = "Algorithms & Data Struc I"
            };
            const int expectedAllocatedTimeSlotsCount = 16;

            // Act
            _weeklySchedule.AddClassToTimeSlots(validClass);
            int actualAllocatedTimeSlotsCount = _weeklySchedule.AllocatedTimeSlotsCount;

            // Assert
            Assert.Equal(expectedAllocatedTimeSlotsCount, actualAllocatedTimeSlotsCount);
        }
    }
}