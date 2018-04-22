using System;

namespace WeeklyCourseCalendar.Domain
{
    public class Class
    {
        public string Name { get; set; }

        public string Section { get; set; }

        public string Title { get; set; }

        public DaysOfWeek Days { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Location { get; set; }

        public string Instructors { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Class @class)
            {
                return Name.Equals(@class.Name) && Section.Equals(@class.Section);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Section.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name}-{Section}{Environment.NewLine}" +
                   $"{Title}{Environment.NewLine}" +
                   $"{StartTime.ToShortTimeString()}-{EndTime.ToShortTimeString()}";
        }
    }
}