using System;
using Xunit;

namespace WeeklyCourseCalendar.Domain.Tests
{
    public class ClassTests
    {
        [Fact, Trait("Category", "Class")]
        public void ClassConstructor_CanCreateNewInstance()
        {
            // Arrange

            // Act
            var @class = new Class();

            // Assert
            Assert.NotNull(@class);
            Assert.IsType<Class>(@class);
        }

        [Fact, Trait("Category", "Class")]
        public void Equals_SameReferenceObjects_AreEqual()
        {
            // Arrange
            var leftSide = new Class
            {
                Day = DayOfWeek.Monday,
                EndTime = DateTime.Parse("9:00 PM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G87",
                Name = "CSC 1100",
                Section = "001",
                StartTime = DateTime.Parse("6:15 PM"),
                Title = "Programming For All"
            };

            Class rightSide = leftSide;

            // Act
            bool leftSideEqualsRightSide = leftSide.Equals(rightSide);

            // Assert
            Assert.True(leftSideEqualsRightSide);
        }

        [Fact, Trait("Category", "Class")]
        public void Equals_ObjectsWithSameNameAndSection_AreEqual()
        {
            // Arrange
            var leftSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("9:00 PM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G87",
                Name = "CSC 1100",
                Section = "001",
                StartTime = DateTime.Parse("6:15 PM"),
                Title = "Programming For All"
            };

            var rightSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("10:00 AM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G86",
                Name = "CSC 1100",
                Section = "001",
                StartTime = DateTime.Parse("10:50 AM"),
                Title = "Programming For All"
            };

            // Act
            bool leftSideEqualsRightSide = leftSide.Equals(rightSide);

            // Assert
            Assert.True(leftSideEqualsRightSide);
        }

        [Fact, Trait("Category", "Class")]
        public void Equals_ObjectsWithDifferentNameAndOrSection_AreNotEqual()
        {
            // Arrange
            var leftSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("9:00 PM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G87",
                Name = "CSC 1200",
                Section = "001",
                StartTime = DateTime.Parse("6:15 PM"),
                Title = "Programming For All"
            };

            var rightSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("10:00 AM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G86",
                Name = "CSC 1100",
                Section = "001",
                StartTime = DateTime.Parse("10:50 AM"),
                Title = "Programming For All"
            };

            // Act
            bool leftSideEqualsRightSide = leftSide.Equals(rightSide);

            // Assert
            Assert.False(leftSideEqualsRightSide);
        }

        [Fact, Trait("Category", "Class")]
        public void Equals_ObjectsOfDifferentTypes_AreNotEqual()
        {
            // Arrange
            var leftSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("9:00 PM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G87",
                Name = "CSC 1200",
                Section = "001",
                StartTime = DateTime.Parse("6:15 PM"),
                Title = "Programming For All"
            };

            string rightSide = "Hello there";

            // Act
            bool leftSideEqualsRightSide = leftSide.Equals(rightSide);

            // Assert
            Assert.False(leftSideEqualsRightSide);
        }

        [Fact, Trait("Category", "Class")]
        public void GetHashCode_SameReferenceObjects_HaveTheSameHashCode()
        {
            // Arrange
            var leftSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("9:00 PM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G87",
                Name = "CSC 1100",
                Section = "001",
                StartTime = DateTime.Parse("6:15 PM"),
                Title = "Programming For All"
            };

            Class rightSide = leftSide;

            // Act
            int leftSideHashCode = leftSide.GetHashCode();
            int rightSideHashCode = rightSide.GetHashCode();

            // Assert
            Assert.Equal(leftSideHashCode, rightSideHashCode);
        }

        [Fact, Trait("Category", "Class")]
        public void GetHashCode_ObjectsWithSameNameAndSection_HaveTheSameHashCode()
        {
            // Arrange
            var leftSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("9:00 PM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G87",
                Name = "CSC 1100",
                Section = "001",
                StartTime = DateTime.Parse("6:15 PM"),
                Title = "Programming For All"
            };

            var rightSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("10:00 AM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G86",
                Name = "CSC 1100",
                Section = "001",
                StartTime = DateTime.Parse("10:50 AM"),
                Title = "Programming For All"
            };

            // Act
            int leftSideHashCode = leftSide.GetHashCode();
            int rightSideHashCode = rightSide.GetHashCode();

            // Assert
            Assert.Equal(leftSideHashCode, rightSideHashCode);
        }

        [Fact, Trait("Category", "Class")]
        public void GetHashCode_ObjectsWithDifferentNameAndOrSection_DoNotHaveTheSameHashCode()
        {
            // Arrange
            var leftSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("9:00 PM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G87",
                Name = "CSC 1200",
                Section = "001",
                StartTime = DateTime.Parse("6:15 PM"),
                Title = "Programming For All"
            };

            var rightSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("10:00 AM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G86",
                Name = "CSC 1100",
                Section = "001",
                StartTime = DateTime.Parse("10:50 AM"),
                Title = "Programming For All"
            };

            // Act
            int leftSideHashCode = leftSide.GetHashCode();
            int rightSideHashCode = rightSide.GetHashCode();

            // Assert
            Assert.NotEqual(leftSideHashCode, rightSideHashCode);
        }

        [Fact, Trait("Category", "Class")]
        public void GetHashCode_ObjectsOfDifferentTypes_DoNotHaveTheSameHashCode()
        {
            // Arrange
            var leftSide = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("9:00 PM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G87",
                Name = "CSC 1200",
                Section = "001",
                StartTime = DateTime.Parse("6:15 PM"),
                Title = "Programming For All"
            };

            string rightSide = "Hello there";

            // Act
            int leftSideHashCode = leftSide.GetHashCode();
            int rightSideHashCode = rightSide.GetHashCode();

            // Assert
            Assert.NotEqual(leftSideHashCode, rightSideHashCode);
        }

        [Fact, Trait("Category", "Class")]
        public void ToString_ValidClass_ReturnsClassDiplayText()
        {
            // Arrange
            var @class = new Class
            {
                Day = DayOfWeek.Monday | DayOfWeek.Wednesday | DayOfWeek.Friday,
                EndTime = DateTime.Parse("9:00 PM"),
                Instructors = "Kristen Obermyer",
                Location = "Mendel G87",
                Name = "CSC 1100",
                Section = "001",
                StartTime = DateTime.Parse("6:15 PM"),
                Title = "Programming For All"
            };

            string expectedToStringValue = $"{@class.Name}-{@class.Section}{Environment.NewLine}" +
                   $"{@class.Title}{Environment.NewLine}" +
                   $"{@class.StartTime.ToShortTimeString()}-{@class.EndTime.ToShortTimeString()}";

            // Act
            string actualToStringValue = @class.ToString();

            // Assert
            Assert.Equal(expectedToStringValue, actualToStringValue);
        }
    }
}