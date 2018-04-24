using System;

namespace WeeklyCourseCalendar.Domain
{
    public class Class
    {
        public string Name { get; set; }

        public string Section { get; set; }

        public string Title { get; set; }

        public DayOfWeek Day { get; set; }

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
                return Name.Equals(@class.Name, StringComparison.InvariantCultureIgnoreCase) &&
                    Section.Equals(@class.Section, StringComparison.InvariantCultureIgnoreCase) &&
                    Title.Equals(@class.Title, StringComparison.InvariantCultureIgnoreCase) &&
                    Day == @class.Day &&
                    StartTime.TimeOfDay == @class.StartTime.TimeOfDay &&
                    EndTime.TimeOfDay == @class.EndTime.TimeOfDay &&
                    Location.Equals(@class.Location, StringComparison.InvariantCultureIgnoreCase) &&
                    Instructors.Equals(@class.Instructors, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (Name.GetHashCode() ^ Section.GetHashCode()) +
                (Title.GetHashCode() ^ Day.GetHashCode()) +
                (StartTime.GetHashCode() ^ EndTime.GetHashCode()) +
                (Location.GetHashCode() ^ Instructors.GetHashCode());
        }

        public override string ToString()
        {
            return $"{Name}-{Section}{Environment.NewLine}" +
                   $"{Title}{Environment.NewLine}" +
                   $"{StartTime.ToShortTimeString()} to {EndTime.ToShortTimeString()}";
        }
    }
}