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
            int actualSchoolDaysCount = weeklySchedule.SchoolDays.ToList().Count;
            int actualSchoolTimesCount = weeklySchedule.SchoolTimes.ToList().Count;

            string actualSemesterName = weeklySchedule.SemesterName;
            DateTime actualSemesterStartDate = weeklySchedule.SemesterStartDate;
            DateTime actualSemesterEndDate = weeklySchedule.SemesterEndDate;
            IEnumerable<TimeSlot> timeSlots = weeklySchedule.TimeSlots;

            Assert.Equal(expectedSchoolDaysCount, actualSchoolDaysCount);
            Assert.Equal(expectedSchoolTimesCount, actualSchoolTimesCount);

            Assert.Equal(expectedSemesterName, actualSemesterName);
            Assert.Equal(expectedSemesterStartDate, actualSemesterStartDate);
            Assert.Equal(expectedSemesterEndDate, actualSemesterEndDate);
        }

        [Fact, Trait("Category", "WeeklySchedule")]
        public void AddClass_2Hr40MinutesClassToEmptySchedule_CreatesTimeSlotThatSpans32Slots()
        {
            // Arrange
            var newClass = new Class
            {
                Day = DayOfWeek.Wednesday,
                EndTime = DateTime.Parse("8:50 PM"),
                Instructors = " Mary Angela Papalaskari",
                Location = "TBA",
                Name = "CSC 1010",
                Section = "100",
                StartTime = DateTime.Parse("06:10 pm"),
                Title = "Programming for All"
            };
            const int expectedSlotSpan = 32;

            // Act
            _weeklySchedule.AddClass(newClass);
            int actualSlotSpan = _weeklySchedule.TimeSlots.FirstOrDefault().SlotSpan;

            // Assert
            Assert.Equal(expectedSlotSpan, actualSlotSpan);
        }

        [Fact, Trait("Category", "WeeklySchedule")]
        public void AddClass_2Hr40MinutesClassToExistingSlot_AddsClassToTimeSlot()
        {
            // Arrange
            AddClassToSchedule();
            int expectedSlotsCount = _weeklySchedule.TimeSlots.Count();
            const int classesAddedCount = 1;
            int expectedClassesInSlotCount = _weeklySchedule.TimeSlots.First().OccupiedSpacesCount + classesAddedCount;

            var classToAdd = new Class
            {
                Day = DayOfWeek.Wednesday,
                EndTime = DateTime.Parse("8:50 PM"),
                Instructors = " Mary Angela Papalaskari",
                Location = "Mendel G87",
                Name = "CSC 1020",
                Section = "001",
                StartTime = DateTime.Parse("06:10 pm"),
                Title = "Introduction to Programming in C"
            };

            // Act
            _weeklySchedule.AddClass(classToAdd);
            int actualSlotsCount = _weeklySchedule.TimeSlots.Count();
            int actualClassesInSlotCount = _weeklySchedule.TimeSlots.First().OccupiedSpacesCount;

            // Assert
            Assert.Equal(expectedSlotsCount, actualSlotsCount);
            Assert.Equal(expectedClassesInSlotCount, actualClassesInSlotCount);
        }

        private void AddClassToSchedule()
        {
            var newClass = new Class
            {
                Day = DayOfWeek.Wednesday,
                EndTime = DateTime.Parse("8:50 PM"),
                Instructors = " Mary Angela Papalaskari",
                Location = "TBA",
                Name = "CSC 1010",
                Section = "100",
                StartTime = DateTime.Parse("06:10 pm"),
                Title = "Programming for All"
            };
            _weeklySchedule.AddClass(newClass);
        }
    }
}