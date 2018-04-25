using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WeeklyCourseCalendar.Data.Services
{
    public class CourseScheduleReader : ICourseScheduleReader
    {
        public IEnumerable<Course> ReadFromFile(string filePath)
        {
            throw new NotImplementedException();
        }

        private List<Course> GetCoursesFromText(IEnumerable<string> scheduleText)
        {
            List<string> individualCourseTexts = DivideTextIntoIndividualCourseTexts(scheduleText);
            List<Course> courses = GetIndividualCoursesFromIndividualCourseTexts(individualCourseTexts);
            return courses;
        }

        private List<string> DivideTextIntoIndividualCourseTexts(IEnumerable<string> linesFromScheduleData)
        {
            const string courseNumberRegexText = @"^\b[A-Z]{3}\b\s*\b\d{4,5}\b";
            var scheduleTexts = new List<string>();
            StringBuilder builder = null;
            foreach (string line in linesFromScheduleData)
            {
                if (Regex.IsMatch(line, courseNumberRegexText))
                {
                    if (builder != null)
                    {
                        scheduleTexts.Add(builder.ToString());
                    }
                    builder = new StringBuilder();
                    builder.AppendLine(line);
                }
                else
                {
                    builder.AppendLine(line);
                }
            }
            scheduleTexts.Add(builder.ToString());
            return scheduleTexts;
        }

        private List<Course> GetIndividualCoursesFromIndividualCourseTexts(List<string> individualScheduleTexts)
        {
            IEnumerable<Course> courses = individualScheduleTexts.Select(text => GetCourseFromCourseScheduleText(text));
            return courses.ToList();
        }

        private Course GetCourseFromCourseScheduleText(string courseScheduleText)
        {
            const string courseRegexText = @"(?<courseNumber>\b[A-Z]{3}\b\s*\b\d{4,5}\b)\s+\-\s+(?<courseSection>\b\w{2,3}\b)\s+(?<courseName>.*)\s+CRN:";
            const string instructorsRegexText = @"Instructors:\s+(?<instructors>.*)";
            const string attributesRegexText = @"Attributes:\s+(?<attributes>.*)";
            const string commentRegexText = @"Comment:\s+(?<comment>.*)\;";
            string courseDetailsRegexText = String.Join("|", courseRegexText, instructorsRegexText, attributesRegexText, commentRegexText);

            var courseDetailsRegex = new Regex(courseDetailsRegexText, RegexOptions.Compiled | RegexOptions.Multiline);
            var course = new Course();

            MatchCollection courseDetailsMatches = courseDetailsRegex.Matches(courseScheduleText);
            foreach (Match match in courseDetailsMatches)
            {
                GroupCollection groups = match.Groups;

                string courseNumber = groups["courseNumber"].Value;
                if (!String.IsNullOrWhiteSpace(courseNumber))
                {
                    course.Number = courseNumber.Trim();
                }

                string courseName = groups["courseName"].Value;
                if (!String.IsNullOrWhiteSpace(courseName))
                {
                    course.Name = courseName.Trim();
                }

                string courseSection = groups["courseSection"].Value;
                if (!String.IsNullOrWhiteSpace(courseSection))
                {
                    course.Section = courseSection.Trim();
                }

                string instructors = groups["instructors"].Value;
                if (!String.IsNullOrWhiteSpace(instructors))
                {
                    course.Instructor = instructors.Trim();
                }

                string comment = groups["comment"].Value;
                if (!String.IsNullOrWhiteSpace(comment))
                {
                    course.Comment = comment.Trim();
                }

                string attributes = groups["attributes"].Value;
                if (!String.IsNullOrWhiteSpace(attributes))
                {
                    course.Attributes = attributes.Trim();
                }
            }
            course.Schedules = GetCourseSchedulesFromText(courseScheduleText);
            return course;
        }

        private List<Schedule> GetCourseSchedulesFromText(string courseScheduleText)
        {
            var schedules = new List<Schedule>();

            const string courseScheduleRegexText =
            @"(?<schedule>(?<days>[MTWRF]{1,5})\s+from\s+(?<startTime>[0-9]{2}\:[0-9]{2}\s+(am|pm))\s+to\s+(?<endTime>[0-9]{2}\:[0-9]{2}\s+(am|pm)))";
            var scheduleRegex = new Regex(courseScheduleRegexText, RegexOptions.Compiled | RegexOptions.Multiline);
            MatchCollection matches = scheduleRegex.Matches(courseScheduleText);
            foreach (Match match in matches)
            {
                var schedule = new Schedule();
                GroupCollection groups = match.Groups;

                string days = groups["days"].Value;
                if (!String.IsNullOrWhiteSpace(days))
                {
                    schedule.Days = GetDaysOfWeek(days);
                }

                string startTime = groups["startTime"].Value;
                if (!String.IsNullOrWhiteSpace(startTime))
                {
                    schedule.StartTime = DateTime.Parse(startTime);
                }

                string endTime = groups["endTime"].Value;
                if (!String.IsNullOrWhiteSpace(endTime))
                {
                    schedule.EndTime = DateTime.Parse(endTime);
                }

                schedules.Add(schedule);
            }

            return schedules;
        }

        private DaysOfWeek GetDaysOfWeek(string days)
        {
            DaysOfWeek daysOfWeek = DaysOfWeek.None;
            if (days.Contains("M"))
            {
                daysOfWeek |= DaysOfWeek.Monday;
            }

            if (days.Contains("T"))
            {
                daysOfWeek |= DaysOfWeek.Tuesday;
            }

            if (days.Contains("W"))
            {
                daysOfWeek |= DaysOfWeek.Wednesday;
            }

            if (days.Contains("R"))
            {
                daysOfWeek |= DaysOfWeek.Thursday;
            }

            if (days.Contains("F"))
            {
                daysOfWeek |= DaysOfWeek.Friday;
            }

            return daysOfWeek;
        }
    }
}