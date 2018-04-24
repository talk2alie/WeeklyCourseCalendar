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
                Day = DayOfWeek.Tuesday,
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

        //[Fact, Trait("Category", "WeeklySchedule")]
        //public void AddClassToTimeSlots_ExistingTimeSlots_TimeSlotsShouldContainClass()
        //{
        //    // Arrange
        //    AddOne1Hour15MinutesClassToWeeklySchedule();
        //    var validClass = new Class
        //    {
        //        Day = DayOfWeek.Thursday,
        //        EndTime = DateTime.Parse("9:45 AM"),
        //        Instructors = "Mary Angela Papalaskari",
        //        Location = "TBA",
        //        Name = "CSC 1051",
        //        Section = "001",
        //        StartTime = DateTime.Parse("8:30 AM"),
        //        Title = "Algorithms & Data Struc I"
        //    };
        //}

        private void AddOne1Hour15MinutesClassToWeeklySchedule()
        {
            var validClass = new Class
            {
                Day = DayOfWeek.Tuesday | DayOfWeek.Thursday,
                EndTime = DateTime.Parse("9:00 AM"),
                Instructors = "Mary Angela Papalaskari",
                Location = "TBA",
                Name = "CSC 1052",
                Section = "001",
                StartTime = DateTime.Parse("8:30 AM"),
                Title = "Algorithms & Data Struc I"
            };
            _weeklySchedule.AddClassToTimeSlots(validClass);
        }
    }
}