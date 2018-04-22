using Xunit;

namespace WeeklyCourseCalendar.Domain.Tests
{
    public class WeeklyScheduleTests
    {
        [Fact, Trait("Category", "WeeklySchedule")]
        public void WeeklySchedule_CanCreateInstance()
        {
            // Arrange

            // Act
            var weeklySchedule = new WeeklySchedule();

            // Assert
            Assert.NotNull(weeklySchedule);
        }
    }
}