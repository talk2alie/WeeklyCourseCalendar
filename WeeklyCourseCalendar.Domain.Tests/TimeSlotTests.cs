using System;
using Xunit;

namespace WeeklyCourseCalendar.Domain.Tests
{
    public class TimeSlotTests
    {
        private readonly TimeSlot _timeSlot;

        public TimeSlotTests()
        {
            _timeSlot = new TimeSlot(day: DayOfWeek.Monday, time: DateTime.Parse("10:05 AM"), slotSpan: 1);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void TimeSlotCreation_SchoolDayAndTime_CreatesInstance()
        {
            // Arrange
            DayOfWeek expectedDay = DayOfWeek.Monday;
            var expectedTime = DateTime.Parse("8:00 AM");
            string expectedTimeSlotId = $"{expectedDay.ToString()}_{expectedTime.ToShortTimeString()}".Replace(" ", "");

            // Act
            var timeSlot = new TimeSlot(day: DayOfWeek.Monday, time: DateTime.Parse("8:00 AM"), slotSpan: 1);
            DayOfWeek actualDay = timeSlot.Day;
            DateTime actualTime = timeSlot.Time;
            string actualTimeSlotId = timeSlot.Id;

            // Assert
            Assert.NotNull(timeSlot);
            Assert.Equal(expectedDay, actualDay);
            Assert.Equal(expectedTime, actualTime);
            Assert.Equal(expectedTimeSlotId, actualTimeSlotId);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void TimeSlotCreation_NonSchoolDay_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            DayOfWeek invalidDay = DayOfWeek.Saturday;
            var time = DateTime.Parse("8:00 AM");

            // Act
            Exception expectedException = Record.Exception(() => new TimeSlot(invalidDay, time, slotSpan: 1));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<ArgumentOutOfRangeException>(expectedException);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void TimeSlotCreation_NonSchoolHours_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            DayOfWeek day = DayOfWeek.Monday;
            var invalidTime = DateTime.Parse("7:00 AM");

            // Act
            Exception expectedException = Record.Exception(() => new TimeSlot(day, invalidTime, slotSpan: 1));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<ArgumentOutOfRangeException>(expectedException);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void AddClass_AcceptableClass_AddsClassSuccessfully()
        {
            // Arrange
            var acceptableClass = new Class
            {
                Day = DayOfWeek.Monday,
                EndTime = DateTime.Parse("12:00 PM"),
                Instructors = "Mary Joe",
                Location = "Mendel 154",
                Name = "CSC 1210",
                Section = "001",
                StartTime = DateTime.Parse("8:05 AM"),
                Title = "Introduction to Programming with Java"
            };
            const int addedClassCount = 1;
            int expectedClassesCount = _timeSlot.OccupiedSpacesCount + addedClassCount;

            // Act
            _timeSlot.AddClass(acceptableClass);
            int actualClassesCount = _timeSlot.OccupiedSpacesCount;

            // Assert
            Assert.Equal(expectedClassesCount, actualClassesCount);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void AddClass_ClassEndsBeforeSlotTime_ThrowsInvalidOperationException()
        {
            // Arrange
            var unacceptableClass = new Class
            {
                Day = DayOfWeek.Monday,
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

        [Fact, Trait("Category", "TimeSlot")]
        public void AddClass_ClassStartsAfterSlotTime_ThrowsInvalidOperationException()
        {
            // Arrange
            var unacceptableClass = new Class
            {
                Day = DayOfWeek.Monday,
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

        [Fact, Trait("Category", "TimeSlot")]
        public void AddClass_ClassHasNonSchoolDay_ThrowsInvalidOperationException()
        {
            // Arrange
            var unacceptableClass = new Class
            {
                Day = DayOfWeek.Saturday,
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

        [Fact, Trait("Category", "TimeSlot")]
        public void AddClass_DuplicateClasses_ThrowsInvalidOperationException()
        {
            // Arrange
            var newClass = new Class
            {
                Day = DayOfWeek.Monday,
                EndTime = DateTime.Parse("12:00 PM"),
                Instructors = "Mary Joe",
                Location = "Mendel 154",
                Name = "CSC 1210",
                Section = "001",
                StartTime = DateTime.Parse("10:00 AM"),
                Title = "Introduction to Programming"
            };
            _timeSlot.AddClass(newClass);

            var duplicateClass = new Class
            {
                Day = DayOfWeek.Monday,
                EndTime = DateTime.Parse("12:00 PM"),
                Instructors = "Mary Joe",
                Location = "Mendel 154",
                Name = "CSC 1210",
                Section = "001",
                StartTime = DateTime.Parse("10:00 AM"),
                Title = "Introduction to Programming"
            };

            // Act
            Exception expectedException = Record.Exception(() => _timeSlot.AddClass(duplicateClass));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<InvalidOperationException>(expectedException);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void AddClass_SlotIsFull_ThrowsInvalidOperationException()
        {
            // Arrange
            PopulateSlotWithNClasses(n: 10);
            var unacceptable = new Class
            {
                Day = DayOfWeek.Monday,
                EndTime = DateTime.Parse("12:00 PM"),
                Instructors = "Mary Joe",
                Location = "Mendel 154",
                Name = "CSC 1210",
                Section = "001",
                StartTime = DateTime.Parse("10:00 AM")
            };

            // Act
            Exception expectedException = Record.Exception(() => _timeSlot.AddClass(unacceptable));

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsType<InvalidOperationException>(expectedException);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void Equals_ObjectsWithSameReference_AreEqual()
        {
            // Arrange
            var timeSlot = new TimeSlot(day: DayOfWeek.Friday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);
            TimeSlot copiedReference = timeSlot;

            // Act
            bool areEqual = timeSlot.Equals(copiedReference);

            // Assert
            Assert.True(areEqual);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void Equals_ObjectsWithSameDayAndTime_AreEqual()
        {
            // Arrange
            var leftSide = new TimeSlot(day: DayOfWeek.Friday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);
            var rightSide = new TimeSlot(day: DayOfWeek.Friday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);

            // Act
            bool areEqual = leftSide.Equals(rightSide);

            // Assert
            Assert.True(areEqual);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void Equals_ObjectsWithDifferentDay_AreNotEqual()
        {
            // Arrange
            var leftSide = new TimeSlot(day: DayOfWeek.Monday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);
            var rightSide = new TimeSlot(day: DayOfWeek.Friday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);

            // Act
            bool areEqual = leftSide.Equals(rightSide);

            // Assert
            Assert.False(areEqual);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void Equals_ObjectsWithDifferentTime_AreNotEqual()
        {
            // Arrange
            var leftSide = new TimeSlot(day: DayOfWeek.Monday, time: DateTime.Parse("5:15 PM"), slotSpan: 1);
            var rightSide = new TimeSlot(day: DayOfWeek.Friday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);

            // Act
            bool areEqual = leftSide.Equals(rightSide);

            // Assert
            Assert.False(areEqual);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void Equals_ObjectsOfDifferentTypes_AreNotEqual()
        {
            // Arrange
            var leftSide = new TimeSlot(day: DayOfWeek.Monday, time: DateTime.Parse("5:15 PM"), slotSpan: 1);
            string rightSide = "Hello World";

            // Act
            bool areEqual = leftSide.Equals(rightSide);

            // Assert
            Assert.False(areEqual);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void GetHashCode_ObjectsWithSameReference_HaveTheSameHashCode()
        {
            // Arrange
            var leftSide = new TimeSlot(day: DayOfWeek.Monday, time: DateTime.Parse("5:15 PM"), slotSpan: 1);
            TimeSlot rightSide = leftSide;

            // Act
            int leftSideHashCode = leftSide.GetHashCode();
            int rightSideHashCode = rightSide.GetHashCode();

            // Assert
            Assert.Equal(leftSideHashCode, rightSideHashCode);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void GetHashCode_ObjectsWithSameDayAndTime_HaveTheSameHashCode()
        {
            // Arrange
            var leftSide = new TimeSlot(day: DayOfWeek.Friday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);
            var rightSide = new TimeSlot(day: DayOfWeek.Friday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);

            // Act
            int leftSideHashCode = leftSide.GetHashCode();
            int rightSideHashCode = rightSide.GetHashCode();

            // Assert
            Assert.Equal(leftSideHashCode, rightSideHashCode);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void GetHashCode_ObjectsWithDifferentDay_DoNotHaveTheSameHashCode()
        {
            // Arrange
            var leftSide = new TimeSlot(day: DayOfWeek.Monday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);
            var rightSide = new TimeSlot(day: DayOfWeek.Friday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);

            // Act
            int leftSideHashCode = leftSide.GetHashCode();
            int rightSideHashCode = rightSide.GetHashCode();

            // Assert
            Assert.NotEqual(leftSideHashCode, rightSideHashCode);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void GetHashCode_ObjectsWithDifferentTime_DoNotHaveTheSameHashCode()
        {
            // Arrange
            var leftSide = new TimeSlot(day: DayOfWeek.Monday, time: DateTime.Parse("5:15 PM"), slotSpan: 1);
            var rightSide = new TimeSlot(day: DayOfWeek.Friday, time: DateTime.Parse("6:15 PM"), slotSpan: 1);

            // Act
            int leftSideHashCode = leftSide.GetHashCode();
            int rightSideHashCode = rightSide.GetHashCode();

            // Assert
            Assert.NotEqual(leftSideHashCode, rightSideHashCode);
        }

        [Fact, Trait("Category", "TimeSlot")]
        public void ToString_ValidTimeSlot_ReturnsId()
        {
            // Arrange
            var validTimeSlot = new TimeSlot(day: DayOfWeek.Monday, time: DateTime.Parse("5:15 PM"), slotSpan: 1);

            // Act
            string toStringValue = validTimeSlot.ToString();
            string idValue = $"{validTimeSlot.Id}; Spans {validTimeSlot.SlotSpan}" +
                (validTimeSlot.SlotSpan > 1 ? "s" : String.Empty);

            // Assert
            Assert.Equal(toStringValue, idValue);
        }

        private void PopulateSlotWithNClasses(int n)
        {
            int index = 0;
            while (index < n)
            {
                _timeSlot.AddClass(new Class
                {
                    Day = DayOfWeek.Monday,
                    EndTime = DateTime.Parse("10:50 AM"),
                    Instructors = $"Instructor Number{index + 1}",
                    Location = $"Location Number{index + 1}",
                    Name = $"Class Number{index + 1}",
                    Section = $"00{index + 1}",
                    StartTime = DateTime.Parse("10:00 AM"),
                    Title = $"Title Number{index + 1}"
                });
                index++;
            }
        }
    }
}