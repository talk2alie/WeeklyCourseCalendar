using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeeklyCourseCalendar.Data
{
    public class Course
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public string Section { get; set; }

        public string Instructor { get; set; }

        public string Comment { get; set; }

        public string Attributes { get; set; }

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();

        public override string ToString()
        {
            string courseText = $"{Number} - {Section} {Name}: ";
            Schedules.ForEach(schedule => courseText += schedule + "; ");
            return courseText.Substring(0, courseText.Length - 2);
        }
    }
}