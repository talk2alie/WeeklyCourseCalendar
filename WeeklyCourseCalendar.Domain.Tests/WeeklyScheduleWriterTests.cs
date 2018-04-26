using System;
using System.Collections.Generic;
using System.IO;
using WeeklyCourseCalendar.Domain.Services;
using Xunit;

namespace WeeklyCourseCalendar.Domain.Tests
{
    public class WeeklyScheduleWriterTests
    {
        private readonly IWeeklyScheduleWriter _weeklyScheduleWriter;

        public WeeklyScheduleWriterTests()
        {
            _weeklyScheduleWriter = new WeeklyScheduleWriter();
        }

        [Fact, Trait("Category", "WeeklyScheduleWriter")]
        public void WeeklyScheduleWriter_CanCreateInstance()
        {
            // Arrange

            // Act
            var weeklyScheduleWriter = new WeeklyScheduleWriter();

            // Assert
            Assert.NotNull(weeklyScheduleWriter);
        }

        [Fact, Trait("Category", "WeeklyScheduleWriter")]
        public void WriteAsHtml_WeeklySchedule_ProducesAValidHtmlFile()
        {
            // Arrange
            string outputPath = "schedule.data";
            const string semesterName = "Fall 2018";
            var semesterStartDate = DateTime.Parse("August 16, 2018");
            var semesterEndDate = DateTime.Parse("December 7, 2018");

            // Act
            var weeklySchedule = new WeeklySchedule(semesterName, semesterStartDate, semesterEndDate);
            IEnumerable<Class> classes = GetDemoClasses();
            foreach (Class @class in classes)
            {
                weeklySchedule.AddClass(@class);
            }
            outputPath = _weeklyScheduleWriter.WriteAsHtml(weeklySchedule, outputPath);

            // Assert
            Assert.True(File.Exists(outputPath));
        }

        private IEnumerable<Class> GetDemoClasses()
        {
            var classes = new List<Class>
            {
                new Class
                {
                    Day = DayOfWeek.Wednesday,
                    EndTime = DateTime.Parse("8:50 PM"),
                    Instructors = "N/A",
                    Location = "TBA",
                    Name = "CSC 1010",
                    Section = "100",
                    StartTime = DateTime.Parse("6:10 PM"),
                    Title = "Programming for All"
                },
                new Class
                {
                    Day = DayOfWeek.Thursday,
                    EndTime = DateTime.Parse("8:50 PM"),
                    Instructors = "N/A",
                    Location = "TBA",
                    Name = "CSC 1020",
                    Section = "100",
                    StartTime = DateTime.Parse("6:10 PM"),
                    Title = "Programming for All"
                },
                new Class
                {
                    Day = DayOfWeek.Tuesday,
                    EndTime = DateTime.Parse("9:45 AM"),
                    Instructors = "Mary Angela Papalaskari",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "001",
                    StartTime = DateTime.Parse("8:30 AM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Tuesday,
                    EndTime = DateTime.Parse("9:45 AM"),
                    Instructors = "Don Goelman",
                    Location = "Mendell G87",
                    Name = "CSC 1058",
                    Section = "001",
                    StartTime = DateTime.Parse("8:30 AM"),
                    Title = "Introduction to Database Systems"
                },
                new Class
                {
                    Day = DayOfWeek.Thursday,
                    EndTime = DateTime.Parse("9:45 AM"),
                    Instructors = "Mary Angela Papalaskari",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "001",
                    StartTime = DateTime.Parse("8:30 AM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Wednesday,
                    EndTime = DateTime.Parse("9:20 AM"),
                    Instructors = "Mary Angela Papalaskari",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "001",
                    StartTime = DateTime.Parse("8:30 AM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Tuesday,
                    EndTime = DateTime.Parse("11:15 AM"),
                    Instructors = "Mary Angela Papalaskari",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "002",
                    StartTime = DateTime.Parse("10:00 AM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Thursday,
                    EndTime = DateTime.Parse("11:15 AM"),
                    Instructors = "Mary Angela Papalaskari",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "002",
                    StartTime = DateTime.Parse("10:00 AM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Wednesday,
                    EndTime = DateTime.Parse("10:20 AM"),
                    Instructors = "Mary Angela Papalaskari",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "002",
                    StartTime = DateTime.Parse("9:30 AM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Tuesday,
                    EndTime = DateTime.Parse("12:45 PM"),
                    Instructors = "Daniel T. Joyce",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "003",
                    StartTime = DateTime.Parse("11:30 AM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Thursday,
                    EndTime = DateTime.Parse("12:45 PM"),
                    Instructors = "Daniel T. Joyce",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "003",
                    StartTime = DateTime.Parse("11:30 AM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Wednesday,
                    EndTime = DateTime.Parse("12:20 PM"),
                    Instructors = "Daniel T. Joyce",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "003",
                    StartTime = DateTime.Parse("11:30 AM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Tuesday,
                    EndTime = DateTime.Parse("2:15 PM"),
                    Instructors = "Barbara Hoffman Zimmerman",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "004",
                    StartTime = DateTime.Parse("1:00 PM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Thursday,
                    EndTime = DateTime.Parse("2:15 PM"),
                    Instructors = "Barbara Hoffman Zimmerman",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "004",
                    StartTime = DateTime.Parse("1:00 PM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Wednesday,
                    EndTime = DateTime.Parse("1:20 PM"),
                    Instructors = "Barbara Hoffman Zimmerman",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "004",
                    StartTime = DateTime.Parse("12:30 PM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Tuesday,
                    EndTime = DateTime.Parse("3:45 PM"),
                    Instructors = "Barbara Hoffman Zimmerman",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "005",
                    StartTime = DateTime.Parse("2:30 PM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Thursday,
                    EndTime = DateTime.Parse("3:45 PM"),
                    Instructors = "Barbara Hoffman Zimmerman",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "005",
                    StartTime = DateTime.Parse("2:30 PM"),
                    Title = "Algorithms & Data Struc I"
                },
                new Class
                {
                    Day = DayOfWeek.Wednesday,
                    EndTime = DateTime.Parse("3:50 PM"),
                    Instructors = "Barbara Hoffman Zimmerman",
                    Location = "TBA",
                    Name = "CSC 1051",
                    Section = "005",
                    StartTime = DateTime.Parse("3:00 PM"),
                    Title = "Algorithms & Data Struc I"
                }
            };
            return classes;
        }
    }
}